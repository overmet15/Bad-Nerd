public class GenericQuestGiver : QuestGiver
{
	public override void init(bool fromSceneLoad)
	{
		GenericQuest genericQuest = (GenericQuest)quests[0];
		genericQuest.setup(this);
		updateQuestObjectOfMyPosition();
		base.init(fromSceneLoad);
	}

	public void updateQuestObjectOfMyPosition()
	{
		foreach (GenericQuest quest in quests)
		{
			quest.giverPosition = base.transform.position;
		}
	}
}
