public class MaxHealthItem : GroupedItem
{
	public float healthValue = 50f;

	public override bool useItem(AttackComponent player)
	{
		base.useItem(player);
		player.maxLife += healthValue;
		player.setLife(player.maxLife);
		return true;
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "maxHealthValue", healthValue);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		healthValue = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "maxHealthValue");
	}
}
