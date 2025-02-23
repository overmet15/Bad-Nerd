public class ZombieDrool : GroupedItem
{
	public float effectiveTimeInSeconds;

	public override bool useItem(AttackComponent player)
	{
		base.useItem(player);
		player.setEnergy(player.maxEnergy);
		player.becomeZombie(effectiveTimeInSeconds);
		return true;
	}
}
