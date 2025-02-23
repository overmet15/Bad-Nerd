using UnityEngine;

public abstract class Armor : EquipmentItem
{
	public const string ARMOR_TAG = "armor";

	public float defense = 0.03f;

	public override string getStats()
	{
		return base.getStats() + "Defense: " + (defense * 100f).ToString("F1") + "%   ";
	}

	public float getDamageReduction()
	{
		return defense * (life / maxLife);
	}

	protected override float getDamageReductionHack()
	{
		return 0.25f;
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "defense", defense);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		defense = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "defense");
	}

	public override bool putAwayItem()
	{
		bool flag = base.putAwayItem();
		if (flag)
		{
			detectHelmetAndChangeMesh();
		}
		return flag;
	}

	public override bool useItem(AttackComponent player)
	{
		bool flag = base.useItem(player);
		if (flag)
		{
			detectHelmetAndChangeMesh();
		}
		return flag;
	}

	public override void detachItem(bool reposition)
	{
		base.detachItem(reposition);
		detectHelmetAndChangeMesh();
	}

	private void detectHelmetAndChangeMesh()
	{
		bool hasHelmet = owner.getUserEquipmentOfThisType(typeof(Helmet)) != null;
		owner.toggleHelmetMesh(hasHelmet);
	}

	protected override int getEquipmentLayer()
	{
		return LayerMask.NameToLayer("armor");
	}

	public override void doUpgrade(ItemUpgrader upgrader)
	{
		defense += upgrader.upgradeValue * 0.01f;
	}
}
