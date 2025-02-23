using UnityEngine;

public class Helmet : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_003";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 90f));
		base.transform.Translate(Vector3.up * 1.6f);
		base.transform.Translate(Vector3.forward * 0.2f);
	}
}
