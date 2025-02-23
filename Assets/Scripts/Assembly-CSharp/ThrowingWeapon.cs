using UnityEngine;

public class ThrowingWeapon : Weapon
{
	public int count = 25;

	private bool used;

	public override string getStats()
	{
		return base.getStats() + "   Shots: " + count + "   ";
	}

	public override string getStatusLabel()
	{
		return count.ToString();
	}

	public override float getWeight()
	{
		return base.getWeight() * (float)count;
	}

	public override void doAttack(int tapCount)
	{
		if (!(owner == null) && !owner.isAlreadyThrowing())
		{
			Vector3 target = ((!(owner.CurrentEnemy == null)) ? owner.CurrentEnemy.transform.position : (owner.transform.position + owner.transform.TransformDirection(Vector3.forward) * 10f));
			owner.onThrow(target);
		}
	}

	public override void throwAt(Vector3 target)
	{
		count--;
		ThrowingWeapon throwingWeapon = this;
		if (count > 0)
		{
			ThrowingWeapon throwingWeapon2 = (ThrowingWeapon)Object.Instantiate(this, base.transform.position, base.transform.rotation);
			throwingWeapon2.transform.parent = base.transform.parent;
			throwingWeapon2.owner = owner;
			throwingWeapon2.assignedTag = assignedTag;
			throwingWeapon = throwingWeapon2;
		}
		target.y += 3f;
		throwingWeapon.doThrow(target);
	}

	public override void doThrow(Vector3 target)
	{
		Object.Destroy(base.gameObject, 1.5f);
		used = true;
		base.doThrow(target);
	}

	public override bool bePickedUp(AttackComponent player)
	{
		if (used)
		{
			return false;
		}
		return base.bePickedUp(player);
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetInt("saveItemList" + itemListIndex + "count", count);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		count = ItemSerializer.GetInt("saveItemList" + itemListIndex + "count");
	}
}
