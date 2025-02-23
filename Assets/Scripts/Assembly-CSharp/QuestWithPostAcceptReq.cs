using System.Collections.Generic;
using UnityEngine;

public class QuestWithPostAcceptReq : Quest
{
	public GroupedItem requiredObject;

	public int requiredCountOfObject;

	public List<string> postAcceptRequirementsErrMsgs;

	public bool isRequirementMet()
	{
		PlayerAttackComponent component = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttackComponent>();
		GroupedItem groupedItem = (GroupedItem)component.getUserItemOfThisType(requiredObject.GetType());
		if (groupedItem != null && groupedItem.count >= requiredCountOfObject)
		{
			return true;
		}
		return false;
	}
}
