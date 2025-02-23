public class HealthItem : GroupedItem
{
	public float healthValue = 5f;

	public override bool useItem(AttackComponent player)
	{
		base.useItem(player);
		player.setLife(player.life + healthValue);
		return true;
	}

	public override string getStats()
	{
		return base.getStats() + "Calories: " + healthValue + "   ";
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "healthValue", healthValue);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		healthValue = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "healthValue");
	}
}
