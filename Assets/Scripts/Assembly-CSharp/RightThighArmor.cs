using UnityEngine;

public class RightThighArmor : Armor
{
	protected override string getNameOfBoneToHoldEquipment()
	{
		return "Bone_R";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.adjustEquipmentPosition(bone);
		base.transform.Rotate(new Vector3(0f, 0f, 290f));
		base.transform.Rotate(new Vector3(0f, -125f, 0f));
		base.transform.Rotate(new Vector3(10f, 0f, 0f));
		base.transform.Translate(Vector3.down * 0.9f);
		base.transform.Translate(Vector3.forward * 1f);
		base.transform.Translate(Vector3.right * 0.2f);
	}
}
