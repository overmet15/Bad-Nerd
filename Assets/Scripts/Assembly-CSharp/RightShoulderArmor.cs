using UnityEngine;

public class RightShoulderArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_001_R_001";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 270f));
	}
}
