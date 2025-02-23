using UnityEngine;

public class LeftShoulderArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_001_L_001";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 270f));
	}
}
