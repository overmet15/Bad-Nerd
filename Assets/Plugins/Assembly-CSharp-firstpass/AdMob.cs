using System;
using Prime31;

public static class AdMob
{
	public static event Action receivedAdEvent;

	public static event Action<string> failedToReceiveAdEvent;

	public static event Action interstitialReceivedAdEvent;

	public static event Action<string> interstitialFailedToReceiveAdEvent;

	static AdMob()
	{
		AdMobAndroidManager.receivedAdEvent += receivedAdEventHandler;
		AdMobAndroidManager.failedToReceiveAdEvent += failedToReceiveAdEventHandler;
		AdMobAndroidManager.interstitialReceivedAdEvent += interstitialReceivedAdEventHandler;
		AdMobAndroidManager.interstitialFailedToReceiveAdEvent += interstitialFailedToReceiveAdEventHandler;
	}

	private static void receivedAdEventHandler()
	{
		AdMob.receivedAdEvent.fire();
	}

	private static void failedToReceiveAdEventHandler(string error)
	{
		AdMob.failedToReceiveAdEvent.fire(error);
	}

	private static void interstitialReceivedAdEventHandler()
	{
		AdMob.interstitialReceivedAdEvent.fire();
	}

	private static void interstitialFailedToReceiveAdEventHandler(string error)
	{
		AdMob.interstitialFailedToReceiveAdEvent.fire(error);
	}

	public static void setTagForChildDirectedTreatment(bool shouldTag)
	{
		AdMobAndroid.setTagForChildDirectedTreatment(shouldTag);
	}

	public static void setTestDevices(string[] testDevices)
	{
		AdMobAndroid.setTestDevices(testDevices);
	}

	public static void createBanner(string iosAdUnitId, string androidAdUnitId, AdMobBanner type, AdMobLocation placement)
	{
		AdMobAndroid.createBanner(androidAdUnitId, (int)type, (int)placement);
	}

	public static void destroyBanner()
	{
		AdMobAndroid.destroyBanner();
	}

	public static void requestInterstital(string androidInterstitialUnitId, string iosInterstitialUnitId)
	{
		AdMobAndroid.requestInterstital(androidInterstitialUnitId);
	}

	public static bool isInterstitalReady()
	{
		return AdMobAndroid.isInterstitalReady();
	}

	public static void displayInterstital()
	{
		AdMobAndroid.displayInterstital();
	}
}
