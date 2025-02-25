using System;
using System.Runtime.InteropServices;
using Prime31;
using UnityEngine;
using UnityEngine.Advertisements;

public class APIService
{
	private static string storeLinkAndroid1 = "https://play.google.com/store/apps/details?id=com.vnlentertainment.badnerd";

	private static string storeLinkAndroid2 = "https://play.google.com/store/apps/details?id=com.vnlentertainment.badnerdzombies";

	private static string storeLinkApple = "https://itunes.apple.com/us/app/bad-nerd-open-world-rpg/id600060295?ls=1&mt=8";

	public static string lunchMoneyIABv3 = "lunchmoney1500_v3";

	private static float lastRefreshTime;

	private static bool adMobBannerAlreadyCreated;

	private static string admobId;

	private static string admobInterstitialId;

	private static bool onFBRequestCompleteAssigned;

	private static string lastFBRequestMessage;

	private static string getAndroidStoreLink()
	{
		if (VNLUtil.getInstance().episode == 0)
		{
			return storeLinkAndroid1;
		}
		return storeLinkAndroid2;
	}

	public static void androidCall(string method, object[] parameters)
	{
		if (Application.isEditor)
		{
			return;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				if (parameters != null)
				{
					androidJavaObject.Call(method, parameters);
				}
				else
				{
					androidJavaObject.Call(method);
				}
			}
		}
	}

	public static string androidCallWithReturn(string method, object[] parameters)
	{
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				if (parameters != null)
				{
					return androidJavaObject.Call<string>(method, parameters);
				}
				return androidJavaObject.Call<string>(method, new object[0]);
			}
		}
	}

	public static string getLang()
	{
		return androidCallWithReturn("getLang", null);
	}

	public static void displayText(string txt)
	{
		displayText(txt, 120, 65, 120, 150, 1.33f);
	}

	public static void displayText(string txt, int left, int top, int right, int bottom, float scale)
	{
		Debug.Log("unity displayText: " + txt);
		androidCall("displayText", new object[6] { txt, left, top, right, bottom, scale });
	}

	public static void unDisplayText()
	{
		Debug.Log("unity unDisplayText");
		androidCall("unDisplayText", null);
	}

	public static void initUnityAd(string id)
	{
		if (Advertisement.isSupported)
		{
			Advertisement.allowPrecache = true;
			Advertisement.Initialize(id);
		}
	}

	public static void showUnityAd()
	{
		Advertisement.Show("rewardedVideoZone", new ShowOptions
		{
			resultCallback = delegate(ShowResult result)
			{
				if (result == ShowResult.Finished)
				{
					VNLUtil.getInstance<PlayerAttackComponent>("Player").addLunchMoney(500);
					VNLUtil.getInstance().displayMessage("You got $500!", false);
				}
			}
		});
	}

	public static void initLeadBoltAd(string adId)
	{
		androidCall("initLeadBoltAd", new object[1] { adId });
	}

	public static void showLeadBoltAd()
	{
		androidCall("showLeadBoltAd", new object[1] { false });
	}

	public static void initLeadBoltNotifs(string notifId, string iconId)
	{
		androidCall("initLeadBoltNotifs", new object[2] { notifId, iconId });
	}

	public static void exitGame(string exitTitle, string exitText)
	{
		if (VNLUtil.getInstance().activeUIID == 0)
		{
			VNLUtil.getInstance().displayConfirmation("Exit the game?", delegate
			{
				Application.Quit();
			}, null);
		}
	}

	public static void setVirtualItemId(string id)
	{
		androidCall("setVirtualItemId", new object[1] { id });
	}

	public static void stopIAB()
	{
		Debug.Log("stopIAB...");
	}

	public static void toggleAd(bool on)
	{
		if (!VNLUtil.getInstance().isAmazonVersion)
		{
			if (on)
			{
				AdMob.createBanner(admobId, admobId, AdMobBanner.SmartBanner, AdMobLocation.BottomCenter);
			}
			else
			{
				AdMob.destroyBanner();
			}
		}
	}

	public static void showFullScreenAd()
	{
		showGooglePlayFullscreenOnly();
	}

	public static void showGooglePlayFullscreenOnly()
	{
		AdMob.requestInterstital(admobInterstitialId, admobInterstitialId);
	}

	public static void initAdmob(string id, string interstitialId)
	{
		admobId = id;
		admobInterstitialId = interstitialId;
		AdMob.interstitialReceivedAdEvent += delegate
		{
			AdMob.displayInterstital();
		};
		AdMob.interstitialFailedToReceiveAdEvent += delegate
		{
			toggleAd(true);
		};
		AdMob.requestInterstital(admobInterstitialId, admobInterstitialId);
	}

	public static void askUserToRate(string marketUrl, string rateTitle, string rateText)
	{
		androidCall("askUserToRate", new object[5] { marketUrl, rateTitle, rateText, "Later", "Don't ask again" });
	}

	public static void postToFB()
	{
		postToFB("If you're an Action RPG Fan, then you'll love Bad Nerd. Tell your friends.", "https://s3.amazonaws.com/org.lucius.edz/badnerdIcon.png", "Bad Nerd", "Unleash The Nerd Rage!", "Available On The App Store and Google Play!");
	}

	public static void postToFB(string msg, string img, string linkName, string caption, string desc)
	{
		string androidStoreLink = getAndroidStoreLink();
		postToFB(msg, img, linkName, androidStoreLink, caption, desc);
	}

	public static void requestToFB(string msg)
	{
		lastFBRequestMessage = msg;
		Debug.Log("fbReq: " + msg);
		logFlurryEvent("requestToFB");
		androidCall("fbRequest", new object[7]
		{
			msg,
			"https://s3.amazonaws.com/org.lucius.edz/badnerdIcon.png",
			"Bad Nerd",
			getAndroidStoreLink(),
			"Unleash The Nerd Rage!",
			"Available On The App Store and Google Play!",
			"Player"
		});
	}

	private static void onFBRequestFailDueToLotLoggedIn(P31Error err)
	{
		if (err != null && err.code.ToString().Contains("999"))
		{
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				Debug.Log("requestToFB 999, trying again...");
				requestToFB(lastFBRequestMessage);
			}, 5f);
		}
	}

	private static void onFBRequestComplete(string url)
	{
		Debug.Log("onFBRequestComplete url: " + url);
		string[] array = url.Split(new string[1] { "&to%5B" }, StringSplitOptions.None);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = array[i];
			Debug.Log("section: " + text2);
			if (!text2.Contains("request="))
			{
				string text3 = text2.Substring(text2.IndexOf('=') + 1);
				Debug.Log("id: " + text3);
				text += text3;
				if (i < array.Length - 1)
				{
					text += ",";
				}
			}
		}
		Debug.Log("ids: " + text);
		VNLUtil.getInstance<PlayerAttackComponent>("Player").apprequestsSuccess(text);
	}

	public static void postToFB(string msg, string img, string linkName, string url, string caption, string desc)
	{
		Debug.Log("fb: " + msg + " - " + url);
		logFlurryEvent("FacebookPosted");
		androidCall("fbPost", new object[8] { "me", msg, img, linkName, url, caption, desc, "VNLUtil" });
	}

	public static void doKiip(string id)
	{
		Debug.Log("kiip: " + id);
	}

	/*public static void leaderBoard(string id)
	{
		int @int = PlayerPrefs.GetInt(id, 0);
		@int++;
		PlayerPrefs.SetInt(id, @int);
		if (VNLUtil.getInstance().episode == 0)
		{
			GameCenterBinding.reportScore(@int, id);
		}
	} 

	public static void showLeaderBoard()
	{
		if (VNLUtil.getInstance().episode == 0)
		{
			GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.AllTime, "baddestBullyBeater");
		}
	}

	public static void showOnlineLeaderBoard()
	{
		if (VNLUtil.getInstance().episode == 0)
		{
			GameCenterBinding.showLeaderboardWithTimeScopeAndLeaderboard(GameCenterLeaderboardTimeScope.AllTime, "baddestBullyBeaterOnline");
		}
	}
	*/

	[DllImport("__Internal")]
	private static extern void _logFlurryEvent(string str);

	[DllImport("__Internal")]
	private static extern void _initFlurry(string str);

	public static void initFlurry(string id)
	{
	}

	public static void initFlurryAndroid(string id)
	{
		androidCall("initFlurry", new object[1] { id });
	}

	public static void initFacebook(string id, string appPageId)
	{
		string text = "\n\niPhone: " + storeLinkApple + "\n\nAndroid: " + getAndroidStoreLink();
		androidCall("initFacebook", new object[3] { id, appPageId, text });
	}

	public static void logFlurryEvent(string str)
	{
		Debug.Log("flurry: " + str);
		androidCall("logFlurryEvent", new object[1] { str });
	}

	public static void buyAmazonItem()
	{
		Debug.Log("buyAmazonItem called");
		if (VNLUtil.getInstance().episode == 0)
		{
			androidCall("buyAmazonItem", new object[2] { "00002", "Player" });
		}
		else
		{
			androidCall("buyAmazonItem", new object[2] { "00003", "Player" });
		}
	}
}
