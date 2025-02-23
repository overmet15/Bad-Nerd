using System.Collections.Generic;
using UnityEngine;

public class ChainedWeapon : Weapon
{
	private Transform myTransform;

	private List<Transform> chainList = new List<Transform>();

	protected override void Awake()
	{
		base.Awake();
		myTransform = base.transform;
		for (int i = 0; i < myTransform.GetChildCount(); i++)
		{
			Transform child = myTransform.GetChild(i);
			chainList.Add(child);
			child.GetComponent<WeaponChild>().Weapon = this;
		}
	}

	public override void toggleCollider(bool on)
	{
		base.toggleCollider(on);
		foreach (Transform chain in chainList)
		{
			if ((bool)chain.GetComponent<Collider>())
			{
				chain.GetComponent<Collider>().enabled = on;
			}
		}
	}

	protected override void setKinematic(bool on)
	{
		base.setKinematic(on);
		foreach (Transform chain in chainList)
		{
			chain.GetComponent<Rigidbody>().isKinematic = on;
		}
	}

	private void parent(bool connect, Transform p)
	{
		foreach (Transform chain in chainList)
		{
			if (connect)
			{
				chain.parent = p;
				chain.GetComponent<Rigidbody>().drag = 0f;
				Debug.Log(string.Concat("parenting: ", chain, " ", chain.parent));
			}
			else
			{
				chain.parent = null;
				chain.GetComponent<Rigidbody>().drag = 2.5f;
				Debug.Log(string.Concat("unparenting: ", chain, " ", chain.parent));
			}
		}
	}

	public override Vector3 getSize()
	{
		return base.getSize() * 2.5f;
	}

	public override bool useItem(AttackComponent player)
	{
		bool flag = base.useItem(player);
		if (flag)
		{
			parent(false, myTransform);
			foreach (Transform chain in chainList)
			{
				chain.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
		return flag;
	}

	public override bool putAwayItem()
	{
		parent(true, myTransform);
		bool flag = base.putAwayItem();
		if (!flag)
		{
			parent(false, myTransform);
		}
		return flag;
	}

	public override void detachItem(bool reposition)
	{
		parent(true, myTransform);
		base.detachItem(reposition);
	}

	protected override void setTagAndLayer(string tag, int layer)
	{
		base.setTagAndLayer(tag, layer);
		for (int i = 0; i < myTransform.GetChildCount(); i++)
		{
			Transform child = myTransform.GetChild(i);
			child.tag = tag;
			child.gameObject.layer = layer;
		}
	}

	public override void carryItemsAcrossLevel(Transform p)
	{
		parent(true, myTransform);
		base.carryItemsAcrossLevel(p);
	}

	public override void unCarryItemsAcrossLevel(Transform p)
	{
		base.unCarryItemsAcrossLevel(p);
		if (isEquipped())
		{
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				BadNerdItem.ignoreCapacity = true;
				VNLUtil.soundFXOff = true;
				ChainedWeapon chainedWeapon2 = clone();
				detachItem(false);
				VNLUtil.toggleAll(base.transform, false);
				chainedWeapon2.bePickedUp(owner);
				Object.Destroy(base.gameObject, 1f);
				VNLUtil.soundFXOff = false;
				BadNerdItem.ignoreCapacity = false;
			});
			return;
		}
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			BadNerdItem.ignoreCapacity = true;
			VNLUtil.soundFXOff = true;
			ChainedWeapon chainedWeapon = clone();
			detachItem(false);
			VNLUtil.toggleAll(base.transform, false);
			chainedWeapon.bePickedUp(owner);
			Object.Destroy(base.gameObject, 1f);
			if (chainedWeapon.isEquipped())
			{
				chainedWeapon.putAwayItem();
			}
			VNLUtil.soundFXOff = false;
			BadNerdItem.ignoreCapacity = false;
		});
	}

	public ChainedWeapon clone()
	{
		ChainedWeapon chainedWeapon = VNLUtil.instantiate<ChainedWeapon>(base.name);
		chainedWeapon.life = life;
		chainedWeapon.maxLife = maxLife;
		chainedWeapon.owner = owner;
		chainedWeapon.damage = damage;
		chainedWeapon.isFixable = isFixable;
		return chainedWeapon;
	}
}
