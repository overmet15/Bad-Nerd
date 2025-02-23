using System;
using UnityEngine;

public class BreakableProp : AttackComponent
{
	public Mesh brokenMesh;

	protected override void Awake()
	{
		givesItemWhenDie = true;
		anim = base.GetComponent<Animation>();
		attackerLimbTag = "playerLimb";
		base.Awake();
	}

	protected override int getPercentDropRate()
	{
		return 10;
	}

	public override void pickupItem(GameObject go)
	{
	}

	protected override void playFightSound()
	{
		throw new NotImplementedException();
	}

	protected override void initCharacter()
	{
	}

	protected override void die()
	{
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
		base.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
		base.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = brokenMesh;
		animateHurt();
		base.die();
	}

	protected override void animateHurt()
	{
		anim.CrossFade(AttackComponent.ANIM_HURT + "1");
	}

	protected override float getMaxSpeed()
	{
		return 0f;
	}

	protected override void toggleAttackColliders(bool on)
	{
	}

	protected override void animateDeath()
	{
	}

	protected override AttackComponent getEnemyToLookAtBeforeAttacking()
	{
		return null;
	}

	protected override void playPunchSound()
	{
		VNLUtil.getInstance().playMetalPunchSound();
	}
}
