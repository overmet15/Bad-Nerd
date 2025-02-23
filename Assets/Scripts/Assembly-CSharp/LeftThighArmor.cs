using UnityEngine;

public class LeftThighArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_L";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 250f));
		base.transform.Rotate(new Vector3(0f, 125f, 0f));
		base.transform.Rotate(new Vector3(10f, 0f, 0f));
		base.transform.Translate(Vector3.down * 0.9f);
		base.transform.Translate(Vector3.forward * 0.95f);
		base.transform.Translate(Vector3.left * 0.2f);
	}
}
