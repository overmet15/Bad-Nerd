using Prime31;
using UnityEngine;

public class AdMobComboUI : MonoBehaviourGUI
{
	private void OnGUI()
	{
		beginColumn();
		if (GUILayout.Button("Set Test Devices"))
		{
			AdMob.setTestDevices(new string[6] { "6D13FA054BC989C5AC41900EE14B0C1B", "8E2F04DC5B964AFD3BC2D90396A9DA6E", "3BAB93112BBB08713B6D6D0A09EDABA1", "079adeed23ef3e9a9ddf0f10c92b8e18", "E2236E5E84CD318D4AD96B62B6E0EE2B", "149c34313ce10e43812233aad0b9aa4d" });
		}
		if (GUILayout.Button("Create Smart Banner"))
		{
			AdMob.createBanner("ca-app-pub-8386987260001674/2631573141", "ca-app-pub-8386987260001674/8398905145", AdMobBanner.SmartBanner, AdMobLocation.BottomCenter);
		}
		if (GUILayout.Button("Create 320x50 banner"))
		{
			AdMob.createBanner("ca-app-pub-8386987260001674/2631573141", "ca-app-pub-8386987260001674/8398905145", AdMobBanner.Phone_320x50, AdMobLocation.TopCenter);
		}
		if (GUILayout.Button("Create 300x250 banner"))
		{
			AdMob.createBanner("ca-app-pub-8386987260001674/2631573141", "ca-app-pub-8386987260001674/8398905145", AdMobBanner.Tablet_300x250, AdMobLocation.BottomCenter);
		}
		if (GUILayout.Button("Destroy Banner"))
		{
			AdMob.destroyBanner();
		}
		endColumn(true);
		if (GUILayout.Button("Request Interstitial"))
		{
			AdMob.requestInterstital("ca-app-pub-8386987260001674/9875638345", "ca-app-pub-8386987260001674/7061772743");
		}
		if (GUILayout.Button("Is Interstitial Ready?"))
		{
			bool flag = AdMob.isInterstitalReady();
			Debug.Log("is interstitial ready? " + flag);
		}
		if (GUILayout.Button("Display Interstitial"))
		{
			AdMob.displayInterstital();
		}
		endColumn();
	}

	private void OnEnable()
	{
		AdMob.receivedAdEvent += receivedAdEvent;
		AdMob.failedToReceiveAdEvent += failedToReceiveAdEvent;
		AdMob.interstitialReceivedAdEvent += interstitialReceivedAdEvent;
		AdMob.interstitialFailedToReceiveAdEvent += interstitialFailedToReceiveAdEvent;
	}

	private void OnDisable()
	{
		AdMob.receivedAdEvent -= receivedAdEvent;
		AdMob.failedToReceiveAdEvent -= failedToReceiveAdEvent;
		AdMob.interstitialReceivedAdEvent -= interstitialReceivedAdEvent;
		AdMob.interstitialFailedToReceiveAdEvent -= interstitialFailedToReceiveAdEvent;
	}

	private void receivedAdEvent()
	{
		Debug.Log("receivedAdEvent");
	}

	private void failedToReceiveAdEvent(string error)
	{
		Debug.Log("failedToReceiveAdEvent: " + error);
	}

	private void interstitialReceivedAdEvent()
	{
		Debug.Log("interstitialReceivedAdEvent");
	}

	private void interstitialFailedToReceiveAdEvent(string error)
	{
		Debug.Log("interstitialFailedToReceiveAdEvent: " + error);
	}
}
