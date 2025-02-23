using System;
using System.Collections;
using UnityEngine;

public class EnemyAttackComponent : AttackComponent
{
	public delegate void UpdateWaypoint(Vector3 waypoint);

	public int[] attackMoves = new int[1] { 1 };

	public int[] attackDelays = new int[5] { 25, 35, 50, 75, 100 };

	private int currentAttackDelay = 1;

	private int attackDelayCounter;

	private int walkDelayCounter;

	private int walkDelayInterval = 1;

	private bool seeking = true;

	private bool isAttackMode;

	public int aggrevatedDefenseLength = 200;

	public int minSeekStateLength = 100;

	public int maxSeekStateLength = 1000;

	protected UnityEngine.AI.NavMeshAgent agent;

	public float seekDistance = 35f;

	public bool isEnemyWithPlayer = true;

	public bool originallyIsnemyWithPlayer;

	public UpdateWaypoint updateWaypoint;

	public bool fightNoiseWhenHurt;

	private int enemyInDistanceCacheCounter;

	private bool alternateSeekAndIdleOnWayPointFlag;

	private Vector3 lastKnownAgentDestination;

	private int freezeCount;

	private bool isDestroyed;

	protected override void Awake()
	{
		originallyIsnemyWithPlayer = isEnemyWithPlayer;
		aggressiveWeaponAnims[0] = 8;
		idleWeaponAnims[0] = 8;
		createHeadBoneCollider();
		recoveryRate *= 3f;
		givesItemWhenDie = true;
		maxEnergy = 30f;
		attackerLimbTag = "playerLimb";
		myLimbTag = "enemyLimb";
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		base.Awake();
		chooseTarget();
		Debug.Log(base.name + " added onDead in attkComp");
		onDead = (VNLUtil.NoNameMethod)Delegate.Combine(onDead, new VNLUtil.NoNameMethod(doPostEnemyDeath));
	}

	protected virtual void createHeadBoneCollider()
	{
		BoxCollider boxCollider = base.transform.Find("model/Armature/mainBone/Bone_001/Bone_002/Bone_003").gameObject.AddComponent<BoxCollider>();
		Vector3 center = boxCollider.center;
		center.x = -2.45609f;
		center.z = 0.9899996f;
		boxCollider.center = center;
	}

	public void temporarilyBecomeHostile(float seconds)
	{
		if (!isEnemyWithPlayer)
		{
			float originalSeekDistance = seekDistance;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				isEnemyWithPlayer = false;
				seekDistance = originalSeekDistance;
			}, seconds);
			seekDistance = 1000f;
			isEnemyWithPlayer = true;
		}
	}

	protected override int getPercentDropRate()
	{
		return 80;
	}

	protected override bool isEnemyInAttackDistance()
	{
		if (base.CurrentEnemy == null)
		{
			return false;
		}
		float num = 0f;
		num = (hasThrowingWeapon() ? stoppingDistanceWithThrowingWeapon : ((!(currentWeapon != null)) ? stoppingDistance : stoppingDistanceWithWeapon));
		float num2 = Vector3.Distance(myTransform.position, base.CurrentEnemy.transform.position);
		return num2 <= num + 0.5f;
	}

	protected override void Update()
	{
		if (isDead)
		{
			return;
		}
		base.Update();
		walkDelayCounter++;
		if (!isAttackMode && walkDelayCounter % walkDelayInterval == 0)
		{
			if (seeking)
			{
				stopSeeking(false);
			}
			else
			{
				startSeeking(false);
			}
		}
		if (isAttackMode)
		{
			bool flag = true;
			if (enemyInDistanceCacheCounter % 25 == 0)
			{
				flag = isEnemyInAttackDistance();
			}
			if (flag && isEnemyWithPlayer)
			{
				attack();
				onStopWalking();
			}
			else
			{
				startSeeking(false);
			}
		}
		else if (seeking)
		{
			if (isHurting())
			{
				stopSeeking(true);
				return;
			}
			if (isEnemyWithPlayer)
			{
				if (!isEnemyInAttackDistance())
				{
					chooseTarget();
					onWalkPressed(getAgentVelocity());
				}
				else
				{
					stopSeeking(true);
					isAttackMode = true;
				}
			}
			else
			{
				onWalkPressed(getAgentVelocity());
				if (agent.remainingDistance < 1f)
				{
					chooseTarget();
				}
			}
		}
		else
		{
			detectIdleChangeTime();
		}
		enemyInDistanceCacheCounter++;
	}

	public virtual void chooseTarget()
	{
		if (isEnemyWithPlayer)
		{
			if (base.CurrentEnemy == null)
			{
				GameObject gameObject = GameObject.Find("Player");
				if (gameObject == null)
				{
					VNLUtil.getInstance().doStartCoRoutine(delegate
					{
						chooseTarget();
					});
				}
				else
				{
					setCurrentEnemy(gameObject.GetComponent<AttackComponent>());
				}
			}
			else
			{
				agent.SetDestination(base.CurrentEnemy.transform.position);
			}
			return;
		}
		bool flag = lastKnownAgentDestination.Equals(Vector3.zero);
		float num = 0f;
		if (!flag)
		{
			num = Vector3.Distance(lastKnownAgentDestination, agent.transform.position);
		}
		if (flag || num < agent.stoppingDistance + 3f)
		{
			alternateSeekAndIdleOnWayPointFlag = !alternateSeekAndIdleOnWayPointFlag;
			if (alternateSeekAndIdleOnWayPointFlag && updateWaypoint != null)
			{
				SpawnManager component = GameObject.Find(Application.loadedLevelName + "SpawnManager").GetComponent<SpawnManager>();
				bool flag2 = true;
				Vector3 waypoint = Vector3.up;
				while (flag2)
				{
					waypoint = component.Waypoints[UnityEngine.Random.Range(0, component.Waypoints.Count)];
					flag2 = component.isWaypointTaken(waypoint);
				}
				lastKnownAgentDestination = waypoint;
				updateWaypoint(lastKnownAgentDestination);
				agent.SetDestination(lastKnownAgentDestination);
			}
			else
			{
				stopSeeking();
			}
		}
		else
		{
			agent.SetDestination(lastKnownAgentDestination);
		}
	}

	private void startSeeking(bool isAggrevated)
	{
		if (isDead)
		{
			return;
		}
		isAttackMode = false;
		walkDelayCounter = 0;
		if (isAggrevated)
		{
			walkDelayInterval = aggrevatedDefenseLength;
		}
		else
		{
			walkDelayInterval = UnityEngine.Random.Range(minSeekStateLength, maxSeekStateLength);
		}
		agent.enabled = true;
		agent.stoppingDistance = 0f;
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			if (!isDestroyed)
			{
				if (hasThrowingWeapon())
				{
					agent.stoppingDistance = stoppingDistanceWithThrowingWeapon;
				}
				else if (currentWeapon != null)
				{
					agent.stoppingDistance = stoppingDistanceWithWeapon;
				}
				else if (isEnemyWithPlayer)
				{
					agent.stoppingDistance = stoppingDistance;
				}
			}
		}, 0.5f);
		agent.Resume();
		seeking = true;
		onWalkPressed(getAgentVelocity());
	}

	protected virtual Vector3 getAgentVelocity()
	{
		return agent.velocity * 0.95f;
	}

	private void stopSeeking(bool startSeekingAgainQuickly)
	{
		if (startSeekingAgainQuickly)
		{
			stopSeeking(10);
		}
		else
		{
			stopSeeking(UnityEngine.Random.Range(minSeekStateLength, maxSeekStateLength));
		}
	}

	public virtual void stopSeeking()
	{
		stopSeeking(false);
	}

	private void stopSeeking(int length)
	{
		if (!isDead && agent.enabled)
		{
			isAttackMode = false;
			walkDelayCounter = 0;
			walkDelayInterval = length;
			if (agent.remainingDistance > 1f)
			{
				lastKnownAgentDestination = agent.destination;
			}
			else
			{
				lastKnownAgentDestination = Vector3.zero;
			}
			agent.enabled = false;
			seeking = false;
			onStopWalking();
		}
	}

	public void attack()
	{
		attackDelayCounter++;
		if (attackDelayCounter == currentAttackDelay)
		{
			int num = UnityEngine.Random.Range(0, attackMoves.Length);
			onAttackPressed(attackMoves[num]);
			attackDelayCounter = 0;
			currentAttackDelay = attackDelays[UnityEngine.Random.Range(0, attackDelays.Length)];
		}
		else
		{
			toggleAttackColliders(false);
		}
	}

	public override void onAttackPressed(int tapCount)
	{
		if (!anim.IsPlaying(lastAttack))
		{
			base.onAttackPressed(tapCount);
		}
	}

	protected override AttackComponent getEnemyToLookAtBeforeAttacking()
	{
		return base.CurrentEnemy;
	}

	protected override void onHurt(AttackComponent enemy, Weapon weapon)
	{
		freezePosition();
		base.onHurt(enemy, weapon);
		startSeeking(true);
		if (fightNoiseWhenHurt && (life == 0f || UnityEngine.Random.Range(0, 2) == 0))
		{
			playFightSound();
		}
	}

	private IEnumerator doUnfreezePosition()
	{
		yield return new WaitForSeconds(1f);
		if (freezeCount == 1)
		{
			unfreezePosition();
			freezeCount--;
		}
		else
		{
			freezeCount--;
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		isDestroyed = true;
	}

	protected override void die()
	{
		if (isDead)
		{
			return;
		}
		if (!isEnemyWithPlayer)
		{
			onDead = (VNLUtil.NoNameMethod)Delegate.Combine(onDead, (VNLUtil.NoNameMethod)delegate
			{
				GameObject.Find(Application.loadedLevelName + "SpawnManager").GetComponent<SpawnManager>().avengeInnocent();
				VNLUtil.getInstance().displayMessage("hurtInnocent", false);
			});
		}
		base.die();
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
		if (!NetworkCore.isNetworkMode && UnityEngine.Random.Range(1, 11) == 1)
		{
			Time.timeScale = 0.2f;
		}
		APIService.logFlurryEvent("Killed-" + base.name);
		reportLeaderBoard();
	}

	protected virtual void reportLeaderBoard()
	{
		if (originallyIsnemyWithPlayer)
		{
			APIService.leaderBoard("baddestBullyBeater");
		}
		else
		{
			APIService.leaderBoard("naughtyBully");
		}
	}

	protected override void dieDetachItems()
	{
		foreach (BadNerdItem item in itemList)
		{
			item.becomeDamagedGoods();
		}
		base.dieDetachItems();
	}

	private void freezePosition()
	{
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		freezeCount++;
		StartCoroutine(doUnfreezePosition());
	}

	private void unfreezePosition()
	{
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}

	protected void doPostEnemyDeath()
	{
		VNLUtil.getInstance().resume();
		GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
	}

	protected override float getMaxSpeed()
	{
		return agent.speed;
	}
}
