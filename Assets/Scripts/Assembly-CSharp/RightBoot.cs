using UnityEngine;

public class RightBoot : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_R_002";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(220f, 0f, -90f));
		base.transform.Rotate(new Vector3(0f, -15f, 0f));
		base.transform.Translate(new Vector3(0f, -1.03f, 0.5f));
	}
}
