using UnityEngine;

public abstract class EquipmentItem : UnGroupedItem
{
	public bool isFixable;

	public float maxLife = 100f;

	public float life;

	protected override void Awake()
	{
		base.Awake();
		if (life == 0f || life > maxLife)
		{
			life = maxLife;
		}
	}

	protected abstract float getDamageReductionHack();

	public override string getStats()
	{
		return base.getStats() + "Condition: " + (int)life + "/" + maxLife + "   ";
	}

	public override string getStatusLabel()
	{
		return (int)life + "/" + maxLife;
	}

	public bool isBroken()
	{
		return life == 0f;
	}

	public override void becomeDamagedGoods()
	{
		if (!isFixable)
		{
			base.becomeDamagedGoods();
			price /= 10;
			if (price > 50)
			{
				price = 50;
			}
			life = 0f;
		}
	}

	public void equipPrefab(AttackComponent player)
	{
		owner = player;
		EquipmentItem equipmentItem = Object.Instantiate(this) as EquipmentItem;
		equipmentItem.name = base.name;
		setUserEquipmentOfThisType(true);
		equipmentItem.bePickedUp(player);
	}

	public override bool bePickedUp(AttackComponent player)
	{
		if (!base.bePickedUp(player))
		{
			return false;
		}
		if (getUserEquipmentOfThisType() == null)
		{
			useItem(player);
		}
		return true;
	}

	public override bool onInventoryUse(AttackComponent player)
	{
		if (isEquipped())
		{
			bool flag = putAwayItem();
			if (flag)
			{
				announceEquipment(player);
			}
			return flag;
		}
		if (useItem(player))
		{
			return true;
		}
		if (life <= 0f)
		{
			if (isFixable)
			{
				VNLUtil.getInstance().displayMessage(new string[1] { "fixFirst" }, null, true, true, base.name);
			}
			else
			{
				VNLUtil.getInstance().displayMessage(new string[1] { "sellBroken" }, null, true, true, base.name);
			}
		}
		return false;
	}

	public override bool useItem(AttackComponent player)
	{
		if (life == 0f)
		{
			return false;
		}
		if (getUserEquipmentOfThisType() != null && !getUserEquipmentOfThisType().putAwayItem())
		{
			return false;
		}
		GameObject[] array = GameObject.FindGameObjectsWithTag(owner.myLimbTag);
		GameObject gameObject = null;
		GameObject[] array2 = array;
		foreach (GameObject gameObject2 in array2)
		{
			if (gameObject2.name.Equals(getNameOfBoneToHoldEquipment()) && gameObject2.transform.IsChildOf(player.transform))
			{
				gameObject = gameObject2;
				break;
			}
		}
		if (gameObject == null)
		{
			Debug.LogWarning(string.Concat(player, " with limb tag: ", owner.myLimbTag, " has no body part: ", getNameOfBoneToHoldEquipment()));
			return false;
		}
		setTagAndLayer(getEquipmentTag(), getEquipmentLayer());
		Vector3 position = gameObject.transform.position;
		base.transform.localPosition = position;
		base.transform.localRotation = gameObject.transform.rotation;
		adjustEquipmentPosition(gameObject);
		base.transform.parent = gameObject.transform;
		setUserEquipmentOfThisType(false);
		base.useItem(player);
		announceEquipment(player);
		return true;
	}

	public void announceEquipment(AttackComponent player)
	{
		if (NetworkCore.isNetworkMode && player.tag.Equals("Player"))
		{
			PlayerAttackComponent playerAttackComponent = (PlayerAttackComponent)player;
			playerAttackComponent.announceEquipments();
		}
	}

	protected virtual void adjustEquipmentPosition(GameObject bone)
	{
	}

	protected virtual string getEquipmentTag()
	{
		return base.tag;
	}

	protected virtual int getEquipmentLayer()
	{
		return owner.gameObject.layer;
	}

	protected virtual void setUserEquipmentOfThisType(bool setNull)
	{
		EquipmentItem userEquipmentOfThisType = getUserEquipmentOfThisType();
		if (userEquipmentOfThisType != null)
		{
			owner.equippedItems.Remove(userEquipmentOfThisType);
		}
		if (!setNull)
		{
			owner.equippedItems.Add(this);
		}
	}

	protected virtual EquipmentItem getUserEquipmentOfThisType()
	{
		return owner.getUserEquipmentOfThisType(GetType());
	}

	public bool isEquippedWithThisType()
	{
		return getUserEquipmentOfThisType() != null;
	}

	public bool isEquipped()
	{
		return owner.equippedItems.Contains(this);
	}

	public override void detachItem(bool reposition)
	{
		if (owner != null && isEquipped())
		{
			reposition = false;
		}
		base.detachItem(reposition);
		if (isEquipped())
		{
			setUserEquipmentOfThisType(true);
			announceEquipment(owner);
		}
	}

	public override bool putAwayItem()
	{
		bool flag = base.putAwayItem();
		if (flag && isEquipped())
		{
			setUserEquipmentOfThisType(true);
		}
		return flag;
	}

	protected abstract string getNameOfBoneToHoldEquipment();

	public override string getInventoryUseButtonLabel()
	{
		if (isEquipped())
		{
			return "Unequip";
		}
		return "Equip";
	}

	public override bool isFixableItem()
	{
		if ((bool)owner.getUserItemOfThisType(typeof(ItemFixer)) && life < maxLife)
		{
			return true;
		}
		return false;
	}

	public override bool isUpgradableItem()
	{
		if (isFixableItem())
		{
			return false;
		}
		if ((bool)owner.getUserItemOfThisType(typeof(ItemUpgrader)))
		{
			return true;
		}
		return false;
	}

	public override bool fix()
	{
		if (!isFixable)
		{
			VNLUtil.getInstance().displayMessage(new string[1] { "sellBroken" }, null, true, true, base.name);
			return false;
		}
		ItemFixer itemFixer = (ItemFixer)owner.getUserItemOfThisType(typeof(ItemFixer));
		life += itemFixer.fixValue;
		if (life > maxLife)
		{
			life = maxLife;
		}
		itemFixer.useItem(owner);
		return true;
	}

	public override bool upgrade()
	{
		if (!isFixable)
		{
			VNLUtil.getInstance().displayMessage(new string[1] { "sellUnfixable" }, null, true, true, base.name);
			return false;
		}
		if (life < maxLife)
		{
			VNLUtil.getInstance().displayMessage(new string[1] { "fixBeforeUpgrade" }, null, true, true, base.name);
			return false;
		}
		ItemUpgrader itemUpgrader = (ItemUpgrader)owner.getUserItemOfThisType(typeof(ItemUpgrader));
		doUpgrade(itemUpgrader);
		itemUpgrader.useItem(owner);
		return true;
	}

	public abstract void doUpgrade(ItemUpgrader upgrader);

	public float doDamage(float damage)
	{
		life -= damage * getDamageReductionHack();
		if (life < 0f)
		{
			life = 0f;
		}
		if (life == 0f)
		{
			bool flag = typeof(PlayerAttackComponent).IsAssignableFrom(owner.GetType());
			if (!isFixable)
			{
				if (isEquipped())
				{
					detachItem(false);
				}
				if (flag)
				{
					VNLUtil.getInstance().displayMessage(new string[1] { "sellBroken" }, null, false, true, base.name);
				}
			}
			else if (isEquipped())
			{
				if (isAbleToFitInCapacity(owner))
				{
					putAwayItem();
					if (flag)
					{
						VNLUtil.getInstance().displayMessage(new string[1] { "placedInBag" }, null, false, true, base.name);
					}
				}
				else
				{
					if (isEquipped())
					{
						detachItem(false);
					}
					if (flag)
					{
						VNLUtil.getInstance().displayMessage(new string[3] { "brokenItemNoRoom_1", "brokenItemNoRoom_2", "brokenItemNoRoom_3" }, delegate
						{
							VNLUtil.getInstance().displayConfirmation("getBackpackNow", delegate
							{
								VNLUtil.getInstance<StoreLoader>("utilityDealer").showStore();
							}, null);
						}, false, true, base.name);
					}
				}
			}
		}
		return life;
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "maxLife", maxLife);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "life", life);
		ItemSerializer.SetInt("saveItemList" + itemListIndex + "isFixable", isFixable ? 1 : 0);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		maxLife = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "maxLife");
		life = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "life");
		isFixable = ItemSerializer.GetInt("saveItemList" + itemListIndex + "isFixable") == 1;
	}
}
