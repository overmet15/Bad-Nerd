using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestInitializer : MonoBehaviour
{
	protected PlayerAttackComponent player;

	private List<QuestGiver> questGiversInThisLevel = new List<QuestGiver>();

	public bool ignorePreRequisites;

	public float delayBeforeCameraOn = 1f;

	[NonSerialized]
	public Map map;

	public GenericQuestsManager genericQuestsManager;

	public int minGenericQuestGivers = 1;

	public int maxGenericQuestGivers = 4;

	protected virtual void Awake()
	{
		VNLUtil.getInstance().updateEpisodeForSceneBasedOnCurrentScene();
		APIService.toggleAd(false);
		GameObject gameObject = GameObject.Find("/map");
		if (gameObject != null)
		{
			map = gameObject.GetComponent<Map>();
			VNLUtil.toggleAll(map.transform, false);
		}
		VNLUtil.getInstance("CharacterRoot");
		player = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		player.togglePlayerCamera(false);
		init();
		initGenericQuests();
		player.unCarryItemsAcrossLevel();
		player.initAttackAndArmorColliders();
		player.resetCameraRotation();
		player.GetComponent<Collider>().enabled = true;
		rememberQuestGivers();
		enableNetworkForPlayer();
		if (!GameStart.enableNetworking && string.Empty.Equals(PlayerPrefs.GetString("currentLevel", string.Empty)))
		{
			player.save(Application.loadedLevelName);
		}
	}

	private void initGenericQuests()
	{
		if (!(genericQuestsManager != null))
		{
			return;
		}
		int num = UnityEngine.Random.Range(minGenericQuestGivers, maxGenericQuestGivers + 1);
		string text = string.Empty;
		for (int i = 0; i < num; i++)
		{
			QuestGiver questGiver = genericQuestsManager.createRandomGiver(text);
			if (questGiver == null)
			{
				break;
			}
			text = text + questGiver.name + ",";
			Transform randomSpawnSpot = getSpawnManager().getRandomSpawnSpot();
			questGiver.transform.position = randomSpawnSpot.position;
			initRotation(randomSpawnSpot, questGiver.gameObject);
		}
	}

	public virtual void enableNetworkForPlayer()
	{
		if (GameStart.enableNetworking)
		{
			player.enableNetwork();
		}
	}

	private void rememberQuestGivers()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("questGiver");
		foreach (GameObject gameObject in array)
		{
			QuestGiver giver = gameObject.GetComponent<QuestGiver>();
			QuestGiver questGiver = giver;
			questGiver.onDestroyBehavior = (VNLUtil.NoNameMethod)Delegate.Combine(questGiver.onDestroyBehavior, (VNLUtil.NoNameMethod)delegate
			{
				questGiversInThisLevel.Remove(giver);
			});
			questGiversInThisLevel.Add(giver);
		}
	}

	public void reEnableQuestGiversIncaseTheyAreNext(Quest completedQuest)
	{
		Debug.Log("in reEnableQuestGiversIncaseTheyAreNext");
		foreach (QuestGiver item in questGiversInThisLevel)
		{
			Debug.Log("testing questGiver: " + item);
			if (!item.gameObject.active && !item.hasNoQuestsLeft() && item.isMetPrerequisite())
			{
				Debug.Log("reEnable questGiver" + item);
				VNLUtil.toggleAll(item.transform, true);
				item.init(false);
			}
		}
		if (genericQuestsManager != null)
		{
			QuestGiver questGiver = genericQuestsManager.createNextQuestBasedOnCompletedQuest(completedQuest);
			if (!(questGiver == null))
			{
				Transform randomSpawnSpot = getSpawnManager().getRandomSpawnSpot();
				questGiver.transform.position = randomSpawnSpot.position;
				initRotation(randomSpawnSpot, questGiver.gameObject);
			}
		}
	}

	private void initPlayer(float delay)
	{
		if (delay == 0f)
		{
			initPlayer();
			return;
		}
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			initPlayer();
		}, delay);
	}

	public void initPlayer()
	{
		player.initAttackAndArmorColliders();
		PlayerAttackComponent.allowMoving = true;
		player.togglePlayerCamera(true);
		VNLUtil.getInstance().activeUIID = 0;
	}

	protected virtual void init()
	{
		if (!GameStart.enableNetworking)
		{
			spawnCrowd();
		}
		bool flag = false;
		if (player.CurrentQuest != null)
		{
			if (Application.loadedLevelName.Equals(player.CurrentQuest.levelToGoTo))
			{
				flag = true;
				initPlayer(player.CurrentQuest.delayBeforeCameraOn);
			}
			player.CurrentQuest.init();
		}
		if (!flag)
		{
			initPlayer(delayBeforeCameraOn);
		}
		initPlayerPosition();
		player.currentLevel = Application.loadedLevelName;
	}

	protected virtual void initPlayerPosition()
	{
		if (GameStart.enableNetworking)
		{
			Transform randomSpawnSpot = getSpawnManager().getRandomSpawnSpot();
			putPlayerInSpot(randomSpawnSpot);
		}
		else
		{
			positionPlayerToPortal();
		}
	}

	protected void positionPlayerToPortal()
	{
		Portal portal = null;
		bool flag = false;
		int num = 0;
		GameObject[] array = GameObject.FindGameObjectsWithTag("Portal");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			Portal component = gameObject.GetComponent<Portal>();
			if (component.portalId == 0)
			{
				component.portalId = num;
				num++;
			}
			if (portal == null)
			{
				portal = component;
			}
			if (!flag && component.isDefaultPortal)
			{
				Debug.Log("no linked portal found yet, will use default portal: " + component.portalId + " if none are found after this");
				portal = component;
			}
			if (!flag)
			{
				Debug.Log("player.currentLevel:" + player.currentLevel + " == portal.levelToGoTo:" + component.levelToGoTo);
			}
			if (flag || player.currentLevel.Length <= 0 || !player.currentLevel.Equals(component.levelToGoTo))
			{
				continue;
			}
			Debug.Log("found linked portal by name: " + component.levelToGoTo + " id: " + component.portalId);
			if (component.leadsToReuseableScene)
			{
				Debug.Log("the portal is leadsToReuseableScene. needs to match VNLUtil.previousReuseableScenePortalId: " + VNLUtil.previousReuseableScenePortalId);
				if (VNLUtil.previousReuseableScenePortalId == component.portalId)
				{
					portal = component;
					flag = true;
					Debug.Log("the portal is for returning from reusable scene: " + component.levelToGoTo + " id: " + component.portalId);
					VNLUtil.previousReuseableScenePortalId = -1;
					VNLUtil.previousPortalId = -1;
				}
				else
				{
					Debug.Log("but doesn't match");
				}
			}
			else
			{
				if (component.leadsToReuseableScene)
				{
					continue;
				}
				Debug.Log("the portal is not leadsToReuseableScene.");
				if (component.portalId > 100)
				{
					Debug.Log("the portal requires a portalID match, id: " + component.portalId + ", needs to match: " + VNLUtil.previousPortalId);
					if (VNLUtil.previousPortalId == component.portalId)
					{
						Debug.Log("the portal portalID matched.");
						portal = component;
						flag = true;
						VNLUtil.previousPortalId = -1;
					}
					else
					{
						Debug.Log("the portal portalID did not match.");
					}
				}
				else
				{
					portal = component;
					flag = true;
				}
			}
		}
		if (portal != null)
		{
			Debug.Log("came in from portal " + portal.portalId + ".  From Level: " + player.currentLevel);
			Transform landingSpotTransform = portal.transform.Find("landingSpot");
			putPlayerInSpot(landingSpotTransform);
		}
		player.faceForward(true);
	}

	private void putPlayerInSpot(Transform landingSpotTransform)
	{
		player.setPosition(landingSpotTransform.position.x, landingSpotTransform.position.y, landingSpotTransform.position.z);
		initRotation(landingSpotTransform, player.gameObject);
	}

	private void initRotation(Transform landingSpotTransform, GameObject go)
	{
		go.transform.LookAt(landingSpotTransform.position + landingSpotTransform.forward);
		Quaternion rotation = go.transform.rotation;
		rotation.x = 0f;
		rotation.z = 0f;
		go.transform.rotation = rotation;
	}

	private void spawnCrowd()
	{
		getSpawnManager().init();
	}

	private SpawnManager getSpawnManager()
	{
		return GameObject.Find(Application.loadedLevelName + "SpawnManager").GetComponent<SpawnManager>();
	}
}
