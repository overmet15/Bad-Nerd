using UnityEngine;

public class Grenade : ThrowingWeapon
{
	public LocalBombItem attachedBomb;

	private bool activated;

	public void OnCollisionEnter(Collision collision)
	{
		if (activated)
		{
			base.GetComponent<Collider>().enabled = false;
			attachedBomb.activateBomb(base.transform);
		}
	}

	public override void doThrow(Vector3 target)
	{
		base.doThrow(target);
		activated = true;
		base.GetComponent<Collider>().enabled = false;
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			base.GetComponent<Collider>().enabled = true;
		}, 0.1f);
	}
}
