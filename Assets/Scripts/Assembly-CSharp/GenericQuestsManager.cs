using UnityEngine;

public class GenericQuestsManager : MonoBehaviour
{
	public QuestGiver giverTemplate;

	public Quest[] priorityQuests;

	public Quest[] quests;

	private PlayerAttackComponent player;

	public QuestGiver createNextQuestBasedOnCompletedQuest(Quest completedQuest)
	{
		player = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		Quest[] array = quests;
		for (int i = 0; i < array.Length; i++)
		{
			GenericQuest genericQuest = (GenericQuest)array[i];
			if (!player.isQuestComplete(genericQuest) && genericQuest.prerequisite == completedQuest)
			{
				return createQuestGiver(genericQuest);
			}
		}
		return null;
	}

	public QuestGiver createRandomGiver(string exclusion)
	{
		player = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		Quest quest = null;
		Quest[] array = priorityQuests;
		foreach (Quest quest2 in array)
		{
			Debug.Log("test: " + quest2.name);
			if (canInclude(exclusion, quest2))
			{
				quest = quest2;
				break;
			}
		}
		if (quest == null)
		{
			quest = quests[Random.Range(0, quests.Length)];
			int num = 0;
			while (!canInclude(exclusion, quest))
			{
				quest = null;
				num++;
				if (num < 50)
				{
					break;
				}
				quest = quests[Random.Range(0, quests.Length)];
			}
		}
		if (quest == null)
		{
			Quest[] array2 = quests;
			foreach (Quest quest3 in array2)
			{
				if (canInclude(exclusion, quest3))
				{
					quest = quest3;
				}
			}
		}
		if (quest == null)
		{
			return null;
		}
		return createQuestGiver(quest);
	}

	private bool canInclude(string excludeCuzItIsAlreadyCreated, Quest q)
	{
		bool result = true;
		if (excludeCuzItIsAlreadyCreated != null && excludeCuzItIsAlreadyCreated.Contains(q.name + ","))
		{
			result = false;
		}
		if (player.isQuestComplete(q))
		{
			result = false;
		}
		string onlyForThisScene = ((GenericQuest)q).onlyForThisScene;
		if (onlyForThisScene != null && onlyForThisScene.Trim().Length > 0 && !onlyForThisScene.Equals(Application.loadedLevelName))
		{
			result = false;
		}
		Quest prerequisite = ((GenericQuest)q).prerequisite;
		if (prerequisite != null && !player.isQuestComplete(prerequisite))
		{
			result = false;
		}
		return result;
	}

	private QuestGiver createQuestGiver(Quest quest)
	{
		QuestGiver questGiver = (QuestGiver)Object.Instantiate(giverTemplate);
		questGiver.name = quest.name;
		questGiver.quests.Add(quest);
		return questGiver;
	}
}
