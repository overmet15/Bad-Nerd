using System;
using UnityEngine;

public class MoneyItem : UnGroupedItem
{
	public int count;

	public int maxAmount = 36;

	protected override void Awake()
	{
		count = UnityEngine.Random.Range(2, maxAmount);
		base.Awake();
	}

	public override bool bePickedUp(AttackComponent player)
	{
		if (!base.bePickedUp(player))
		{
			return false;
		}
		player.addLunchMoney(count);
		detachItem(false);
		UnityEngine.Object.Destroy(base.gameObject);
		return true;
	}

	public override string getStatusLabel()
	{
		throw new NotImplementedException();
	}
}
