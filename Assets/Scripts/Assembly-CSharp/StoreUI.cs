using System.Collections.Generic;
using UnityEngine;

public class StoreUI : AbstractInventoryUI
{
	private bool sellMode;

	private int buyQuantity = 1;

	private GameObject storeSellingLabel;

	private Transform moreButton;

	private Transform lessButton;

	protected override void Awake()
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i] == null)
			{
				itemList.RemoveAt(i);
				i--;
			}
		}
		moreButton = GameObject.Find("StoreUIBuyMoreButton").transform;
		lessButton = GameObject.Find("StoreUIBuyLessButton").transform;
		storeSellingLabel = GameObject.Find("StoreSellingLabel");
		base.Awake();
		APIService.logFlurryEvent("Shopped-" + base.name);
	}

	protected override void toggleIntro(bool on)
	{
		base.toggleIntro(false);
		storeSellingLabel.active = false;
		if (on)
		{
			if (sellMode)
			{
				storeSellingLabel.active = on;
			}
			else
			{
				base.toggleIntro(on);
			}
		}
	}

	protected override string getItemSelectFunction()
	{
		return "onStoreSelect";
	}

	protected override void assignEventHandlerTarget(UIButtonMessage msg, GameObject clone, GameObject original)
	{
		if (sellMode)
		{
			msg.target = original;
		}
		else
		{
			msg.target = clone;
		}
	}

	protected override List<BadNerdItem> getItemList()
	{
		if (sellMode)
		{
			return player.itemList;
		}
		return itemList;
	}

	protected override void showDescriptionPanel()
	{
		base.showDescriptionPanel();
		UILabel component = GameObject.Find("StoreUIBuyButton").transform.Find("Label").GetComponent<UILabel>();
		component.text = ((!sellMode) ? "Buy" : "Sell");
		buyQuantity = 1;
		if (typeof(GroupedItem).IsAssignableFrom(selectedItem.GetType()))
		{
			VNLUtil.toggleComponent(moreButton, true);
			VNLUtil.toggleComponent(lessButton, true);
		}
		else
		{
			VNLUtil.toggleComponent(moreButton, false);
			VNLUtil.toggleComponent(lessButton, false);
		}
		updateQuantityCount();
		APIService.logFlurryEvent("Browsed-" + selectedItem.name);
	}

	protected override void onItemSelect(BadNerdItem selectedItem)
	{
		if (sellMode)
		{
			if (typeof(GroupedItem).IsAssignableFrom(selectedItem.GetType()))
			{
				GroupedItem groupedItem = (GroupedItem)selectedItem;
				groupedItem.count -= buyQuantity;
				if (groupedItem.count <= 0)
				{
					selectedItem.detachItem(false);
					Object.Destroy(selectedItem.gameObject);
				}
			}
			else
			{
				selectedItem.detachItem(false);
				Object.Destroy(selectedItem.gameObject);
			}
			player.addLunchMoney(calculateTransationPrice(selectedItem, buyQuantity));
			APIService.logFlurryEvent("Sold-" + selectedItem.name);
		}
		else
		{
			int num = calculateTransationPrice(selectedItem, buyQuantity);
			if (player.LunchMoney < num)
			{
				VNLUtil.getInstance().displayConfirmation("askMomForSome", delegate
				{
					if (VNLUtil.getInstance().isAmazonVersion)
					{
						APIService.buyAmazonItem();
					}
					else
					{
						APIService.showUnityAd();
					}
				}, null).setBlock(true)
					.setTitle("Lunch Money");
				return;
			}
			BadNerdItem badNerdItem = VNLUtil.instantiate<BadNerdItem>(selectedItem.gameObject.name);
			badNerdItem.name = selectedItem.name;
			if (typeof(GroupedItem).IsAssignableFrom(badNerdItem.GetType()))
			{
				GroupedItem groupedItem2 = (GroupedItem)badNerdItem;
				groupedItem2.count = buyQuantity;
			}
			if (typeof(EquipmentItem).IsAssignableFrom(badNerdItem.GetType()))
			{
				EquipmentItem equipmentItem = (EquipmentItem)badNerdItem;
				equipmentItem.isFixable = true;
			}
			if (!badNerdItem.bePickedUp(player))
			{
				Object.Destroy(badNerdItem.gameObject);
				return;
			}
			player.addLunchMoney(-num);
			APIService.logFlurryEvent("Bought-" + selectedItem.name);
		}
		hideDescriptionPanel();
		init(false);
	}

	private int calculateTransationPrice(BadNerdItem item, int quantity)
	{
		if (sellMode)
		{
			return (int)((float)(item.price * quantity) * 0.85f);
		}
		return item.price * quantity;
	}

	public void toggleMode()
	{
		if (VNLUtil.getInstance().activeUIID == 20)
		{
			sellMode = !sellMode;
			UILabel component = GameObject.Find("StoreUIToggleModeButton").transform.Find("Label").GetComponent<UILabel>();
			component.text = ((!sellMode) ? "Sell" : "Buy");
			init(true);
		}
	}

	protected override string getPriceLabel(BadNerdItem item)
	{
		if (sellMode)
		{
			return item.getStatusLabel();
		}
		return item.getPriceLabel();
	}

	protected override string getCountLabel(BadNerdItem item)
	{
		if (sellMode)
		{
			bool flag = false;
			if (typeof(EquipmentItem).IsAssignableFrom(item.GetType()))
			{
				EquipmentItem equipmentItem = (EquipmentItem)item;
				flag = equipmentItem.isEquipped();
			}
			if (flag)
			{
				return "Equipped";
			}
			return null;
		}
		return item.getCountLabel();
	}

	private void updateQuantityCount()
	{
		UILabel component = GameObject.Find("StoreUIBuyQuantityLabel").GetComponent<UILabel>();
		UILabel component2 = GameObject.Find("StoreUITotalPriceLabel").GetComponent<UILabel>();
		component.text = buyQuantity.ToString();
		component2.text = "For $" + calculateTransationPrice(selectedItem, buyQuantity);
	}

	public void more()
	{
		if (VNLUtil.getInstance().activeUIID != 30)
		{
			return;
		}
		if (sellMode)
		{
			GroupedItem groupedItem = (GroupedItem)selectedItem;
			if (buyQuantity < groupedItem.count)
			{
				buyQuantity++;
			}
		}
		else
		{
			buyQuantity++;
		}
		updateQuantityCount();
	}

	public void less()
	{
		if (VNLUtil.getInstance().activeUIID == 30 && buyQuantity > 1)
		{
			buyQuantity--;
			updateQuantityCount();
		}
	}
}
