using System;
using System.Collections.Generic;
using UnityEngine;

public class GenericQuest : Quest
{
	private List<EnemyAttackComponent> enemies;

	public int minEnemies;

	public int maxEnemies;

	public string onlyForThisScene;

	[NonSerialized]
	public Vector3 giverPosition;

	public Mesh giverMesh;

	public Quest prerequisite;

	public int enemyMinStrength = 1;

	public int enemyMaxStrength = 1;

	public virtual void setup(GenericQuestGiver giver)
	{
		assignMesh(giver);
	}

	private void assignMesh(GenericQuestGiver giver)
	{
		giver.transform.Find("Armature/mainBone").GetComponent<SkinnedMeshRenderer>().sharedMesh = giverMesh;
	}

	protected override void spawnQuestObjects()
	{
		SpawnManager component = GameObject.Find(Application.loadedLevelName + "SpawnManager").GetComponent<SpawnManager>();
		enemies = component.spawnPrefabs;
		int num = (objectCount = UnityEngine.Random.Range(minEnemies, maxEnemies + 1));
		Debug.Log("init generic quest object Count: " + objectCount);
		for (int i = 0; i < num; i++)
		{
			EnemyAttackComponent enemyAttackComponent = enemies[UnityEngine.Random.Range(0, enemies.Count)];
			EnemyAttackComponent enemyAttackComponent2 = UnityEngine.Object.Instantiate(enemyAttackComponent) as EnemyAttackComponent;
			enemyAttackComponent2.gameObject.AddComponent(typeof(QuestObject));
			enemyAttackComponent2.name = enemyAttackComponent.name;
			QuestObject component2 = enemyAttackComponent2.GetComponent<QuestObject>();
			component2.completeByDeath = true;
			component2.distanceToStartTalk = 10f;
			component2.quest = this;
			component2.showArrow = true;
			Transform randomSpawnSpot = component.getRandomSpawnSpot();
			while (randomSpawnSpot.position == giverPosition)
			{
				randomSpawnSpot = component.getRandomSpawnSpot();
			}
			enemyAttackComponent2.transform.position = randomSpawnSpot.position;
			enemyAttackComponent2.transform.rotation = randomSpawnSpot.rotation;
			int num2 = UnityEngine.Random.Range(enemyMinStrength, enemyMaxStrength + 1);
			enemyAttackComponent2.strength = num2;
			enemyAttackComponent2.maxLife *= num2;
			enemyAttackComponent2.life = enemyAttackComponent2.maxLife;
			component2.init();
		}
	}
}
