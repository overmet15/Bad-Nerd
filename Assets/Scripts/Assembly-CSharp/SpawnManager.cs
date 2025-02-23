using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	private class SpawnedInstanceProxy
	{
		public string name;

		public Vector3 lastKnownPosition;

		public float life;

		public bool isDead;

		public bool isEnemyWithPlayer;

		public Vector3 headedWaypoint;

		public Quaternion rotation;
	}

	private float lastActiveTime;

	private List<SpawnedInstanceProxy> spawnedInstances;

	private List<AttackComponent> actualCurrentLevelSpawns;

	public float amountOfTimeBeforeReset;

	public int numberOfCharactersToSpawn;

	public List<EnemyAttackComponent> spawnPrefabs;

	private List<Vector3> waypoints;

	private List<Vector3> spawnSpots;

	public bool spawnUsingRandomRange;

	public Quest prerequisiteForSpawn;

	public List<Vector3> Waypoints
	{
		get
		{
			return waypoints;
		}
	}

	private void Awake()
	{
		GameObject gameObject = GameObject.Find(Application.loadedLevelName + "SpawnManager");
		if (gameObject != null && gameObject != base.gameObject)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.name = Application.loadedLevelName + "SpawnManager";
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void init()
	{
		if (lastActiveTime < Time.time - amountOfTimeBeforeReset)
		{
			Debug.Log(base.name + " reset");
			spawnedInstances = null;
		}
		actualCurrentLevelSpawns = new List<AttackComponent>();
		if (spawnedInstances == null)
		{
			createNewBatch();
		}
		else
		{
			reCreateCharacters();
		}
	}

	public void avengeInnocent()
	{
		if (actualCurrentLevelSpawns == null)
		{
			return;
		}
		foreach (EnemyAttackComponent actualCurrentLevelSpawn in actualCurrentLevelSpawns)
		{
			actualCurrentLevelSpawn.temporarilyBecomeHostile(120f);
		}
	}

	public void unSpawn()
	{
		if (actualCurrentLevelSpawns != null)
		{
			foreach (AttackComponent actualCurrentLevelSpawn in actualCurrentLevelSpawns)
			{
				UnityEngine.Object.Destroy(actualCurrentLevelSpawn.gameObject);
			}
		}
		actualCurrentLevelSpawns = new List<AttackComponent>();
	}

	private void createNewBatch()
	{
		Debug.Log(base.name + " createNewBatch()");
		spawnedInstances = new List<SpawnedInstanceProxy>();
		if (spawnSpots == null)
		{
			spawnSpots = new List<Vector3>();
			GameObject[] array = GameObject.FindGameObjectsWithTag("spawnSpot");
			GameObject[] array2 = array;
			foreach (GameObject gameObject in array2)
			{
				if (!(gameObject.transform.parent != base.transform))
				{
					spawnSpots.Add(gameObject.transform.position);
				}
			}
		}
		int num = 0;
		int num2 = numberOfCharactersToSpawn;
		if (prerequisiteForSpawn != null && !VNLUtil.getInstance<PlayerAttackComponent>("Player").isQuestComplete(prerequisiteForSpawn))
		{
			Debug.Log(base.name + " prerequisite not met, not spawning.");
			num2 = 0;
		}
		else if (spawnUsingRandomRange)
		{
			num2 = UnityEngine.Random.Range(0, numberOfCharactersToSpawn + 1);
			Debug.Log(base.name + " spawning random count of " + num2);
		}
		Debug.Log(base.name + " numberToSpawn: " + num2);
		int num3 = 0;
		while (num < num2)
		{
			if (num3 == spawnSpots.Count)
			{
				num3 = 0;
			}
			Vector3 spot = spawnSpots[num3];
			EnemyAttackComponent prefab = spawnPrefabs[UnityEngine.Random.Range(0, spawnPrefabs.Count)];
			create(prefab, spot);
			num++;
			num3++;
		}
		if (waypoints != null)
		{
			return;
		}
		waypoints = new List<Vector3>();
		GameObject[] array3 = GameObject.FindGameObjectsWithTag("randomWaypoint");
		GameObject[] array4 = array3;
		foreach (GameObject gameObject2 in array4)
		{
			if (!(gameObject2.transform.parent != base.transform))
			{
				waypoints.Add(gameObject2.transform.position);
			}
		}
	}

	private void reCreateCharacters()
	{
		Debug.Log(base.name + " reCreateCharacters()");
		List<SpawnedInstanceProxy> list = spawnedInstances;
		spawnedInstances = new List<SpawnedInstanceProxy>();
		foreach (SpawnedInstanceProxy item in list)
		{
			if (!item.isDead)
			{
				Debug.Log("re-creating: " + item.name);
				EnemyAttackComponent enemyAttackComponent = VNLUtil.instantiate<EnemyAttackComponent>(item.name);
				enemyAttackComponent.transform.position = item.lastKnownPosition;
				enemyAttackComponent.setLife(item.life);
				enemyAttackComponent.isEnemyWithPlayer = item.isEnemyWithPlayer;
				enemyAttackComponent.transform.rotation = item.rotation;
				store(enemyAttackComponent);
				Debug.Log("re-created: " + enemyAttackComponent);
			}
		}
	}

	private EnemyAttackComponent create(EnemyAttackComponent prefab, Vector3 spot)
	{
		Debug.Log("creating: " + prefab);
		EnemyAttackComponent enemyAttackComponent = UnityEngine.Object.Instantiate(prefab) as EnemyAttackComponent;
		enemyAttackComponent.name = prefab.name;
		enemyAttackComponent.transform.position = spot;
		store(enemyAttackComponent);
		Debug.Log("created: " + enemyAttackComponent);
		return enemyAttackComponent;
	}

	private void store(EnemyAttackComponent instance)
	{
		SpawnedInstanceProxy proxy = new SpawnedInstanceProxy();
		spawnedInstances.Add(proxy);
		EnemyAttackComponent enemyAttackComponent = instance;
		enemyAttackComponent.updateData = (AttackComponent.UpdateData)Delegate.Combine(enemyAttackComponent.updateData, (AttackComponent.UpdateData)delegate
		{
			proxy.name = instance.name;
			proxy.life = instance.life;
			proxy.lastKnownPosition = instance.transform.position;
			proxy.isDead = instance.isDead;
			proxy.isEnemyWithPlayer = instance.isEnemyWithPlayer;
			proxy.rotation = instance.transform.rotation;
			lastActiveTime = Time.time;
			if (proxy.isDead)
			{
				spawnedInstances.Remove(proxy);
				actualCurrentLevelSpawns.Remove(instance);
			}
		});
		EnemyAttackComponent enemyAttackComponent2 = instance;
		enemyAttackComponent2.updateWaypoint = (EnemyAttackComponent.UpdateWaypoint)Delegate.Combine(enemyAttackComponent2.updateWaypoint, (EnemyAttackComponent.UpdateWaypoint)delegate(Vector3 waypoint)
		{
			proxy.headedWaypoint = waypoint;
		});
		actualCurrentLevelSpawns.Add(instance);
	}

	public bool isWaypointTaken(Vector3 waypoint)
	{
		foreach (SpawnedInstanceProxy spawnedInstance in spawnedInstances)
		{
			if (spawnedInstance.headedWaypoint.Equals(waypoint))
			{
				return true;
			}
		}
		return false;
	}

	public Transform getRandomSpawnSpot()
	{
		List<Transform> list = new List<Transform>();
		GameObject[] array = GameObject.FindGameObjectsWithTag("spawnSpot");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			if (!(gameObject.transform.parent != base.transform))
			{
				list.Add(gameObject.transform);
			}
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}
}
