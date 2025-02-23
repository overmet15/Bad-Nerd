public class ItemFixer : GroupedItem
{
	public float fixValue = 20f;

	public override bool onInventoryUse(AttackComponent player)
	{
		VNLUtil.getInstance().displayMessage("howToFix", true);
		return false;
	}

	public override string getStats()
	{
		return base.getStats() + "Restoration: " + fixValue + "   ";
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "fixValue", fixValue);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		fixValue = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "fixValue");
	}
}
