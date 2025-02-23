using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : AbstractInventoryUI
{
	private GameObject fixButton;

	private GameObject upgradeButton;

	public VNLUtil.NoNameMethod tutorialInventoryTrigger;

	protected override void Awake()
	{
		fixButton = GameObject.Find("InventoryUIFixButton");
		upgradeButton = GameObject.Find("InventoryUIUpgradeButton");
		base.Awake();
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			filterWeapons();
		});
	}

	protected override List<BadNerdItem> getItemList()
	{
		return player.itemList;
	}

	protected override string getItemSelectFunction()
	{
		return "onInventorySelect";
	}

	protected override void onItemSelect(BadNerdItem item)
	{
		if (item.onInventoryUse(player))
		{
			hideDescriptionPanel();
			init(false);
		}
	}

	protected override void assignEventHandlerTarget(UIButtonMessage msg, GameObject clone, GameObject original)
	{
		msg.target = original;
	}

	protected override string getPriceLabel(BadNerdItem item)
	{
		return item.getStatusLabel();
	}

	protected override string getCountLabel(BadNerdItem item)
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

	protected override void showDescriptionPanel()
	{
		base.showDescriptionPanel();
		UILabel component = GameObject.Find("InventoryUIUseButton").transform.Find("Label").GetComponent<UILabel>();
		component.text = selectedItem.getInventoryUseButtonLabel();
		bool flag = selectedItem.isUpgradableItem();
		VNLUtil.toggleComponent(upgradeButton.transform, flag);
		upgradeButton.GetComponent<Collider>().enabled = flag;
		bool flag2 = selectedItem.isFixableItem();
		VNLUtil.toggleComponent(fixButton.transform, flag2);
		fixButton.GetComponent<Collider>().enabled = flag2;
	}

	public void drop()
	{
		if (VNLUtil.getInstance().activeUIID == 30)
		{
			selectedItem.detachItem(true);
			hideDescriptionPanel();
			init(false);
		}
	}

	public void fix()
	{
		if (VNLUtil.getInstance().activeUIID == 30 && selectedItem.fix())
		{
			hideDescriptionPanel();
			init(false);
		}
	}

	public void upgrade()
	{
		if (VNLUtil.getInstance().activeUIID == 30 && selectedItem.upgrade())
		{
			hideDescriptionPanel();
			init(false);
		}
	}

	public override void close()
	{
		base.close();
		if (tutorialInventoryTrigger != null)
		{
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				tutorialInventoryTrigger();
			});
		}
	}
}
