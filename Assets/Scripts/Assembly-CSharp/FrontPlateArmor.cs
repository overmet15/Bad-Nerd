using UnityEngine;

public class FrontPlateArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_001";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 90f));
		base.transform.Translate(Vector3.forward * 0.9f);
		base.transform.Rotate(new Vector3(-10f, 0f, 0f));
	}
}
