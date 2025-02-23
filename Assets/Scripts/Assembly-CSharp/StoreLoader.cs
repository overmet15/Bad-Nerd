using UnityEngine;

public class StoreLoader : MonoBehaviour
{
	public StoreUI storeUIPrefab;

	public int type;

	private bool detecting = true;

	public string overrideIntroLabel;

	public string overrideTitleLabel;

	public void OnCollisionEnter(Collision collision)
	{
		if (!detecting)
		{
			return;
		}
		GameObject gameObject = collision.gameObject;
		if (gameObject.CompareTag("Player") && VNLUtil.getInstance().activeUIID == 0)
		{
			detecting = false;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				detecting = true;
			}, 3f);
			showStore();
		}
	}

	public void showStore()
	{
		if (GameObject.Find("StoreUI") == null)
		{
			StoreUI storeUI = (StoreUI)Object.Instantiate(storeUIPrefab);
			storeUI.gameObject.name = "StoreUI";
			if (!string.Empty.Equals(overrideIntroLabel))
			{
				storeUI.setIntroLabelText(overrideIntroLabel);
			}
			if (!string.Empty.Equals(overrideTitleLabel))
			{
				storeUI.setTitleText(overrideTitleLabel);
			}
			if (type == 0)
			{
				storeUI.filterWeapons();
			}
			else if (type == 1)
			{
				storeUI.filterArmor();
			}
			if (type == 2)
			{
				storeUI.filterMisc();
			}
		}
	}
}
