using UnityEngine;

public class WeaponChild : MonoBehaviour
{
	private Weapon weapon;

	public Rigidbody connection;

	public Weapon Weapon
	{
		get
		{
			return weapon;
		}
		set
		{
			weapon = value;
		}
	}

	private void Awake()
	{
		connection = GetComponent<ConfigurableJoint>().connectedBody;
	}

	public void connect(bool on)
	{
		if (on)
		{
			GetComponent<ConfigurableJoint>().connectedBody = connection;
		}
		else
		{
			GetComponent<ConfigurableJoint>().connectedBody = null;
		}
	}

	public Weapon getWeapon()
	{
		return weapon;
	}

	public AttackComponent getOwner()
	{
		return getWeapon().owner;
	}
}
