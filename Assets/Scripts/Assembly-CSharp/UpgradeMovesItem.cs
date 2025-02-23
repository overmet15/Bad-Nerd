using System;
using UnityEngine;

public class UpgradeMovesItem : UnGroupedItem
{
	public bool isCounterAttack;

	public bool isWeapon;

	public int attackToUpgrade;

	public int requiredLevel;

	private Transform mainBone;

	protected override void Awake()
	{
		mainBone = base.transform.Find("Armature").Find("mainBone");
		foreach (AnimationState item in base.GetComponent<Animation>())
		{
			item.speed = 1f / Time.timeScale;
		}
	}

	public override string getPlaceHolderName()
	{
		return "ObjectPlaceHolder3";
	}

	public override bool bePickedUp(AttackComponent player)
	{
		bool flag = false;
		string txt = "haveUpgradeAlready";
		if (isCounterAttack)
		{
			string placeHolder = "Counter Atk Lvl " + requiredLevel;
			string text = "levelTooLow";
			if (player.counterAttackLevel < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text }, null, true, true, placeHolder);
			}
			else if (player.counterAttackLevel > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (player.counterAttackLevel == requiredLevel)
			{
				player.counterAttackLevel++;
				flag = true;
			}
		}
		else if (isWeapon)
		{
			string placeHolder2 = attackToUpgrade + "-Tap Wpn Lvl " + requiredLevel;
			string text2 = "levelTooLow";
			if (attackToUpgrade == 1 && player.weaponAttack1Level < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text2 }, null, true, true, placeHolder2);
			}
			else if (attackToUpgrade == 1 && player.weaponAttack1Level > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (attackToUpgrade == 1 && player.weaponAttack1Level == requiredLevel)
			{
				player.weaponAttack1Level++;
				flag = true;
			}
			else if (attackToUpgrade == 2 && player.weaponAttack2Level < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text2 }, null, true, true, placeHolder2);
			}
			else if (attackToUpgrade == 2 && player.weaponAttack2Level > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (attackToUpgrade == 2 && player.weaponAttack2Level == requiredLevel)
			{
				player.weaponAttack2Level++;
				flag = true;
			}
			else if (attackToUpgrade == 3 && player.weaponAttack3Level < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text2 }, null, true, true, placeHolder2);
			}
			else if (attackToUpgrade == 3 && player.weaponAttack3Level > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (attackToUpgrade == 3 && player.weaponAttack3Level == requiredLevel)
			{
				player.weaponAttack3Level++;
				flag = true;
			}
		}
		else
		{
			string placeHolder3 = attackToUpgrade + "-Tap Atk Lvl " + requiredLevel;
			string text3 = "levelTooLow";
			if (attackToUpgrade == 1 && player.attack1Level < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text3 }, null, true, true, placeHolder3);
			}
			else if (attackToUpgrade == 1 && player.attack1Level > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (attackToUpgrade == 1 && player.attack1Level == requiredLevel)
			{
				player.attack1Level++;
				flag = true;
			}
			else if (attackToUpgrade == 2 && player.attack2Level < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text3 }, null, true, true, placeHolder3);
			}
			else if (attackToUpgrade == 2 && player.attack2Level > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (attackToUpgrade == 2 && player.attack2Level == requiredLevel)
			{
				player.attack2Level++;
				flag = true;
			}
			else if (attackToUpgrade == 3 && player.attack3Level < requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { text3 }, null, true, true, placeHolder3);
			}
			else if (attackToUpgrade == 3 && player.attack3Level > requiredLevel)
			{
				VNLUtil.getInstance().displayMessage(txt, true);
			}
			else if (attackToUpgrade == 3 && player.attack3Level == requiredLevel)
			{
				player.attack3Level++;
				flag = true;
			}
		}
		if (flag)
		{
			VNLUtil.getInstance().playAudio(useSound, true);
		}
		UnityEngine.Object.Destroy(base.gameObject);
		return flag;
	}

	public override Mesh getMesh()
	{
		return mainBone.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
	}

	protected override Renderer getRenderer()
	{
		return mainBone.GetComponent<Renderer>();
	}

	public override string getStats()
	{
		if (isCounterAttack)
		{
			return "Temporarily disables your enemies";
		}
		if (isWeapon)
		{
			return "Weapon Damage: x" + attackToUpgrade * (requiredLevel + 1);
		}
		return "Damage: x" + attackToUpgrade * (requiredLevel + 1);
	}

	public override string getCountLabel()
	{
		bool flag = false;
		PlayerAttackComponent component = GameObject.Find("Player").GetComponent<PlayerAttackComponent>();
		if (isCounterAttack)
		{
			if (component.counterAttackLevel >= requiredLevel + 1)
			{
				flag = true;
			}
		}
		else if (isWeapon)
		{
			if (attackToUpgrade == 1 && component.weaponAttack1Level >= requiredLevel + 1)
			{
				flag = true;
			}
			else if (attackToUpgrade == 2 && component.weaponAttack2Level >= requiredLevel + 1)
			{
				flag = true;
			}
			else if (attackToUpgrade == 3 && component.weaponAttack3Level >= requiredLevel + 1)
			{
				flag = true;
			}
		}
		else if (attackToUpgrade == 1 && component.attack1Level >= requiredLevel + 1)
		{
			flag = true;
		}
		else if (attackToUpgrade == 2 && component.attack2Level >= requiredLevel + 1)
		{
			flag = true;
		}
		else if (attackToUpgrade == 3 && component.attack3Level >= requiredLevel + 1)
		{
			flag = true;
		}
		if (flag)
		{
			return "You have this";
		}
		return null;
	}

	public override string getStatusLabel()
	{
		throw new NotImplementedException();
	}
}
