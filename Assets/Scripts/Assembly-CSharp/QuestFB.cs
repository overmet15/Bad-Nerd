public class QuestFB : Quest
{
	public bool isRequest;

	protected override void onQuestCompleted()
	{
		base.onQuestCompleted();
		if (isRequest)
		{
			//APIService.requestToFB("This new game is absolutely amazing!  If you hate bullies then this game is for you.  Check it out now!");
		}
		else
		{
			APIService.postToFB();
		}
	}
}
