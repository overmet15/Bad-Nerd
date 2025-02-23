using UnityEngine;

public class LeftBoot : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_L_002";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(28f, 180f, 90f));
		base.transform.Rotate(new Vector3(0f, 5f, 0f));
		base.transform.Translate(new Vector3(0f, -1.03f, 0.5f));
	}
}
