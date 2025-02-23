public class ItemUpgrader : GroupedItem
{
	public float upgradeValue = 2f;

	public override bool onInventoryUse(AttackComponent player)
	{
		VNLUtil.getInstance().displayMessage("howToUpgrade", true);
		return false;
	}

	public override string getStats()
	{
		return base.getStats() + "Upgrade: " + 2 + " units   ";
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "upgradeValue", upgradeValue);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		upgradeValue = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "upgradeValue");
	}
}
