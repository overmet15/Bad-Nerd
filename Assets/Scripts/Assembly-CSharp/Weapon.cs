using UnityEngine;

public class Weapon : EquipmentItem
{
	public float damage = 1f;

	public bool isThrowable;

	protected override string getNameOfBoneToHoldEquipment()
	{
		return "rightHand";
	}

	public override string getStats()
	{
		return base.getStats() + "Damage: " + damage.ToString("F2") + "\nThrowable: " + ((!isThrowable) ? "No" : "Yes");
	}

	protected override void setUserEquipmentOfThisType(bool setNull)
	{
		base.setUserEquipmentOfThisType(setNull);
		if (owner != null)
		{
			owner.currentWeapon = ((!setNull) ? this : null);
		}
	}

	protected override float getDamageReductionHack()
	{
		return 1f;
	}

	protected override string getEquipmentTag()
	{
		return owner.myLimbTag;
	}

	protected override int getEquipmentLayer()
	{
		return LayerMask.NameToLayer(owner.myLimbTag);
	}

	protected override EquipmentItem getUserEquipmentOfThisType()
	{
		return owner.currentWeapon;
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Translate(Vector3.left * 0.35f, base.transform);
	}

	public virtual void throwAt(Vector3 target)
	{
		target.y += 3f;
		doThrow(target);
	}

	public virtual void doThrow(Vector3 target)
	{
		base.GetComponent<Rigidbody>().isKinematic = false;
		detachItem(false);
		base.GetComponent<Rigidbody>().AddRelativeTorque(Vector3.forward * 500f, ForceMode.Impulse);
		base.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(target - base.transform.position) * 50f, ForceMode.Impulse);
		playDetatchSound();
	}

	public void doDamage()
	{
		doDamage(1f);
	}

	public virtual void doAttack(int tapCount)
	{
		owner.doAttack(tapCount);
	}

	public override string getPlaceHolderName()
	{
		return "ObjectPlaceHolder";
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "damage", damage);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		damage = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "damage");
	}

	public override void detachItem(bool reposition)
	{
		owner.onWalkPressed(Vector3.zero);
		base.detachItem(reposition);
	}

	public override bool useItem(AttackComponent player)
	{
		bool result = base.useItem(player);
		owner.onWalkPressed(Vector3.zero);
		return result;
	}

	public override bool putAwayItem()
	{
		bool flag = base.putAwayItem();
		if (flag)
		{
			owner.onWalkPressed(Vector3.zero);
		}
		return flag;
	}

	public override void doUpgrade(ItemUpgrader upgrader)
	{
		damage += upgrader.upgradeValue;
	}
}
