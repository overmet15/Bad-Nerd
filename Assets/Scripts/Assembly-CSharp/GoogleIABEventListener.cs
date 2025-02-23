using System.Collections.Generic;
using Prime31;
using UnityEngine;

public class GoogleIABEventListener : MonoBehaviour
{
	private void OnEnable()
	{
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}

	private void OnDisable()
	{
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	private void billingSupportedEvent()
	{
		Debug.Log("billingSupportedEvent");
		GoogleIAB.setAutoVerifySignatures(true);
		GoogleIAB.queryInventory(new string[1] { APIService.lunchMoneyIABv3 });
	}

	private void billingNotSupportedEvent(string error)
	{
		Debug.Log("billingNotSupportedEvent: " + error);
	}

	private void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
		Debug.Log(string.Format("queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
		Utils.logObject(purchases);
		Utils.logObject(skus);
		bool flag = true;
		bool flag2 = true;
		foreach (GooglePurchase purchase in purchases)
		{
			Debug.Log("queryInventorySucceededEvent .. consumeProduct: " + purchase.productId + ": " + purchase.purchaseState);
			GoogleIAB.consumeProduct(purchase.productId);
			flag = false;
			flag2 = false;
		}
		if (flag2)
		{
			Debug.Log("queryInventorySucceededEvent.. purchaseProduct");
			flag = false;
			GoogleIAB.purchaseProduct(APIService.lunchMoneyIABv3);
		}
		if (flag)
		{
			APIService.stopIAB();
		}
	}

	private void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);
	}

	private void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
	{
		Debug.Log("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
	}

	private void purchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("purchaseSucceededEvent: " + purchase);
		GoogleIAB.consumeProduct(APIService.lunchMoneyIABv3);
	}

	private void purchaseFailedEvent(string error)
	{
		Debug.Log("purchaseFailedEvent: " + error);
	}

	private void consumePurchaseSucceededEvent(GooglePurchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		GameObject gameObject = GameObject.Find("Player");
		PlayerAttackComponent component = gameObject.GetComponent<PlayerAttackComponent>();
		component.onPurchaseSuccessful();
	}

	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
		GameObject gameObject = GameObject.Find("Player");
		PlayerAttackComponent component = gameObject.GetComponent<PlayerAttackComponent>();
		component.onPurchaseFailed();
	}
}
