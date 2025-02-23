using UnityEngine;

public class BackPack : Armor
{
	public float capacity = 50f;

	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_001";
	}

	public override string getStats()
	{
		return base.getStats() + "\nCapacity: " + capacity + "   ";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 90f));
		base.transform.Translate(Vector3.back * 1f);
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "capacity", capacity);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		float @float = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "capacity");
		if (@float > 0f)
		{
			capacity = @float;
		}
	}
}
