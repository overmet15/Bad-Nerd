using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUI : AbstractUI
{
	protected BadNerdItem selectedItem;

	private UIPanel descriptionPanel;

	private GameObject storeScrollCamera;

	private MyJoystick lControl;

	private GameObject storeIntroLabel;

	private GameObject storeTitleLabel;

	protected PlayerAttackComponent player;

	public List<BadNerdItem> itemList;

	protected Type[] categories;

	protected bool isFilterExcludeMode;

	private Vector3 originalCameraPosition = Vector3.zero;

	public bool sortByOrder;

	public bool sortByPrice;

	protected override void Awake()
	{
		base.Awake();
		VNLUtil.getInstance().activeUIID = 20;
		player = GameObject.Find("Player").GetComponent<PlayerAttackComponent>();
		storeIntroLabel = GameObject.Find("StoreIntroLabel");
		storeTitleLabel = GameObject.Find("StoreTitleLabel");
		lControl = GameObject.Find("LeftJoystick").GetComponent<MyJoystick>();
		lControl.enabled = false;
		descriptionPanel = GameObject.Find("DescriptionPanel").GetComponent<UIPanel>();
		VNLUtil.toggleAll(descriptionPanel.transform, false);
		storeScrollCamera = GameObject.Find("StoreScrollCamera");
		enableLights();
		VNLUtil.getInstance().pause();
		originalCameraPosition = storeScrollCamera.GetComponent<Camera>().transform.position;
	}

	protected virtual void enableLights()
	{
		if (VNLUtil.getInstance().episodeForScene == 1)
		{
			storeScrollCamera.AddComponent<Light>().intensity = 0.5f;
		}
	}

	public void setIntroLabelText(string txt)
	{
		storeIntroLabel.GetComponent<UILabel>().text = txt;
	}

	public void setTitleText(string txt)
	{
		storeTitleLabel.GetComponent<UILabel>().text = txt;
	}

	public void filterWeapons()
	{
		isFilterExcludeMode = false;
		categories = new Type[1] { typeof(Weapon) };
		init(true);
	}

	public void filterArmor()
	{
		isFilterExcludeMode = false;
		categories = new Type[1] { typeof(Armor) };
		init(true);
	}

	public virtual void filterMisc()
	{
		isFilterExcludeMode = true;
		categories = new Type[2]
		{
			typeof(Weapon),
			typeof(Armor)
		};
		init(true);
	}

	private List<BadNerdItem> getSortedItemList(List<BadNerdItem> l)
	{
		if (sortByOrder)
		{
			l.Sort(VNLUtil.compareByOder);
			return l;
		}
		if (sortByPrice)
		{
			l.Sort(VNLUtil.compareByPrice);
			return l;
		}
		return l;
	}

	protected void init(bool resetScroll)
	{
		Camera component = storeScrollCamera.GetComponent<Camera>();
		UIDraggableCamera component2 = storeScrollCamera.GetComponent<UIDraggableCamera>();
		GameObject gameObject = GameObject.Find("View UI");
		GameObject gameObject2 = GameObject.Find("StoreItemList");
		for (int i = 0; i < gameObject2.transform.GetChildCount(); i++)
		{
			UnityEngine.Object.Destroy(gameObject2.transform.GetChild(i).gameObject);
		}
		gameObject2.transform.DetachChildren();
		GameObject gameObject3 = null;
		foreach (BadNerdItem sortedItem in getSortedItemList(getItemList()))
		{
			bool flag = false;
			Type[] array = categories;
			foreach (Type type in array)
			{
				if (type.IsAssignableFrom(sortedItem.getCategory()) && isFilterExcludeMode)
				{
					flag = true;
				}
				else if (!type.IsAssignableFrom(sortedItem.getCategory()) && !isFilterExcludeMode)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				GameObject gameObject4 = VNLUtil.instantiate("StoreItem");
				if (gameObject3 == null)
				{
					gameObject3 = gameObject4;
				}
				gameObject4.transform.parent = gameObject2.transform;
				component2.rootForBounds = gameObject.transform;
				gameObject4.GetComponent<UIDragCamera>().draggableCamera = component2;
				Transform transform = gameObject4.transform.Find(sortedItem.getPlaceHolderName());
				GameObject gameObject5 = VNLUtil.instantiate(sortedItem.gameObject.name).gameObject;
				gameObject5.name = sortedItem.name;
				BadNerdItem component3 = gameObject5.GetComponent<BadNerdItem>();
				Vector3 position = transform.position;
				position.x += component3.hOffset;
				position.y += component3.vOffset;
				gameObject5.transform.position = position;
				gameObject5.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
				gameObject5.transform.parent = transform;
				ParticleRenderer component4 = gameObject5.GetComponent<ParticleRenderer>();
				if ((bool)component4)
				{
					component4.enabled = false;
				}
				Vector3 size = component3.getSize();
				if (size.y > size.x && size.y > size.z)
				{
					gameObject5.transform.localScale /= size.y;
				}
				else if (size.z > size.x && size.z > size.y)
				{
					gameObject5.transform.localScale /= size.z;
				}
				else
				{
					gameObject5.transform.localScale /= size.x;
				}
				VNLUtil.setLayer(gameObject5.transform, "uiItem");
				component3.spin = true;
				UIButtonMessage component5 = gameObject4.GetComponent<UIButtonMessage>();
				assignEventHandlerTarget(component5, gameObject5, sortedItem.gameObject);
				component5.functionName = getItemSelectFunction();
				UILabel component6 = gameObject4.transform.Find("PriceLabel").gameObject.GetComponent<UILabel>();
				component6.text = getPriceLabel(sortedItem);
				UILabel component7 = gameObject4.transform.Find("CountLabel").gameObject.GetComponent<UILabel>();
				string countLabel = getCountLabel(sortedItem);
				if (countLabel == null)
				{
					component7.enabled = false;
				}
				else
				{
					component7.text = countLabel;
				}
				UILabel component8 = gameObject4.transform.Find("ItemNameLabel").gameObject.GetComponent<UILabel>();
				component8.text = sortedItem.name;
				if (gameObject5.GetComponent<Rigidbody>() != null)
				{
					gameObject5.GetComponent<Rigidbody>().isKinematic = true;
				}
			}
		}
		gameObject2.GetComponent<UITable>().Reposition();
		toggleIntro(true);
		if (resetScroll && originalCameraPosition != Vector3.zero)
		{
			component.transform.position = originalCameraPosition;
		}
		if (gameObject3 == null)
		{
			component.enabled = false;
		}
		else
		{
			component.enabled = true;
		}
	}

	protected abstract string getPriceLabel(BadNerdItem item);

	protected abstract string getCountLabel(BadNerdItem item);

	protected abstract void assignEventHandlerTarget(UIButtonMessage msg, GameObject clone, GameObject original);

	protected abstract string getItemSelectFunction();

	protected abstract List<BadNerdItem> getItemList();

	public void selectItem(BadNerdItem item)
	{
		selectedItem = item;
		showDescriptionPanel();
	}

	public void confirmSelectItem()
	{
		if (VNLUtil.getInstance().activeUIID == 30)
		{
			onItemSelect(selectedItem);
		}
	}

	protected abstract void onItemSelect(BadNerdItem selectedItem);

	protected virtual void showDescriptionPanel()
	{
		VNLUtil.getInstance().activeUIID = 30;
		storeScrollCamera.active = false;
		toggleIntro(false);
		VNLUtil.toggleAll(descriptionPanel.transform, true);
		UILabel component = GameObject.Find("ItemName").GetComponent<UILabel>();
		component.text = selectedItem.name;
		UILabel component2 = GameObject.Find("ItemDescription").GetComponent<UILabel>();
		float num = 447f / (float)Screen.height;
		float num2 = (float)Screen.width * 0.8f * num;
		component2.lineWidth = (int)num2;
		component2.text = selectedItem.getDetailedDescription();
	}

	protected virtual void toggleIntro(bool on)
	{
		storeIntroLabel.active = on;
	}

	protected void hideDescriptionPanel()
	{
		if (VNLUtil.getInstance().activeUIID == 30)
		{
			VNLUtil.getInstance().activeUIID = 20;
			toggleIntro(true);
			storeScrollCamera.active = true;
			VNLUtil.toggleAll(descriptionPanel.transform, false);
		}
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			close();
			hideDescriptionPanel();
		}
	}

	public virtual void close()
	{
		if (VNLUtil.getInstance().activeUIID == 20)
		{
			VNLUtil.getInstance().activeUIID = 0;
			lControl.enabled = true;
			playCloseSound();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void OnDestroy()
	{
		VNLUtil.getInstance().resume();
	}
}
