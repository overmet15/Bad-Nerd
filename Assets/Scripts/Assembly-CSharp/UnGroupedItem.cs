public abstract class UnGroupedItem : BadNerdItem
{
	public override bool bePickedUp(AttackComponent player)
	{
		if (!base.bePickedUp(player))
		{
			return false;
		}
		if (!player.itemList.Contains(this))
		{
			player.itemList.Add(this);
		}
		return true;
	}
}
