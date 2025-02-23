using UnityEngine;

public abstract class GroupedItem : BadNerdItem
{
	public int count = 1;

	public override bool bePickedUp(AttackComponent player)
	{
		if (!base.bePickedUp(player))
		{
			return false;
		}
		BadNerdItem badNerdItem = null;
		foreach (BadNerdItem item in player.itemList)
		{
			if (item.name.Equals(base.name))
			{
				badNerdItem = item;
				break;
			}
		}
		if (badNerdItem == null)
		{
			badNerdItem = this;
			player.itemList.Add(this);
		}
		else
		{
			((GroupedItem)badNerdItem).count += count;
			Object.Destroy(base.gameObject);
		}
		return true;
	}

	public override bool useItem(AttackComponent player)
	{
		count--;
		if (count == 0)
		{
			player.itemList.Remove(this);
			Object.Destroy(base.gameObject);
		}
		base.useItem(player);
		return true;
	}

	public override string getStatusLabel()
	{
		return count.ToString();
	}

	public override float getWeight()
	{
		return base.getWeight() * (float)count;
	}

	public override void saveInItemList(int itemListIndex)
	{
		base.saveInItemList(itemListIndex);
		ItemSerializer.SetInt("saveItemList" + itemListIndex + "count", count);
	}

	public override void loadToItemList(int itemListIndex)
	{
		base.loadToItemList(itemListIndex);
		count = ItemSerializer.GetInt("saveItemList" + itemListIndex + "count");
	}
}
