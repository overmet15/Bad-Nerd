using System;

public class UpgradeStoreUI : StoreUI
{
	public override void filterMisc()
	{
		isFilterExcludeMode = false;
		categories = new Type[1] { typeof(UpgradeMovesItem) };
		init(true);
	}

	public void filterCounterAttacks()
	{
		isFilterExcludeMode = false;
		categories = new Type[1] { typeof(UpgradeCounterAttacksItem) };
		init(true);
	}

	protected override void enableLights()
	{
	}
}
