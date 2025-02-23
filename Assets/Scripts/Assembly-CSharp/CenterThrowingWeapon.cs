using UnityEngine;

public class CenterThrowingWeapon : ThrowingWeapon
{
	public override string getPlaceHolderName()
	{
		return "ObjectPlaceHolder2";
	}

	protected override void adjustEquipmentPosition(GameObject bone)
	{
		base.transform.Translate(Vector3.left * 0.35f, base.transform);
	}
}
