using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
	public List<string> randomStuffToSay = new List<string>();

	public List<Quest> quests;

	private PlayerAttackComponent player;

	protected Quest quest;

	public bool destroyIfDone;

	public bool destroyIfDoneOnlyWhenSceneLoads;

	public bool destroyIfAcceptLastQuest;

	public bool clearTheRoom;

	public Quest prerequisite;

	private Transform myTransform;

	private Transform playerTransform;

	private bool lookAtPlayer;

	private bool detecting = true;

	private VNLUtil.NoNameMethod onChangeBehavior;

	public VNLUtil.NoNameMethod onDestroyBehavior;

	public int requireCompletedNumOfQuests;

	private GameObject exclaimationMarkHolder;

	public int episode;

	public bool ignoreOnMac;

	public bool ignorePreReqOnMac;

	public string[] idleAnims = new string[1] { "Idle5" };

	private void Awake()
	{
		if (false && ignorePreReqOnMac)
		{
			prerequisite = null;
		}
		myTransform = base.transform;
		if (exclaimationMarkHolder == null)
		{
			exclaimationMarkHolder = VNLUtil.getInstance().getExclaimationMark(myTransform);
			exclaimationMarkHolder.GetComponentInChildren<Animation>().enabled = false;
		}
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackComponent>();
			playerTransform = player.transform;
			if (GameStart.enableNetworking)
			{
				VNLUtil.toggleAll(myTransform, false);
			}
			else if (!VNLUtil.getInstance<QuestInitializer>().ignorePreRequisites && !isMetPrerequisite())
			{
				VNLUtil.toggleAll(myTransform, false);
			}
			else
			{
				init(true);
			}
		});
	}

	public bool isMetPrerequisite()
	{
		bool flag = prerequisite == null || player.isQuestComplete(prerequisite);
		if (requireCompletedNumOfQuests == 0)
		{
			return flag;
		}
		return flag && player.getCompletedQuestCount() >= requireCompletedNumOfQuests;
	}

	public virtual void init(bool fromSceneLoad)
	{
		determineQuestGiverState(fromSceneLoad);
		detectWhichAnimToPlay();
		onChangeBehavior = delegate
		{
			determineQuestGiverState(false);
			detectWhichAnimToPlay();
		};
		PlayerAttackComponent playerAttackComponent = player;
		playerAttackComponent.onQuestStatusChange = (VNLUtil.NoNameMethod)Delegate.Combine(playerAttackComponent.onQuestStatusChange, onChangeBehavior);
	}

	private void determineQuestGiverState(bool fromSceneLoad)
	{
		bool flag = false;
		bool flag2 = false;
		if (flag && ignoreOnMac)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			flag2 = true;
		}
		else if (VNLUtil.getInstance().episode > episode)
		{
			Debug.Log(base.name + " destroying due to different episode");
			UnityEngine.Object.Destroy(base.gameObject);
			flag2 = true;
		}
		else if (destroyIfDone && hasNoQuestsLeft())
		{
			Debug.Log(base.name + " destroyIfDone");
			UnityEngine.Object.Destroy(base.gameObject);
			flag2 = true;
		}
		else if (fromSceneLoad && destroyIfDoneOnlyWhenSceneLoads && hasNoQuestsLeft())
		{
			Debug.Log(base.name + " destroyIfDoneOnlyWhenSceneLoads hasNoQuestsLeft");
			UnityEngine.Object.Destroy(base.gameObject);
			flag2 = true;
		}
		else if (destroyIfAcceptLastQuest && getNumberOfQuestsLeftToComplete() == 1 && quests[quests.Count - 1].Equals(player.CurrentQuest))
		{
			Debug.Log(base.name + " destroyIfAcceptLastQuest");
			UnityEngine.Object.Destroy(base.gameObject);
			flag2 = true;
		}
		else if (destroyIfAcceptLastQuest && hasNoQuestsLeft())
		{
			Debug.Log(base.name + " destroyIfAcceptLastQuest hasNoQuestsLeft");
			UnityEngine.Object.Destroy(base.gameObject);
			flag2 = true;
		}
		else if (randomStuffToSay.Count == 0)
		{
			randomStuffToSay.Add("rand_1");
			randomStuffToSay.Add("rand_2");
			randomStuffToSay.Add("rand_3");
			randomStuffToSay.Add("rand_4");
			randomStuffToSay.Add("rand_5");
		}
		if (clearTheRoom && !flag2 && !hasNoQuestsLeft())
		{
			GameObject.Find(Application.loadedLevelName + "SpawnManager").GetComponent<SpawnManager>().unSpawn();
		}
	}

	private void OnDestroy()
	{
		PlayerAttackComponent playerAttackComponent = player;
		playerAttackComponent.onQuestStatusChange = (VNLUtil.NoNameMethod)Delegate.Remove(playerAttackComponent.onQuestStatusChange, onChangeBehavior);
		if (onDestroyBehavior != null)
		{
			onDestroyBehavior();
		}
	}

	public bool hasNoQuestsLeft()
	{
		return getNumberOfQuestsLeftToComplete() == 0;
	}

	private int getNumberOfQuestsLeftToComplete()
	{
		int num = quests.Count;
		foreach (Quest quest in quests)
		{
			if (player.isQuestComplete(quest))
			{
				num--;
			}
		}
		return num;
	}

	private void detectWhichAnimToPlay()
	{
		if (player.CurrentQuest == null && getNumberOfQuestsLeftToComplete() > 0)
		{
			base.GetComponent<Animation>().CrossFade("Waving1");
			VNLUtil.toggleAll(exclaimationMarkHolder.transform, true);
			lookAtPlayer = true;
		}
		else
		{
			string text = idleAnims[UnityEngine.Random.Range(0, idleAnims.Length)];
			base.GetComponent<Animation>().CrossFade(text);
			VNLUtil.toggleAll(exclaimationMarkHolder.transform, false);
			lookAtPlayer = false;
		}
	}

	private void Update()
	{
		if (lookAtPlayer)
		{
			myTransform.LookAt(playerTransform);
			Quaternion rotation = myTransform.rotation;
			rotation.x = 0f;
			rotation.z = 0f;
			myTransform.transform.rotation = rotation;
		}
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (!detecting)
		{
			return;
		}
		GameObject gameObject = collision.gameObject;
		if (player != null && gameObject == player.gameObject)
		{
			show();
			detecting = false;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				detecting = true;
			}, 3f);
		}
	}

	public void show()
	{
		quest = null;
		foreach (Quest quest in quests)
		{
			if (!player.isQuestComplete(quest))
			{
				this.quest = quest;
				break;
			}
		}
		if (player.CurrentQuest != null && player.CurrentQuest.name.Equals(this.quest.name) && player.CurrentQuest.inProgressText.Count > 0)
		{
			VNLUtil.getInstance().displayMessage(player.CurrentQuest.inProgressText.ToArray(), null, false, true, null).setTitle(player.CurrentQuest.name);
		}
		else if (player.CurrentQuest != null)
		{
			sayRandomStuff();
		}
		else if (this.quest != null)
		{
			if (this.quest.storyTexts.Count > 0)
			{
				displayDialog(this.quest.storyTexts.ToArray());
			}
			else
			{
				onOK();
			}
		}
		else
		{
			sayRandomStuff();
		}
	}

	private void sayRandomStuff()
	{
		VNLUtil.getInstance().displayMessage(randomStuffToSay[UnityEngine.Random.Range(0, randomStuffToSay.Count)], false).setTitle(base.name);
	}

	protected IMessageUI displayDialog(string[] msgs)
	{
		return VNLUtil.getInstance().displayMessage(msgs, delegate
		{
			onOK();
		}, false, true, null).setTitle(base.name);
	}

	protected void onOK()
	{
		if (quest.confirmationMsg == null || quest.confirmationMsg.Length == 0)
		{
			acceptQuest();
			return;
		}
		VNLUtil.getInstance().displayConfirmation(quest.confirmationMsg, delegate
		{
			acceptQuest();
		}, null).setTitle(quest.name);
	}

	protected virtual void acceptQuest()
	{
		if (player.CurrentQuest != null)
		{
			Debug.LogWarning("double click on accept quest, ignoring");
			return;
		}
		Debug.Log("quest accepted: " + quest);
		player.setCurrentQuest(quest);
	}
}
