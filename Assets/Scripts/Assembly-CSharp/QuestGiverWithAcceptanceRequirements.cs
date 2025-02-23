public class QuestGiverWithAcceptanceRequirements : QuestGiver
{
	protected override void acceptQuest()
	{
		if (typeof(QuestWithPostAcceptReq) != quest.GetType())
		{
			base.acceptQuest();
			return;
		}
		QuestWithPostAcceptReq theQuest = (QuestWithPostAcceptReq)quest;
		if (theQuest.isRequirementMet())
		{
			base.acceptQuest();
		}
		else if (theQuest.postAcceptRequirementsErrMsgs.Count > 0)
		{
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				VNLUtil.getInstance().displayMessage(theQuest.postAcceptRequirementsErrMsgs.ToArray(), null, false, true, null).setTitle(theQuest.name);
			}, 0.5f);
		}
	}
}
