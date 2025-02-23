using System;
using UnityEngine;

public abstract class BadNerdItem : MonoBehaviour
{
	public const string SAVE_ITEM_LIST_PREFIX = "saveItemList";

	public int price = 1;

	public string description;

	public bool spin;

	public float spinSpeed = 1f;

	protected string assignedTag;

	public float carryingWeight = 1f;

	public AudioClip pickupSound;

	public AudioClip useSound;

	public AudioClip detatchSound;

	private bool isOwnedbyPlayer;

	public float hOffset;

	public float vOffset;

	public int storeOrder;

	public bool isForPlayerOnly;

	public bool isInMiscSection;

	public bool isInMiscSectionInInventory;

	public AttackComponent owner;

	public bool dontStripDescriptionLineBreaks;

	private bool detecting = true;

	public static bool ignoreCapacity;

	public virtual Type getCategory()
	{
		bool flag = isInMiscSection;
		if (owner != null)
		{
			flag = isInMiscSectionInInventory;
		}
		if (flag)
		{
			return typeof(EquipmentItem);
		}
		return GetType();
	}

	public virtual float getWeight()
	{
		return carryingWeight;
	}

	protected virtual void Awake()
	{
		assignedTag = base.tag;
	}

	public string getPriceLabel()
	{
		return price + " Bucks";
	}

	public virtual string getCountLabel()
	{
		int playerOwnCount = getPlayerOwnCount();
		if (playerOwnCount > 0)
		{
			return "you have " + playerOwnCount;
		}
		return null;
	}

	public virtual void carryItemsAcrossLevel(Transform parent)
	{
		if (base.transform.parent == null)
		{
			Debug.Log("parenting obj for crossing level: " + base.name);
			base.transform.parent = parent;
		}
	}

	public virtual void becomeDamagedGoods()
	{
	}

	public virtual void unCarryItemsAcrossLevel(Transform parent)
	{
		if (base.transform.parent == parent)
		{
			Debug.Log("un-parenting obj for crossing level: " + base.name);
			base.transform.parent = null;
		}
	}

	public virtual string getStats()
	{
		return "Weight: " + getWeight() + "  ";
	}

	public virtual Vector3 getSize()
	{
		return VNLUtil.getSize(this);
	}

	public string getDetailedDescription()
	{
		if (!dontStripDescriptionLineBreaks)
		{
			description = description.Replace(" \n", "\n").Replace("\n ", "\n").Replace('\n', ' ');
			description = description.Replace(" \\n", "\\n").Replace("\\n ", "\\n").Replace("\\n", " ");
		}
		return getStats() + "\n\n" + description;
	}

	private int getPlayerOwnCount()
	{
		PlayerAttackComponent component = GameObject.Find("Player").GetComponent<PlayerAttackComponent>();
		int num = 0;
		foreach (BadNerdItem item in component.itemList)
		{
			if (item.name.Equals(base.name))
			{
				if (typeof(GroupedItem).IsAssignableFrom(item.GetType()))
				{
					GroupedItem groupedItem = (GroupedItem)item;
					return groupedItem.count;
				}
				num++;
			}
		}
		return num;
	}

	public abstract string getStatusLabel();

	public void onStoreSelect()
	{
		StoreUI component = GameObject.Find("StoreUI").GetComponent<StoreUI>();
		component.selectItem(this);
	}

	public void onInventorySelect()
	{
		InventoryUI component = GameObject.Find("InventoryUI").GetComponent<InventoryUI>();
		component.selectItem(this);
	}

	protected virtual void Update()
	{
		if (spin && getRenderer().isVisible)
		{
			base.gameObject.transform.Rotate(Vector3.up, spinSpeed);
		}
	}

	public virtual bool useItem(AttackComponent player)
	{
		playUseSound();
		return true;
	}

	public bool isAbleToFitInCapacity(AttackComponent player)
	{
		if (ignoreCapacity)
		{
			return true;
		}
		bool flag = true;
		if (typeof(EquipmentItem).IsAssignableFrom(GetType()))
		{
			EquipmentItem equipmentItem = (EquipmentItem)this;
			if (!equipmentItem.isBroken() && !player.getUserEquipmentOfThisType(equipmentItem.GetType()))
			{
				flag = false;
			}
		}
		if (flag && getWeight() > 0f)
		{
			float num = player.getTotalItemWeight(true) + getWeight();
			float num2 = player.capacity;
			BackPack backPack = (BackPack)player.getUserEquipmentOfThisType(typeof(BackPack));
			if (backPack != null)
			{
				num2 += backPack.capacity;
			}
			if (num2 < num)
			{
				return false;
			}
		}
		return true;
	}

	public virtual bool bePickedUp(AttackComponent player)
	{
		if (!detecting)
		{
			return false;
		}
		if (!player.tag.Equals("Player") && isForPlayerOnly)
		{
			return false;
		}
		if (!isAbleToFitInCapacity(player))
		{
			if (typeof(PlayerAttackComponent).IsAssignableFrom(player.GetType()))
			{
				detecting = false;
				VNLUtil.getInstance().doStartCoRoutine(delegate
				{
					detecting = true;
				}, 3f);
				VNLUtil.getInstance().displayMessage("needBackPack", true);
			}
			return false;
		}
		owner = player;
		if (owner.name.Contains("Player"))
		{
			isOwnedbyPlayer = true;
		}
		else
		{
			isOwnedbyPlayer = false;
		}
		setKinematic(true);
		toggleCollider(false);
		putAwayItem();
		playPickupSound();
		return true;
	}

	protected void playPickupSound()
	{
		if (isOwnedbyPlayer)
		{
			VNLUtil.getInstance().playAudio(pickupSound, true);
		}
	}

	protected void playUseSound()
	{
		if (isOwnedbyPlayer)
		{
			VNLUtil.getInstance().playAudio(useSound, true);
		}
	}

	public virtual void toggleCollider(bool on)
	{
		base.GetComponent<Collider>().enabled = on;
	}

	protected virtual void setTagAndLayer(string tag, int layer)
	{
		base.tag = tag;
		base.gameObject.layer = layer;
	}

	public virtual bool putAwayItem()
	{
		if (!isAbleToFitInCapacity(owner))
		{
			if (typeof(PlayerAttackComponent).IsAssignableFrom(owner.GetType()))
			{
				VNLUtil.getInstance().displayMessage(new string[1] { "cannotFit" }, null, true, true, base.name);
			}
			return false;
		}
		setTagAndLayer(assignedTag, LayerMask.NameToLayer("hidden"));
		base.transform.parent = null;
		base.transform.localScale = Vector3.one;
		toggleCollider(false);
		setKinematic(true);
		return true;
	}

	public void pickupPrefab(AttackComponent player)
	{
		owner = player;
		BadNerdItem badNerdItem = UnityEngine.Object.Instantiate(this) as BadNerdItem;
		badNerdItem.name = base.name;
		badNerdItem.bePickedUp(player);
	}

	public virtual void detachItem(bool reposition)
	{
		base.transform.parent = null;
		base.transform.localScale = Vector3.one;
		setTagAndLayer(assignedTag, LayerMask.NameToLayer("Default"));
		setKinematic(false);
		toggleCollider(true);
		if (owner != null)
		{
			owner.itemList.Remove(this);
			if (reposition)
			{
				base.transform.position = owner.transform.position;
			}
		}
		base.GetComponent<Rigidbody>().AddForce(new Vector3(0.7f, 1.2f, 0f), ForceMode.Impulse);
		if (isOwnedbyPlayer)
		{
			playDetatchSound();
		}
	}

	protected virtual void setKinematic(bool on)
	{
		base.GetComponent<Rigidbody>().isKinematic = on;
	}

	protected void playDetatchSound()
	{
		VNLUtil.getInstance().playAudio(detatchSound, true);
	}

	public void destroy()
	{
		if (owner != null)
		{
			owner.itemList.Remove(this);
			owner.currentWeapon = null;
		}
		base.transform.parent = null;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public virtual string getPlaceHolderName()
	{
		return "ObjectPlaceHolder2";
	}

	public virtual string getInventoryUseButtonLabel()
	{
		return "Use";
	}

	public virtual bool onInventoryUse(AttackComponent player)
	{
		useItem(player);
		return true;
	}

	public virtual bool isFixableItem()
	{
		return false;
	}

	public virtual bool isUpgradableItem()
	{
		return false;
	}

	public virtual bool fix()
	{
		return false;
	}

	public virtual bool upgrade()
	{
		return false;
	}

	public virtual Mesh getMesh()
	{
		MeshFilter meshFilter = GetComponent(typeof(MeshFilter)) as MeshFilter;
		return meshFilter.sharedMesh;
	}

	protected virtual Renderer getRenderer()
	{
		return base.GetComponent<Renderer>();
	}

	public virtual void saveInItemList(int itemListIndex)
	{
		ItemSerializer.SetString("saveItemList" + itemListIndex + "tag", base.tag);
		ItemSerializer.SetString("saveItemList" + itemListIndex + "assignedTag", assignedTag);
		ItemSerializer.SetFloat("saveItemList" + itemListIndex + "carryingWeight", carryingWeight);
		ItemSerializer.SetInt("saveItemList" + itemListIndex + "price", price);
	}

	public virtual void loadToItemList(int itemListIndex)
	{
		base.tag = ItemSerializer.GetString("saveItemList" + itemListIndex + "tag");
		assignedTag = ItemSerializer.GetString("saveItemList" + itemListIndex + "assignedTag");
		carryingWeight = ItemSerializer.GetFloat("saveItemList" + itemListIndex + "carryingWeight");
		price = ItemSerializer.GetInt("saveItemList" + itemListIndex + "price");
	}
}
