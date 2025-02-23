public class InfirmaryInitializer : QuestInitializer
{
	protected override void Awake()
	{
		base.Awake();
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			player.stopAnimation();
			player.isDead = false;
			player.setLife(player.maxLife);
			player.animateIdle();
			VNLUtil.getInstance<StoreLoader>("weaponsDealerInfirmary").showStore();
		});
	}

	public override void enableNetworkForPlayer()
	{
	}

	protected override void initPlayerPosition()
	{
		positionPlayerToPortal();
	}
}
