using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStart : MonoBehaviour
{
	public const int GAME_MODE_DEATH_MATCH = 0;

	public const int GAME_MODE_GEEK_TAG = 1;

	public static bool resumeGame;

	public static bool enableNetworking;

	public static string onlineMap;

	public static int GAME_MODE;

	public GameObject mainMenu;

	public GameObject onlineMenu;

	public static bool requiresAndroidText;

	public static bool isZeemoteConnected;

	private void Start()
	{
		Screen.sleepTimeout = -1;
		VNLUtil.getInstance().activeUIID = 0;
		SystemLanguage systemLanguage = Application.systemLanguage;
		Debug.Log("Language: " + systemLanguage);
		if (Language.CurrentLanguage() != LanguageCode.EN && Language.CurrentLanguage() != LanguageCode.ES && Language.CurrentLanguage() != LanguageCode.FR && Language.CurrentLanguage() != LanguageCode.IT && Language.CurrentLanguage() != LanguageCode.NL && Language.CurrentLanguage() != LanguageCode.PT)
		{
			requiresAndroidText = true;
		}
		Debug.Log("requiresAndroidText: " + requiresAndroidText);
		if (systemLanguage == SystemLanguage.Chinese)
		{
			Debug.Log("detecting type of zh");
			string lang = APIService.getLang();
			Debug.Log("detected: " + lang);
			if (!lang.ToLower().Contains("cn"))
			{
				Debug.Log("switching language");
				Language.SwitchLanguage(LanguageCode.ZA);
			}
		}
		mainMenu = GameObject.Find("MainMenu");
		onlineMenu = GameObject.Find("OnlineMenu");
		VNLUtil.toggleAll(onlineMenu.transform, false);
		UILabel component = GameObject.Find("GameTitle").GetComponent<UILabel>();
		if (VNLUtil.getInstance().episode == 0)
		{
			component.text = "Bad Nerd";
		}
		else
		{
			component.text = "Nerd vs Zombies";
		}
		if (GameCenterBinding.isGameCenterAvailable())
		{
			GameCenterBinding.authenticateLocalPlayer();
		}
		if (PlayerPrefs.GetInt("hasSavedGame") == 0)
		{
			VNLUtil.toggleComponent(GameObject.Find("LoadButton").transform, false);
		}
		VNLUtil.getInstance().playNormalMusic(1);
		VNLUtil.getInstance().resetFirstTimeStartSceneMusicDone();
		if (VNLUtil.getInstance().episode == 0)
		{
			APIService.askUserToRate("https://play.google.com/store/apps/details?id=com.vnlentertainment.badnerd", "Rate Us", "Thank you for playing this game!  If you enjoyed this game please encourage us to keep working hard at improving it  by giving us 5 stars, a nice comment, and a PLUS ONE(+1) in the Android Market!  \n\nThank You!");
		}
		else
		{
			APIService.askUserToRate("https://play.google.com/store/apps/details?id=com.vnlentertainment.badnerdzombies", "Rate Us", "Thank you for playing this game!  If you enjoyed this game please encourage us to keep working hard at improving it  by giving us 5 stars, a nice comment, and a PLUS ONE(+1) in the Android Market!  \n\nThank You!");
		}
		int @int = PlayerPrefs.GetInt("gameStartedCount");
		@int++;
		PlayerPrefs.SetInt("gameStartedCount", @int);
		APIService.initFlurry("UA-33116527-1");
		if (VNLUtil.getInstance().episode == 0)
		{
			APIService.initFlurryAndroid("UA-32723865-1");
		}
		else
		{
			APIService.initFlurryAndroid("UA-34903179-1");
		}
		APIService.initFacebook("324125197666869", "396848653712131");
		APIService.initAdmob("ca-app-pub-1844236426719231/7457068926", "ca-app-pub-1844236426719231/1238799725");
		APIService.toggleAd(true);
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			APIService.toggleAd(false);
		}, 1f);
		APIService.initUnityAd("131623436");
		if (@int % 10 == 0)
		{
			APIService.logFlurryEvent("LoadGameFacebookAsked");
			VNLUtil.getInstance().displayConfirmation("facebookPromo", delegate
			{
				APIService.logFlurryEvent("LoadGameFacebookAccepted");
				APIService.postToFB();
			}, delegate
			{
				APIService.logFlurryEvent("LoadGameFacebookRejected");
			}).setBlock(true)
				.setTitle("Facebook");
		}
		if (VNLUtil.getInstance().episode == 0)
		{
			Object.Destroy(GameObject.Find("zombie1"));
		}
		else
		{
			Object.Destroy(GameObject.Find("theboy"));
		}
	}

	public void onPurchaseSuccessful()
	{
		setNeedToFinishPurchase(true);
	}

	public static void setNeedToFinishPurchase(bool flag)
	{
		PlayerPrefs.SetInt("needToFinishPurchase", flag ? 1 : 0);
		Debug.Log("setNeedToFinishPurchase: " + flag);
	}

	public static bool getNeedToFinishPurchase()
	{
		return PlayerPrefs.GetInt("needToFinishPurchase", 0) == 1;
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			APIService.exitGame("Exit", "Are you sure?");
		}
	}

	public void onStartClicked()
	{
		if (VNLUtil.getInstance().activeUIID != 0)
		{
			return;
		}
		if (PlayerPrefs.GetInt("hasSavedGame") != 0)
		{
			VNLUtil.getInstance().displayConfirmation("newGameWarning", delegate
			{
				PlayerPrefs.DeleteAll();
				loadLevel(VNLUtil.getInstance().getIntroScene());
			}, null).setBlock(true);
		}
		else
		{
			loadLevel(VNLUtil.getInstance().getIntroScene());
		}
	}

	public void onLoadClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			resumeGame = true;
			string text = PlayerPrefs.GetString("currentLevel", VNLUtil.getInstance().getIntroScene());
			if ("ZombieEnding".Equals(text))
			{
				text = "HomeBase2";
			}
			else if ("Ending1".Equals(text))
			{
				PlayerPrefs.SetInt("seenZombieIntro", 1);
				text = "ZombieIntro";
			}
			else if (isCompletedEpisode1() && PlayerPrefs.GetInt("seenZombieIntro", 0) == 0)
			{
				PlayerPrefs.SetInt("seenZombieIntro", 1);
				text = "ZombieIntro";
			}
			loadLevel(text);
		}
	}

	private bool isCompletedEpisode1()
	{
		return isCompleted("Wave1");
	}

	private bool isCompleted(string questName)
	{
		string @string = PlayerPrefs.GetString("levelsCompleted");
		List<string> list = @string.Split(',').ToList();
		return list.Contains(questName);
	}

	public void onOnlineClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			VNLUtil.toggleAll(mainMenu.transform, false);
			VNLUtil.toggleAll(onlineMenu.transform, true);
		}
	}

	public void onScoresClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			APIService.showLeaderBoard();
		}
	}

	public void onGeekTagButtonClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			VNLUtil.getInstance().displayMessage("noGameType", true);
		}
	}

	public void onDeathMatchButtonClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			resumeGame = true;
			if (isCompleted("Insane Child"))
			{
				enableDeathMatch("OnlineArena2");
			}
			else
			{
				enableDeathMatch("OnlineArena");
			}
			loadLevel(onlineMap);
		}
	}

	public static void enableDeathMatch(string level)
	{
		GAME_MODE = 0;
		enableNetworking = true;
		onlineMap = level;
	}

	public static void exitOnlineMode()
	{
		enableNetworking = false;
	}

	public void onAchievementsClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			GameCenterBinding.showAchievements();
		}
	}

	public void onBackButtonClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			VNLUtil.toggleAll(mainMenu.transform, true);
			VNLUtil.toggleAll(onlineMenu.transform, false);
		}
	}

	public void onShareClicked()
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			APIService.postToFB();
			VNLUtil.getInstance().displayMessage("thanksForLove", true);
		}
	}

	public void onControllerInitClicked()
	{
		if (VNLUtil.getInstance().activeUIID != 0)
		{
			return;
		}
		if (isZeemoteConnected)
		{
			VNLUtil.getInstance().displayMessage("controllerAlreadyConnected", true);
			return;
		}
		VNLUtil.getInstance().displayMessage("plsEnableController", delegate
		{
			bool flag = ZeemoteInput.SetupPlugin();
			Debug.Log("setupZeemoteReturn: " + flag);
			int num = ZeemoteInput.FindAvailableControllers();
			Debug.Log("controllerCount: " + num);
			if (num > 0)
			{
				int num2 = 0;
				string[] controllerNameList = ZeemoteInput.GetControllerNameList();
				foreach (string text in controllerNameList)
				{
					Debug.Log("trying: " + text);
					isZeemoteConnected = ZeemoteInput.ConnectController(1, num2);
					Debug.Log("isZeemoteConnected: " + isZeemoteConnected);
					if (isZeemoteConnected)
					{
						break;
					}
					num2++;
				}
			}
			if (isZeemoteConnected)
			{
				VNLUtil.getInstance().displayMessage("controllerConnected", true);
				if (PlayerPrefs.GetInt("zeemoteTut", 0) == 0)
				{
					VNLUtil.getInstance().displayMessage(new string[2] { "zeemoteTut1", "zeemoteTut2" }, delegate
					{
						PlayerPrefs.SetInt("zeemoteTut", 1);
					}, false, false, null);
				}
			}
			else
			{
				VNLUtil.getInstance().displayMessage("controllerNotConnected", true);
			}
		}, true, false);
	}

	private void loadLevel(string level)
	{
		VNLUtil.getInstance().resume();
		VNLUtil.getInstance<LevelLoader>().loadLevel(level);
	}

	private void OnApplicationQuit()
	{
		Debug.Log("quiting...");
		if (isZeemoteConnected)
		{
			ZeemoteInput.DisconnectController(1);
			ZeemoteInput.CleanupPlugin();
		}
	}
}
