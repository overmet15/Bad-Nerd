public class EnergyItem : GroupedItem
{
	public override bool useItem(AttackComponent player)
	{
		base.useItem(player);
		player.setEnergy(player.maxEnergy);
		return true;
	}
}
