using System;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	private static string[] sceneKeys = new string[22]
	{
		"ArtGallery", "Auditorium", "BrightHall", "Cafeteria", "Gym", "HomeBase", "Infirmary", "PlayRoom", "TheLibrary", "WashRoom",
		"ArtGallery2", "Auditorium2", "BrightHall2", "Cafeteria2", "Gym2", "HomeBase2", "Infirmary2", "PlayRoom2", "TheLibrary2", "WashRoom2",
		"OnlineArena", "OnlineArena2"
	};

	private static string[] sceneNames = new string[22]
	{
		"Art Gallery", "Auditorium", "Bright Hall", "Cafeteria", "Gym", "Main Hall", "Infirmary", "Play Room", "Grand Library", "Washroom",
		"Art Gallery", "Auditorium", "Bright Hall", "Cafeteria", "Gym", "Main Hall", "Infirmary", "Play Room", "Grand Library", "Washroom",
		"Online Arena", "Online Arena"
	};

	public string levelToGoTo;

	public bool isDefaultPortal;

	public int portalId;

	public bool leadsToReuseableScene;

	public bool isInReuseableScene;

	public Quest prerequisite;

	public bool goToQuestDestination;

	public string overrideDoorSign;

	public bool goOnline;

	public bool isInfirmaryDoor;

	private bool detecting = true;

	private void Awake()
	{
		if (isInReuseableScene)
		{
			levelToGoTo = VNLUtil.previousLevel;
			VNLUtil.previousLevel = null;
		}
		if (levelToGoTo == null || levelToGoTo.Length == 0)
		{
			base.GetComponent<Collider>().enabled = false;
		}
		GetComponentInChildren<UILabel>().text = getDoorSign();
	}

	private string getDoorSign()
	{
		if (overrideDoorSign != null && overrideDoorSign.Length > 0)
		{
			return overrideDoorSign;
		}
		int num = new List<string>(sceneKeys).IndexOf(levelToGoTo);
		return sceneNames[num];
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (!detecting)
		{
			return;
		}
		PlayerAttackComponent player = collision.gameObject.GetComponent<PlayerAttackComponent>();
		if (!(player != null))
		{
			return;
		}
		if (!canUnlockDoorBasedOnPrerequisite(player))
		{
			VNLUtil.getInstance().displayMessage("lockedDoor", false);
			detecting = false;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				detecting = true;
			}, 3f);
			return;
		}
		string gotoThisMap = null;
		if (isInfirmaryDoor && GameStart.enableNetworking)
		{
			gotoThisMap = GameStart.onlineMap;
		}
		else if (goToQuestDestination && player.CurrentQuest != null && player.CurrentQuest.gotoQuestDestinationFromInfirmary)
		{
			gotoThisMap = player.CurrentQuest.levelToGoTo;
		}
		else if (GameStart.enableNetworking && !isInfirmaryDoor && string.Empty.Equals(PlayerPrefs.GetString("currentLevel", string.Empty)))
		{
			gotoThisMap = VNLUtil.getInstance().getIntroScene();
			PlayerAttackComponent playerAttackComponent = player;
			playerAttackComponent.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Combine(playerAttackComponent.onGoToScene, (VNLUtil.NoNameMethod)delegate
			{
				Debug.Log("deletePlayerWhenDone should now delete, for showing intro from multiplayer exit");
				UnityEngine.Object.Destroy(VNLUtil.getInstance("CharacterRoot"));
			});
		}
		else
		{
			gotoThisMap = levelToGoTo;
		}
		if (Application.loadedLevelName.Equals(gotoThisMap))
		{
			return;
		}
		detecting = false;
		PlayerAttackComponent.allowMoving = false;
		player.onStopWalking();
		VNLUtil.previousPortalId = portalId;
		if (leadsToReuseableScene)
		{
			VNLUtil.previousLevel = Application.loadedLevelName;
			VNLUtil.previousReuseableScenePortalId = portalId;
		}
		if (goOnline)
		{
			GameStart.enableDeathMatch(gotoThisMap);
		}
		else if (!isInfirmaryDoor && GameStart.enableNetworking)
		{
			NetworkCore networkCore = player.GetComponent<NetworkCore>();
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				networkCore.disconnect();
				player.gotoLevel(gotoThisMap);
			}, 5f);
			GameStart.exitOnlineMode();
			return;
		}
		player.gotoLevel(gotoThisMap);
	}

	private bool canUnlockDoorBasedOnPrerequisite(PlayerAttackComponent player)
	{
		if (prerequisite != null)
		{
			if (VNLUtil.getInstance().episode > prerequisite.episode)
			{
				return true;
			}
			if (player.isQuestComplete(prerequisite))
			{
				return true;
			}
			return false;
		}
		return true;
	}
}
