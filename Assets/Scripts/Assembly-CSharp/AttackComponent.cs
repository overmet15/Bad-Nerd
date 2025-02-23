using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
	public delegate void UpdateData(AttackComponent data);

	public const string WEAPON_TAG = "weapon";

	public const string ENEMY_TAG = "enemy";

	public const string PLAYER_TAG = "Player";

	public const string PLAYER_LIMB = "playerLimb";

	public const string ENEMY_LIMB = "enemyLimb";

	private const int LOOKAT_PERIOD = 10;

	protected const float lowEnergyThreshold = 7f;

	private const int COUNTER_ATTACK_ATTACK_LEVEL = 7;

	public Mesh normalMesh;

	public Mesh helmetMesh;

	public Mesh normalGlassesMesh;

	public Mesh helmetGlassesMesh;

	public Mesh[] geekMeshes;

	public VNLUtil.NoNameMethod onDead;

	public VNLUtil.NoNameMethod onHealthChanged;

	public VNLUtil.NoNameMethod onEnergyChanged;

	public UpdateData updateData;

	[NonSerialized]
	public bool touchedCountroller;

	public float capacity = 5f;

	protected Animation anim;

	private static string ANIM_RUN = "Run";

	private static string ANIM_WALK = "Walk";

	private static string ANIM_IDLE = "Idle";

	private static string ANIM_AGGRESSIVE = "Aggressive";

	private static string ANIM_ATTACK = "Attack";

	private static string ANIM_WEAPON = "Weapon";

	private static string ANIM_THROW = "Throw";

	protected static string ANIM_HURT = "Hurt";

	private static string ANIM_DEATH = "Death";

	private static string ANIM_COUNTER_ATTACK = "CounterAttack";

	private static string ANIM_BLOCK = "Block";

	private static string ANIM_WEAPON_BLOCK = "WeaponBlock";

	protected string attackerLimbTag = "enemyLimb";

	[NonSerialized]
	public string myLimbTag = "playerLimb";

	public int[] beatDownAnims = new int[1] { 9999 };

	public int[] aggressiveAnims = new int[1] { 1 };

	public int[] aggressiveWeaponAnims = new int[1] { 1 };

	public int[] idleAnims = new int[1] { 1 };

	public int[] idleWeaponAnims = new int[1] { 1 };

	public int[] deathAnims = new int[1] { 1 };

	public int[] walkAnims = new int[1] { 1 };

	public int[] runAnims = new int[1] { 1 };

	public int[] weaponWalkAnims = new int[1] { 11 };

	public int[] throwAnims = new int[1] { 1 };

	private Vector3 throwTarget = Vector3.zero;

	public List<BadNerdItem> itemList = new List<BadNerdItem>();

	[NonSerialized]
	public Weapon currentWeapon;

	public List<EquipmentItem> equippedItems = new List<EquipmentItem>();

	[NonSerialized]
	public bool isDead;

	private int idleTime;

	private int idleInterval = 100;

	public int attack1Level = 1;

	public int attack2Level = 1;

	public int attack3Level = 1;

	public int counterAttackLevel;

	public int weaponAttack1Level = 1;

	public int weaponAttack2Level = 1;

	public int weaponAttack3Level = 1;

	[NonSerialized]
	public int currentAttackLevel;

	protected string lastAttack;

	private int lastAttackTapCount;

	protected string lastHurt;

	private bool collidersOn = true;

	public float maxLife = 3f;

	public float life;

	public float strength = 1f;

	public float energy;

	public float maxEnergy = 50f;

	public float recoveryRate = 0.01f;

	public int knockDownProbabiliy = 25;

	private string runAnim;

	private string walkAnim;

	private string weaponWalkAnim;

	private float maxSpeed;

	private Transform character;

	private Vector3 originalColliderSize;

	private Vector3 originalColliderY;

	public string nickName;

	public int chanceOfDroppedItemDividedBy = 3;

	public int maxValueOfDroppedItem;

	private AttackComponent currentEnemy;

	public int voiceType;

	public int lunchMoney = 2;

	private float[] attack1Delays = new float[5] { 0.5f, 0.5f, 0.5f, 0.3f, 0.3f };

	private float[] attack2Delays = new float[5] { 0.5f, 0.3f, 0.3f, 0.3f, 0.3f };

	private float[] attack3Delays = new float[5] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };

	private float[] weaponAttack1Delays = new float[5] { 0.3f, 0.5f, 0.5f, 0.5f, 0.7f };

	private float[] weaponAttack2Delays = new float[5] { 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };

	private float[] weaponAttack3Delays = new float[5] { 0.1f, 0.3f, 0.3f, 0.3f, 0.3f };

	protected Transform myTransform;

	private Vector3 targetToSnapTo;

	private Quaternion rotationToLookAt;

	private int lookAtCount;

	private int snapToCount;

	private bool isDown;

	public float autoFaceThresholdDistance = 8f;

	public float autoFaceThresholdDistanceForWeapon = 10f;

	public float autoFaceThresholdDistanceForThrowingWeapon = 25f;

	public float stoppingDistance = 5f;

	public float stoppingDistanceWithWeapon = 7f;

	public float stoppingDistanceWithThrowingWeapon = 25f;

	public float deathSinkDelay = 5f;

	public bool givesItemWhenDie;

	private bool isCounterAttackPeriod;

	private bool isCounterAttackAllowed = true;

	public string attackPrefix = string.Empty;

	private bool isPlayingFightNoise;

	public string animSuffix = string.Empty;

	public bool isRun;

	public float runSpeedMultiple = 1.5f;

	protected string lastWalkAnim;

	private int collisionRequests;

	private string lastCounterAttackPlayed;

	private float zombieDroolsLinedUpForSeconds;

	[NonSerialized]
	public bool isZombiefied;

	private string originalAttackPrefix;

	private int originalVoiceType;

	private Mesh originalMesh;

	public AttackComponent CurrentEnemy
	{
		get
		{
			return currentEnemy;
		}
	}

	public int LunchMoney
	{
		get
		{
			return lunchMoney;
		}
	}

	protected virtual void Awake()
	{
		myTransform = base.transform;
		life = maxLife;
		energy = maxEnergy;
		initCharacter();
		pickupOnDesignTimeItems();
		putOnDesignTimeEquipments();
	}

	protected virtual void Update()
	{
		doSnapping();
	}

	public void doSnapping()
	{
		faceVectorStep();
		moveToVectorStep();
	}

	protected virtual void loadCharacterAnims()
	{
		Animation gameAnimations = VNLUtil.getInstance().getGameAnimations();
		foreach (AnimationState item in gameAnimations)
		{
			if ((bool)anim.GetClip(item.name))
			{
				anim.RemoveClip(item.name);
			}
		}
		foreach (AnimationState item2 in gameAnimations)
		{
			anim.AddClip(item2.clip, item2.name);
		}
	}

	protected virtual void initCharacter()
	{
		currentAttackLevel = 1;
		originalColliderSize = GetComponent<BoxCollider>().size;
		originalColliderY = GetComponent<BoxCollider>().center;
		Animation[] componentsInChildren = myTransform.GetComponentsInChildren<Animation>();
		Animation[] array = componentsInChildren;
		foreach (Animation animation in array)
		{
			if (animation.transform.Find("Armature") != null)
			{
				character = animation.transform;
			}
		}
		anim = character.GetComponent<Animation>();
		loadCharacterAnims();
		toggleAttackColliders(false);
		walkAnim = ANIM_WALK + walkAnims[UnityEngine.Random.Range(0, walkAnims.Length)];
		runAnim = ANIM_RUN + runAnims[UnityEngine.Random.Range(0, runAnims.Length)];
		weaponWalkAnim = ANIM_WALK + weaponWalkAnims[UnityEngine.Random.Range(0, weaponWalkAnims.Length)];
		foreach (AnimationState item in anim)
		{
			if (item.name.StartsWith(ANIM_ATTACK) || item.name.StartsWith(ANIM_COUNTER_ATTACK))
			{
				item.wrapMode = WrapMode.Once;
				item.layer = 1;
			}
			if (item.name.StartsWith(ANIM_WEAPON))
			{
				item.wrapMode = WrapMode.Once;
				item.layer = 1;
			}
			if (item.name.Equals(ANIM_WEAPON_BLOCK) || item.name.Equals(ANIM_BLOCK))
			{
				item.wrapMode = WrapMode.Loop;
				item.layer = 0;
			}
			if (item.name.StartsWith(ANIM_HURT))
			{
				item.wrapMode = WrapMode.Once;
				item.layer = 2;
			}
			if (item.name.StartsWith(ANIM_IDLE))
			{
				item.wrapMode = WrapMode.Loop;
			}
			if (item.name.StartsWith(ANIM_WALK) || item.name.StartsWith(ANIM_RUN))
			{
				item.wrapMode = WrapMode.Loop;
			}
			if (item.name.StartsWith(ANIM_DEATH))
			{
				item.wrapMode = WrapMode.ClampForever;
				item.layer = 3;
			}
			if (item.name.StartsWith(ANIM_THROW))
			{
				item.wrapMode = WrapMode.Once;
				item.layer = 1;
			}
			if (item.name.Equals(ANIM_HURT + "99"))
			{
				item.wrapMode = WrapMode.Once;
				item.layer = 0;
			}
		}
		Rigidbody[] componentsInChildren2 = myTransform.GetComponentsInChildren<Rigidbody>();
		Rigidbody[] array2 = componentsInChildren2;
		foreach (Rigidbody rigidbody in array2)
		{
			if (rigidbody.tag.Equals(myLimbTag))
			{
				rigidbody.isKinematic = true;
			}
		}
	}

	public void stopAnimation()
	{
		anim.Stop();
	}

	public virtual void addLunchMoney(int newValue)
	{
		lunchMoney += newValue;
	}

	public void lookAt(Transform target)
	{
		lookAt(target.position);
	}

	public void lookAt(Vector3 target)
	{
		rotationToLookAt = Quaternion.LookRotation(target - myTransform.position);
		lookAtCount = 10;
	}

	private void faceVectorStep()
	{
		if (lookAtCount > 0)
		{
			lookAtCount--;
			Quaternion rotation = Quaternion.Slerp(myTransform.rotation, rotationToLookAt, Time.deltaTime * 10f);
			rotation.x = 0f;
			rotation.z = 0f;
			myTransform.rotation = rotation;
		}
	}

	public void moveTo(Transform target)
	{
		float num = 0f;
		if (!hasThrowingWeapon())
		{
			num = ((!(currentWeapon == null)) ? stoppingDistanceWithWeapon : stoppingDistance);
		}
		Vector3 vector = Vector3.Normalize(myTransform.position - target.position);
		targetToSnapTo = target.position + vector * num;
		targetToSnapTo.y = myTransform.position.y;
		float num2 = Vector3.Distance(target.position, myTransform.position);
		float num3 = Vector3.Distance(targetToSnapTo, target.position);
		bool flag = false;
		if (num3 > num2)
		{
			Vector3 vector2 = myTransform.TransformDirection(Vector3.back);
			Vector3 position = base.transform.position;
			position -= vector * 5f;
			vector.y = 0f;
			if (Physics.Raycast(position, vector, 20f, base.gameObject.layer))
			{
				flag = true;
			}
		}
		if (!flag)
		{
			snapToCount = 10;
		}
	}

	private void moveToVectorStep()
	{
		if (snapToCount > 0)
		{
			snapToCount--;
			Vector3 position = Vector3.Lerp(myTransform.position, targetToSnapTo, Time.deltaTime * 10f);
			myTransform.position = position;
		}
	}

	public bool isAlreadyThrowing()
	{
		return throwTarget != Vector3.zero;
	}

	private void putOnDesignTimeEquipments()
	{
		if (currentWeapon != null)
		{
			currentWeapon.equipPrefab(this);
		}
		List<EquipmentItem> list = equippedItems;
		equippedItems = new List<EquipmentItem>();
		for (int i = 0; i < list.Count; i++)
		{
			EquipmentItem equipmentItem = list[i];
			equipmentItem.equipPrefab(this);
		}
	}

	private void pickupOnDesignTimeItems()
	{
		List<BadNerdItem> list = itemList;
		itemList = new List<BadNerdItem>();
		for (int i = 0; i < list.Count; i++)
		{
			BadNerdItem badNerdItem = list[i];
			badNerdItem.pickupPrefab(this);
		}
	}

	private void FixedUpdate()
	{
		if (energy < maxEnergy)
		{
			setEnergy(energy + recoveryRate);
		}
	}

	public virtual void setEnergy(float newEnergy)
	{
		energy = newEnergy;
		if (energy < 0f)
		{
			energy = 0f;
		}
		else if (energy > maxEnergy)
		{
			energy = maxEnergy;
		}
		if (onEnergyChanged != null)
		{
			onEnergyChanged();
		}
	}

	public void onThrow(Vector3 target)
	{
		if (currentWeapon != null && currentWeapon.isThrowable)
		{
			shrinkBodyCollider(false);
			if (!isAlreadyThrowing())
			{
				throwTarget = target;
				anim.CrossFade(ANIM_THROW + throwAnims[UnityEngine.Random.Range(0, throwAnims.Length)]);
				lookAt(target);
				Quaternion rotation = myTransform.rotation;
				rotation.x = 0f;
				rotation.z = 0f;
				myTransform.rotation = rotation;
				StartCoroutine("throwWeapon");
			}
		}
	}

	private IEnumerator throwWeapon()
	{
		yield return new WaitForSeconds(0.45f);
		if (currentWeapon != null)
		{
			currentWeapon.throwAt(throwTarget);
		}
		yield return new WaitForSeconds(1f);
		throwTarget = Vector3.zero;
	}

	public virtual void onAttackPressed(int tapCount)
	{
		forceOnAttackPressed(tapCount);
	}

	public void forceOnAttackPressed(int tapCount)
	{
		if (!isDead && !isHurting())
		{
			snapToEnemy();
			if (currentWeapon != null)
			{
				currentWeapon.doAttack(tapCount);
			}
			else
			{
				doAttack(tapCount);
			}
		}
	}

	public virtual void onBlockPressed()
	{
		if (!isDead && !isRun)
		{
			snapToEnemy();
			doBlock();
		}
	}

	public virtual void onBlockReleased()
	{
		if (isDead)
		{
			return;
		}
		onWalkPressed(Vector3.one);
		if (!isCounterAttackPeriod && isCounterAttackAllowed)
		{
			isCounterAttackPeriod = true;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				isCounterAttackPeriod = false;
			}, 0.5f);
		}
		if (isCounterAttackAllowed)
		{
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				isCounterAttackAllowed = true;
			}, 2.25f);
		}
		isCounterAttackAllowed = false;
	}

	public void doBlock()
	{
		string text = ((!(currentWeapon == null)) ? ANIM_WEAPON_BLOCK : ANIM_BLOCK);
		if (!anim.IsPlaying(text))
		{
			AnimationClip clip = anim.GetClip(text);
			if ((bool)clip)
			{
				anim.CrossFade(text);
			}
		}
	}

	public bool isBlocking()
	{
		return anim.IsPlaying(ANIM_BLOCK) || anim.IsPlaying(ANIM_WEAPON_BLOCK);
	}

	private void snapToEnemy()
	{
		AttackComponent enemyToLookAtBeforeAttacking = getEnemyToLookAtBeforeAttacking();
		if (enemyToLookAtBeforeAttacking == null || enemyToLookAtBeforeAttacking.isDead)
		{
			return;
		}
		float num = Vector3.Distance(myTransform.position, enemyToLookAtBeforeAttacking.transform.position);
		bool flag = false;
		float num2 = 0f;
		if (!hasThrowingWeapon())
		{
			num2 = ((!(currentWeapon != null)) ? autoFaceThresholdDistance : autoFaceThresholdDistanceForWeapon);
		}
		else
		{
			flag = true;
			num2 = autoFaceThresholdDistanceForThrowingWeapon;
		}
		if (num < num2)
		{
			setCurrentEnemy(enemyToLookAtBeforeAttacking);
			lookAt(enemyToLookAtBeforeAttacking.transform);
			if (!flag)
			{
				moveTo(enemyToLookAtBeforeAttacking.transform);
			}
		}
		else if (flag)
		{
			setCurrentEnemy(null);
		}
	}

	protected bool hasThrowingWeapon()
	{
		return currentWeapon != null && typeof(ThrowingWeapon).IsAssignableFrom(currentWeapon.GetType());
	}

	protected virtual void indicateLowEnergy()
	{
	}

	protected abstract AttackComponent getEnemyToLookAtBeforeAttacking();

	public void doAttack(int tapCount)
	{
		int num = filterAttackLevelBasedOnEnergy(getAttackLevel(tapCount));
		if (num == 0)
		{
			if (!isAttacking())
			{
				indicateLowEnergy();
			}
			return;
		}
		bool flag = false;
		if (currentEnemy != null && currentEnemy.isDowned() && lookAtCount > 0)
		{
			flag = true;
		}
		string text = ((!(currentWeapon == null) && !isZombiefied) ? ANIM_WEAPON : ANIM_ATTACK);
		text += attackPrefix;
		string text2 = null;
		if (flag)
		{
			if (anim.IsPlaying(lastAttack))
			{
				return;
			}
			text2 = text + beatDownAnims[UnityEngine.Random.Range(0, beatDownAnims.Length)];
		}
		else
		{
			text2 = text + tapCount + num;
		}
		bool flag2 = true;
		if (anim.IsPlaying(text2))
		{
			if (tapCount != 1)
			{
				return;
			}
			flag2 = false;
		}
		AnimationClip clip = anim.GetClip(text2);
		if ((bool)clip)
		{
			currentAttackLevel = num;
			lastAttack = text2;
			lastAttackTapCount = tapCount;
			if (flag2)
			{
				anim.CrossFade(text2);
			}
			else
			{
				anim.CrossFadeQueued(text2);
			}
			StartCoroutine(playFightSoundsToFightAnim());
			if (flag)
			{
				StartCoroutine(enableAttackCollidersWithDelay(0.4f));
			}
			else
			{
				StartCoroutine(enableAttackCollidersWithDelay(tapCount, num));
			}
			float num2 = 5f;
			setEnergy(energy - num2);
		}
	}

	public void initAttackAndArmorColliders()
	{
		StartCoroutine(toggleAttackAndArmorCollidersWithDelay());
	}

	private IEnumerator toggleAttackAndArmorCollidersWithDelay()
	{
		yield return new WaitForSeconds(0.1f);
		toggleAttackColliders(true);
		toggleAttackColliders("armor", true);
		yield return new WaitForSeconds(0.1f);
		toggleAttackColliders(false);
		toggleAttackColliders("armor", false);
	}

	private IEnumerator playFightSoundsToFightAnim()
	{
		if (isPlayingFightNoise)
		{
			yield break;
		}
		isPlayingFightNoise = true;
		yield return new WaitForSeconds(0.2f);
		while (anim.IsPlaying(lastAttack))
		{
			playFightSound();
			float delay = UnityEngine.Random.Range(0.4f, 0.8f);
			yield return new WaitForSeconds(delay);
			if (lastAttackTapCount == 1)
			{
				break;
			}
		}
		isPlayingFightNoise = false;
	}

	protected virtual void playFightSound()
	{
		if (voiceType == 0)
		{
			VNLUtil.getInstance().playKidFightSound();
		}
		else if (voiceType == 1)
		{
			VNLUtil.getInstance().playZombieFightSound();
		}
		else if (voiceType == 99)
		{
			VNLUtil.getInstance().playNerdFightSound();
		}
	}

	private int filterAttackLevelBasedOnEnergy(int attackLevel)
	{
		float num = energy / maxEnergy;
		float f = (float)attackLevel * num * 1.5f;
		int num2 = Mathf.RoundToInt(f);
		if (num2 > attackLevel)
		{
			num2 = attackLevel;
		}
		if (num2 == 0 && energy > 7f)
		{
			num2 = 1;
		}
		return num2;
	}

	protected virtual int getAttackLevel(int tapCount)
	{
		if ("Z".Equals(attackPrefix))
		{
			return 1;
		}
		int result = 0;
		if (tapCount > 3)
		{
			tapCount = UnityEngine.Random.Range(1, 4);
		}
		if (currentWeapon == null)
		{
			switch (tapCount)
			{
			case 1:
				result = attack1Level;
				break;
			case 2:
				result = attack2Level;
				break;
			case 3:
				result = attack3Level;
				break;
			}
		}
		else
		{
			switch (tapCount)
			{
			case 1:
				result = weaponAttack1Level;
				break;
			case 2:
				result = weaponAttack2Level;
				break;
			case 3:
				result = weaponAttack3Level;
				break;
			}
		}
		return result;
	}

	private void makeAttackerLookAtMe(AttackComponent enemy)
	{
		if (!isDead && enemy != null)
		{
			enemy.lookAt(base.transform);
		}
	}

	public virtual void onWalkPressed(Vector3 speed)
	{
		if (isDead)
		{
			return;
		}
		shrinkBodyCollider(false);
		string text = (isRun ? runAnim : ((!(currentWeapon == null)) ? weaponWalkAnim : walkAnim));
		text += animSuffix;
		anim.CrossFade(text);
		lastWalkAnim = text;
		anim[text].speed = getSpeed(speed);
		if (!anim.IsPlaying(lastAttack))
		{
			toggleAttackColliders(false);
		}
		idleTime = 0;
		touchedCountroller = true;
		if (isRun)
		{
			setEnergy(energy - 0.15f);
			if (energy < 7f)
			{
				isRun = false;
			}
		}
	}

	protected float getSpeed(Vector3 speed)
	{
		float num = Mathf.Abs(speed.x) + Mathf.Abs(speed.z);
		if (maxSpeed == 0f)
		{
			maxSpeed = getMaxSpeed();
		}
		return num / maxSpeed;
	}

	protected abstract float getMaxSpeed();

	private IEnumerator enableAttackCollidersWithDelay(float delay)
	{
		collisionRequests++;
		yield return new WaitForSeconds(delay);
		if (collisionRequests == 1)
		{
			toggleAttackColliders(true);
		}
		collisionRequests--;
	}

	private IEnumerator enableAttackCollidersWithDelay(int tapCount, int level)
	{
		level--;
		float delay = 0f;
		if (currentWeapon == null)
		{
			switch (tapCount)
			{
			case 1:
				delay = attack1Delays[level];
				break;
			case 2:
				delay = attack2Delays[level];
				break;
			}
			if (tapCount == 3)
			{
				delay = attack3Delays[level];
			}
		}
		else
		{
			switch (tapCount)
			{
			case 1:
				delay = weaponAttack1Delays[level];
				break;
			case 2:
				delay = weaponAttack2Delays[level];
				break;
			}
			if (tapCount == 3)
			{
				delay = weaponAttack3Delays[level];
			}
		}
		return enableAttackCollidersWithDelay(delay);
	}

	protected virtual void toggleAttackColliders(bool on)
	{
		toggleAttackColliders(myLimbTag, on);
	}

	protected virtual void toggleAttackColliders(string theTag, bool on)
	{
		if (!isDead)
		{
			if (collidersOn == on || (!on && anim.IsPlaying(lastAttack)))
			{
				return;
			}
		}
		else
		{
			on = false;
		}
		collidersOn = on;
		GameObject[] array = GameObject.FindGameObjectsWithTag(theTag);
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (gameObject.transform.IsChildOf(myTransform) && (bool)gameObject.GetComponent<Collider>())
			{
				gameObject.GetComponent<Collider>().enabled = on;
				Weapon component = gameObject.GetComponent<Weapon>();
				if (component != null)
				{
					component.toggleCollider(on);
				}
			}
		}
	}

	public virtual void onStopWalking()
	{
		shrinkBodyCollider(false);
		toggleAttackColliders(false);
		detectIdleChangeTime();
	}

	protected virtual bool isEnemyInAttackDistance()
	{
		if (CurrentEnemy == null)
		{
			return false;
		}
		float num = Vector3.Distance(base.transform.position, CurrentEnemy.transform.position);
		return num <= 5.5f;
	}

	protected bool detectIdleChangeTime()
	{
		if (isBlocking())
		{
			return false;
		}
		idleTime++;
		if (idleTime % idleInterval == 0 || idleTime == 1)
		{
			animateIdle();
			idleInterval = UnityEngine.Random.Range(100, 1000);
			return true;
		}
		return false;
	}

	public void animateIdle()
	{
		int[] array = null;
		array = ((!isEnemyInAttackDistance()) ? ((!(currentWeapon == null)) ? idleWeaponAnims : idleAnims) : ((!(currentWeapon == null)) ? aggressiveWeaponAnims : aggressiveAnims));
		int num = array[UnityEngine.Random.Range(0, array.Length)];
		string text = ANIM_IDLE + num + animSuffix;
		anim.CrossFade(text);
	}

	protected virtual void onHurt(AttackComponent enemy, Weapon weapon)
	{
		if (isDead)
		{
			return;
		}
		makeAttackerLookAtMe(enemy);
		enemy.setCurrentEnemy(this);
		setCurrentEnemy(enemy);
		toggleAttackColliders(false);
		float num = 0f;
		if (weapon == null)
		{
			num = (float)enemy.currentAttackLevel * enemy.strength;
		}
		else
		{
			if (weapon.life == 0f)
			{
				return;
			}
			num = (float)enemy.currentAttackLevel * weapon.damage;
			weapon.doDamage();
		}
		num *= 1f - getArmorDamageReductionAndApplyDamageToArmor(num);
		if (enemy.isZombiefied)
		{
			num *= 5f;
		}
		bool flag = isBlocking();
		if (flag)
		{
			num *= 0.25f;
		}
		if (isZombiefied)
		{
			num *= 0.1f;
		}
		if (lastCounterAttackPlayed != null && anim.IsPlaying(lastCounterAttackPlayed))
		{
			return;
		}
		if (isCounterAttackPeriod && counterAttackLevel > 0)
		{
			doCounterAttack();
		}
		else
		{
			setLife(life - num);
			if (CurrentEnemy.lastAttack != null && CurrentEnemy.lastAttack.StartsWith(ANIM_COUNTER_ATTACK))
			{
				setEnergy(1f);
			}
			else
			{
				setEnergy(energy - ((!flag) ? 1.5f : 0.5f));
			}
			if (life <= 0f)
			{
				doDie();
			}
			else
			{
				animateHurt();
			}
			if (!isDowned())
			{
				lookAt(enemy.transform);
			}
		}
		playPunchSound();
	}

	public virtual void doCounterAttack()
	{
		string text = ANIM_COUNTER_ATTACK + counterAttackLevel;
		AnimationClip clip = anim.GetClip(text);
		if ((bool)clip)
		{
			lastCounterAttackPlayed = text;
			currentAttackLevel = counterAttackLevel;
			lastAttack = text;
			anim.CrossFade(text);
			StartCoroutine(playFightSoundsToFightAnim());
			StartCoroutine(enableAttackCollidersWithDelay(0.4f));
			float num = 5f;
			setEnergy(energy - num);
		}
	}

	public virtual void doDie()
	{
		if (!isDead)
		{
			setLife(0f);
			animateDeath();
			die();
		}
	}

	protected virtual void playPunchSound()
	{
		VNLUtil.getInstance().playPunchSound();
	}

	protected virtual void animateDeath()
	{
		string text = ANIM_DEATH + getDeathType();
		AnimationClip clip = anim.GetClip(text);
		if ((bool)clip)
		{
			anim.CrossFade(text);
		}
	}

	public void beStunned()
	{
		animateHurt();
	}

	protected virtual void animateHurt()
	{
		int num = 0;
		num = getHurtType();
		string text = ANIM_HURT + num + animSuffix;
		AnimationClip clip = anim.GetClip(text);
		if ((bool)clip)
		{
			anim.CrossFade(text);
			lastHurt = text;
		}
	}

	public void animateHurt(string hurt)
	{
		AnimationClip clip = anim.GetClip(hurt);
		if ((bool)clip)
		{
			anim.CrossFade(hurt);
			lastHurt = hurt;
		}
	}

	public virtual void setLife(float newLife)
	{
		life = newLife;
		if (life > maxLife)
		{
			life = maxLife;
		}
		else if (life < 0f)
		{
			life = 0f;
		}
		if (onHealthChanged != null)
		{
			onHealthChanged();
		}
	}

	protected int getDeathType()
	{
		int num = UnityEngine.Random.Range(0, deathAnims.Length);
		return deathAnims[num];
	}

	protected virtual int getHurtType()
	{
		int num = 0;
		int num2 = UnityEngine.Random.Range(1, 101);
		float num3 = life / maxLife;
		float num4 = energy / maxEnergy;
		bool flag = num4 < 0.4f;
		num = ((CurrentEnemy != null && CurrentEnemy.lastAttack != null && CurrentEnemy.lastAttack.StartsWith(ANIM_COUNTER_ATTACK)) ? 999 : (isBlocking() ? 4 : ((!flag) ? UnityEngine.Random.Range(1, 7) : ((num3 > 0.4f && num2 < knockDownProbabiliy) ? 14 : ((!(num3 <= 0.4f) || !((float)num2 < (float)knockDownProbabiliy * 2f)) ? UnityEngine.Random.Range(1, 7) : 14)))));
		if (flag)
		{
			if (anim.IsPlaying(ANIM_HURT + "14"))
			{
				num = 15;
			}
			else if (anim.IsPlaying(ANIM_HURT + "15"))
			{
				num = 16;
			}
			else if (anim.IsPlaying(ANIM_HURT + "16"))
			{
				num = 15;
			}
		}
		if (num == 14 || num == 15 || num == 16)
		{
			shrinkBodyCollider(true);
		}
		else
		{
			isDown = false;
		}
		return num;
	}

	protected virtual void die()
	{
		if (!isDead)
		{
			isDead = true;
			toggleAttackColliders(false);
			dieDetachItems();
			base.GetComponent<Collider>().enabled = false;
			base.GetComponent<Rigidbody>().useGravity = false;
			if (givesItemWhenDie)
			{
				dropRandomItem();
			}
			StartCoroutine("doPostDeath");
		}
	}

	protected virtual void dropRandomItem()
	{
		VNLUtil.getInstance().createRandomItem(this, getPercentDropRate(), chanceOfDroppedItemDividedBy, maxValueOfDroppedItem);
	}

	protected virtual int getPercentDropRate()
	{
		return 0;
	}

	protected IEnumerator doPostDeath()
	{
		Debug.Log(base.name + " doPostDeath called");
		if (onDead != null)
		{
			yield return new WaitForSeconds(1f);
			if (onDead != null)
			{
				Debug.Log(base.name + " died, going to do onDead()");
				onDead();
			}
		}
		else
		{
			Debug.Log(base.name + " has no onDead");
		}
		yield return new WaitForSeconds(deathSinkDelay);
		base.GetComponent<Rigidbody>().useGravity = true;
		yield return new WaitForSeconds(3f);
		dieDoDestroy();
	}

	protected virtual void dieDetachItems()
	{
		while (itemList.Count > 0)
		{
			itemList[0].detachItem(true);
		}
	}

	protected virtual void dieDoDestroy()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public bool isHurting()
	{
		if (isDead)
		{
			return true;
		}
		return anim.IsPlaying(lastHurt);
	}

	public bool isDowned()
	{
		return isDown;
	}

	public bool isAttacking()
	{
		return anim.IsPlaying(lastAttack);
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (isDead)
		{
			return;
		}
		GameObject gameObject = collision.gameObject;
		if (gameObject.CompareTag(attackerLimbTag))
		{
			AttackComponent attackComponent = null;
			WeaponChild component = gameObject.GetComponent<WeaponChild>();
			attackComponent = ((!(component != null)) ? gameObject.transform.root.GetComponentInChildren<AttackComponent>() : component.getOwner());
			if (!(attackComponent.currentWeapon != null) || !(gameObject.GetComponent<Weapon>() == null) || !(gameObject.GetComponent<WeaponChild>() == null))
			{
				onHurt(attackComponent, attackComponent.currentWeapon);
			}
			return;
		}
		if (gameObject.CompareTag("weapon"))
		{
			Weapon weapon = gameObject.GetComponent<Weapon>();
			if (weapon == null)
			{
				WeaponChild component2 = gameObject.GetComponent<WeaponChild>();
				if (component2 != null)
				{
					weapon = component2.Weapon;
				}
			}
			if (weapon != null && VNLUtil.isMovingFast(weapon.gameObject) && weapon.owner != null && weapon.owner != this)
			{
				onHurt(weapon.owner, weapon);
				return;
			}
		}
		if (!VNLUtil.isMovingFast(gameObject) && gameObject.transform.parent == null)
		{
			pickupItem(gameObject);
		}
	}

	public virtual void pickupItem(GameObject go)
	{
		BadNerdItem component = go.GetComponent<BadNerdItem>();
		if (component != null)
		{
			component.bePickedUp(this);
		}
	}

	private void shrinkBodyCollider(bool shrink)
	{
		if (shrink)
		{
			Vector3 size = originalColliderSize;
			size.y /= 3f;
			Vector3 center = originalColliderY;
			center.y = originalColliderY.y - originalColliderSize.y / 2f + size.y / 2f;
			BoxCollider component = GetComponent<BoxCollider>();
			component.center = center;
			component.size = size;
			isDown = true;
		}
		else if (!isHurting())
		{
			BoxCollider component2 = GetComponent<BoxCollider>();
			component2.center = originalColliderY;
			component2.size = originalColliderSize;
			isDown = false;
		}
	}

	public virtual void setCurrentEnemy(AttackComponent attacker)
	{
		currentEnemy = attacker;
	}

	private float getArmorDamageReductionAndApplyDamageToArmor(float damage)
	{
		float num = 0f;
		int num2 = 0;
		Armor[] array = new Armor[equippedItems.Count];
		for (int i = 0; i < equippedItems.Count; i++)
		{
			EquipmentItem equipmentItem = equippedItems[i];
			if (typeof(Armor).IsAssignableFrom(equipmentItem.GetType()))
			{
				Armor armor = (array[num2] = (Armor)equipmentItem);
				num2++;
				num += armor.getDamageReduction();
			}
		}
		if (num > 0.9f)
		{
			num = 0.9f;
		}
		if (num2 > 0)
		{
			int num3 = UnityEngine.Random.Range(0, num2);
			Armor armor2 = array[num3];
			armor2.doDamage(damage);
		}
		return num;
	}

	public float getTotalItemWeight(bool ignoreWornItems)
	{
		float num = 0f;
		for (int i = 0; i < itemList.Count; i++)
		{
			BadNerdItem badNerdItem = itemList[i];
			if (!ignoreWornItems || !(getUserEquipmentOfThisType(badNerdItem.GetType()) != null))
			{
				num += badNerdItem.getWeight();
			}
		}
		return num;
	}

	public BadNerdItem getUserItemOfThisType(Type type)
	{
		BadNerdItem result = null;
		for (int i = 0; i < itemList.Count; i++)
		{
			BadNerdItem badNerdItem = itemList[i];
			if (type.IsAssignableFrom(badNerdItem.GetType()))
			{
				result = badNerdItem;
				break;
			}
		}
		return result;
	}

	public EquipmentItem getUserEquipmentOfThisType(Type type)
	{
		EquipmentItem result = null;
		for (int i = 0; i < equippedItems.Count; i++)
		{
			EquipmentItem equipmentItem = equippedItems[i];
			if (type.IsAssignableFrom(equipmentItem.GetType()) || equipmentItem.GetType().IsAssignableFrom(type))
			{
				result = equipmentItem;
				break;
			}
		}
		return result;
	}

	protected virtual void OnDestroy()
	{
		if (updateData != null)
		{
			updateData(this);
		}
	}

	public void toggleHelmetMesh(bool hasHelmet)
	{
		if (!(helmetMesh == null) && !(normalMesh == null))
		{
			base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = ((!hasHelmet) ? normalMesh : helmetMesh);
			GameObject.Find("badNerdGlasses").GetComponentInChildren<MeshFilter>().sharedMesh = ((!hasHelmet) ? normalGlassesMesh : helmetGlassesMesh);
		}
	}

	public virtual void becomeZombie(float seconds)
	{
		Debug.Log("called becomeZombie");
		if (isZombiefied)
		{
			zombieDroolsLinedUpForSeconds += seconds;
			Debug.Log("already zombie, extend: " + zombieDroolsLinedUpForSeconds);
			return;
		}
		isZombiefied = true;
		Debug.Log("zombie effect activated for " + seconds + "s");
		zombieDroolsLinedUpForSeconds = seconds;
		StartCoroutine(zombieDelay());
		startZombieEffect();
	}

	protected virtual void startZombieEffect()
	{
		originalAttackPrefix = attackPrefix;
		attackPrefix = "Z";
		originalVoiceType = voiceType;
		voiceType = 1;
		becomeAZombieSkin();
	}

	private IEnumerator zombieDelay()
	{
		while (zombieDroolsLinedUpForSeconds > 0f)
		{
			Debug.Log("waiting for zombie effect to wear out in " + zombieDroolsLinedUpForSeconds + "s");
			float seconds = zombieDroolsLinedUpForSeconds;
			zombieDroolsLinedUpForSeconds = 0f;
			yield return new WaitForSeconds(seconds);
			Debug.Log("zombie effect wearing out");
		}
		isZombiefied = false;
		Debug.Log("no effects lined up.  Ending effect...");
		endZombieEffect();
	}

	protected virtual void endZombieEffect()
	{
		attackPrefix = originalAttackPrefix;
		voiceType = originalVoiceType;
		base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = originalMesh;
	}

	public void becomeAGeekSkin()
	{
		if (originalMesh == null)
		{
			originalMesh = base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
		}
		base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = geekMeshes[UnityEngine.Random.Range(0, geekMeshes.Length)];
	}

	public void becomeAZombieSkin()
	{
		if (originalMesh == null)
		{
			originalMesh = base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
		}
		base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = VNLUtil.getInstance().zombieMeshes[UnityEngine.Random.Range(0, VNLUtil.getInstance().zombieMeshes.Length)];
	}

	public void becomeBadNerdSkin()
	{
		if (originalMesh == null)
		{
			originalMesh = base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
		}
		base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = normalMesh;
	}
}
