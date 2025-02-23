public class ZombieAttackComponent : EnemyAttackComponent
{
	protected override void dropRandomItem()
	{
		base.dropRandomItem();
		ZombieDrool zombieDrool = VNLUtil.instantiate<ZombieDrool>("Zombie Drool");
		zombieDrool.transform.position = myTransform.position;
	}
}
