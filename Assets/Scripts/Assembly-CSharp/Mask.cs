using UnityEngine;

public class Mask : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_003";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 90f));
		base.transform.Translate(Vector3.up * 0.5f);
		base.transform.Translate(Vector3.forward * 1.2f);
		base.transform.Rotate(new Vector3(10f, 0f, 0f));
	}
}
