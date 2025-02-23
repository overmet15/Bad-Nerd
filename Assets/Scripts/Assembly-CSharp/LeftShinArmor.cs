using UnityEngine;

public class LeftShinArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_L_001";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(28f, 180f, 90f));
		base.transform.Translate(new Vector3(0f, -0.85f, 0.2f));
	}
}
