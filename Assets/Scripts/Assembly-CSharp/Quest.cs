using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
	public Quest chainedQuest;

	private Quest parentQuest;

	public List<string> storyTexts;

	public List<string> beginQuestTexts;

	public List<string> successTexts;

	public List<string> inProgressText;

	public string confirmationMsg;

	public string levelToGoTo;

	public string levelToGoToWhenDone = "HomeBase";

	public bool autoJumpToLocation;

	public bool gotoQuestDestinationFromInfirmary;

	public bool infoOnly;

	public float delayBeforeCameraOn = 1f;

	protected int objectCount;

	private PlayerAttackComponent player;

	public BadNerdItem reward;

	public int moneyReward = 30;

	public bool shouldDoFB;

	public bool shouldDoKiip;

	private bool complete;

	public bool isDoNotPersistCompletion;

	public bool deletePlayerWhenDone;

	public int episode;

	public bool considerCompleteIfDifferentEpisode;

	public string nextQuestGiverLocation;

	public bool changeMusicToEpisode;

	public int episodeMusic;

	public Quest nextQuest;

	public bool doNotSpawnCrowd;

	public PlayerAttackComponent Player
	{
		get
		{
			return player;
		}
		set
		{
			player = value;
		}
	}

	public virtual void init()
	{
		if (!Application.loadedLevelName.Equals(levelToGoTo))
		{
			return;
		}
		Debug.Log("initializaing quest: " + base.name);
		complete = false;
		spawnQuestObjects();
		if (doNotSpawnCrowd)
		{
			GameObject.Find(Application.loadedLevelName + "SpawnManager").GetComponent<SpawnManager>().unSpawn();
		}
		if (beginQuestTexts.Count > 0)
		{
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				VNLUtil.getInstance().displayMessage(beginQuestTexts.ToArray(), null, false, true, null).setTitle(base.name);
			}, 1.5f);
		}
		APIService.logFlurryEvent("QuestStart-" + base.name);
		shouldDoFB = false;
		if (shouldDoKiip)
		{
			shouldDoFB = true;
		}
		if (infoOnly)
		{
			notifyComplete(false);
		}
		if (changeMusicToEpisode)
		{
			VNLUtil.getInstance().changeEpisodeMusic(episodeMusic);
			VNLUtil.getInstance().playNormalMusicForced();
		}
	}

	protected virtual void spawnQuestObjects()
	{
		objectCount = base.transform.GetChildCount();
		for (int i = 0; i < objectCount; i++)
		{
			QuestObject component = base.transform.GetChild(i).GetComponent<QuestObject>();
			QuestObject questObject = UnityEngine.Object.Instantiate(component) as QuestObject;
			questObject.quest = this;
			questObject.name = component.name;
			questObject.init();
			Debug.Log("init quest object Count: " + objectCount);
		}
	}

	public void notifyComplete(bool willNotComplete)
	{
		if (objectCount > 0)
		{
			objectCount--;
		}
		Debug.Log(base.name + " quest objectCount: " + objectCount);
		if (!willNotComplete && objectCount == 0)
		{
			onQuestCompleted();
		}
	}

	protected virtual void onQuestCompleted()
	{
		Debug.Log(base.name + " onQuestCompleted called, complete = " + complete);
		if (complete)
		{
			return;
		}
		complete = true;
		if (successTexts.Count == 0)
		{
			doComplete();
			return;
		}
		VNLUtil.getInstance().displayMessage(successTexts.ToArray(), delegate
		{
			doComplete();
		}, false, true, null).setTitle(getRootQuest().name);
	}

	private void doComplete()
	{
		if (chainedQuest == null)
		{
			Debug.Log(base.name + " chainedQuest==null");
			if (reward != null)
			{
				BadNerdItem badNerdItem = UnityEngine.Object.Instantiate(reward) as BadNerdItem;
				badNerdItem.name = reward.name;
				if (moneyReward > 0)
				{
					MoneyItem moneyItem = (MoneyItem)badNerdItem;
					moneyItem.count = moneyReward;
				}
				badNerdItem.bePickedUp(player);
			}
			getRootQuest().player.completeQuest();
			if (getRootQuest().shouldDoFB)
			{
				doFBPrompt();
			}
			if (getRootQuest().shouldDoKiip)
			{
				doKiip();
			}
			if (getRootQuest().deletePlayerWhenDone)
			{
				PlayerAttackComponent instance = VNLUtil.getInstance<PlayerAttackComponent>("Player");
				instance.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Combine(instance.onGoToScene, (VNLUtil.NoNameMethod)delegate
				{
					Debug.Log("deletePlayerWhenDone should now delete");
					UnityEngine.Object.Destroy(VNLUtil.getInstance("CharacterRoot"));
				});
			}
		}
		else
		{
			Debug.Log(base.name + " partial quest complete, chain quest pending: " + chainedQuest.name);
			chainedQuest.parentQuest = this;
			chainedQuest.init();
		}
		APIService.logFlurryEvent("QuestComplete-" + base.name);
	}

	public Quest getRootQuest()
	{
		if (parentQuest == null)
		{
			return this;
		}
		return parentQuest.getRootQuest();
	}

	private void doKiip()
	{
		APIService.doKiip(getRootQuest().name);
	}

	private void doFBPrompt()
	{
		APIService.logFlurryEvent("AcheivementPostAsked");
		VNLUtil.getInstance().displayConfirmation("postAchievement", delegate
		{
			APIService.logFlurryEvent("AcheivementPostAccepted");
			APIService.postToFB("I just completed '" + getRootQuest().name + "' on Bad Nerd!", "https://s3.amazonaws.com/org.lucius.edz/badnerd.png", "Bad Nerd", "Unleash The Nerd Rage!", "Available On The App Store and Google Play!");
		}, delegate
		{
			APIService.logFlurryEvent("AcheivementPostRejected");
		});
	}
}
