using UnityEngine;

public class RightElbowArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_001_R_002";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 270f));
		base.transform.Translate(Vector3.down * 0.4f);
	}
}
