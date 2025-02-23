public class QuestForInventoryTutorial : Quest
{
	public override void init()
	{
		getRootQuest().Player.tutorialInventoryTrigger = delegate
		{
			if (objectCount == 0)
			{
				onQuestCompleted();
				getRootQuest().Player.tutorialInventoryTrigger = null;
			}
		};
		base.init();
	}
}
