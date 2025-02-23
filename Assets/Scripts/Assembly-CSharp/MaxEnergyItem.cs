public class MaxEnergyItem : GroupedItem
{
	public float healthValue = 50f;

	public override bool useItem(AttackComponent player)
	{
		base.useItem(player);
		player.maxEnergy += healthValue;
		return true;
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "maxEnergyValue", healthValue);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		healthValue = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "maxEnergyValue");
	}
}
