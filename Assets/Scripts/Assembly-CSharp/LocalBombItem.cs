using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBombItem : GroupedItem
{
	public ParticleSystem[] particlesWhenExecuted;

	public float range = 10f;

	public int loops = 3;

	public float damage;

	public float energyDrain;

	public AudioClip explosionSound;

	private bool activated;

	private bool exploded;

	public override bool bePickedUp(AttackComponent player)
	{
		if (exploded)
		{
			return false;
		}
		return base.bePickedUp(player);
	}

	public override bool useItem(AttackComponent player)
	{
		base.useItem(player);
		activateBomb(owner.transform);
		return true;
	}

	public void activateBomb(Transform parent)
	{
		LocalBombItem localBombItem = VNLUtil.instantiate<LocalBombItem>(base.name);
		localBombItem.transform.position = parent.position;
		localBombItem.activated = true;
		localBombItem.owner = owner;
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (activated && collision.transform.gameObject.layer == LayerMask.NameToLayer("Default"))
		{
			ParticleSystem[] array = particlesWhenExecuted;
			foreach (ParticleSystem original in array)
			{
				ParticleSystem particleSystem = Object.Instantiate(original) as ParticleSystem;
				particleSystem.transform.position = base.transform.position;
			}
			exploded = true;
			activated = false;
			StartCoroutine(affectEnemies());
			VNLUtil.getInstance().playAudio(explosionSound, false);
		}
	}

	public IEnumerator affectEnemies()
	{
		for (int i = 0; i < loops; i++)
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
			List<AttackComponent> list = new List<AttackComponent>();
			GameObject[] array = enemies;
			foreach (GameObject go in array)
			{
				AttackComponent enemy = go.GetComponent<EnemyAttackComponent>();
				list.Add(enemy);
			}
			GameObject[] array2 = players;
			foreach (GameObject go2 in array2)
			{
				AttackComponent p = go2.GetComponent<PlayerAttackComponent>();
				list.Add(p);
			}
			foreach (AttackComponent enemy2 in list)
			{
				if (!enemy2.isDead && !(enemy2 == owner))
				{
					float enemyDistance = Vector3.Distance(base.transform.position, enemy2.transform.position);
					if (enemyDistance < range)
					{
						doEffect(enemy2);
					}
				}
			}
			yield return new WaitForSeconds(0.5f);
		}
		Object.Destroy(base.gameObject);
	}

	protected virtual void doEffect(AttackComponent enemy)
	{
		enemy.beStunned();
		if (damage > 0f)
		{
			enemy.setLife(enemy.life - enemy.life * damage);
		}
		if (energyDrain > 0f)
		{
			enemy.setEnergy(enemy.energy - enemy.energy * energyDrain);
		}
	}

	public override string getStats()
	{
		return base.getStats() + "Damage: " + damage * 100f * (float)loops + "%   Drain: " + energyDrain * 100f * (float)loops + "%   ";
	}
}
