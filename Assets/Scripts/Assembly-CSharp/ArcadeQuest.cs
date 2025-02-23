using System.Collections.Generic;
using UnityEngine;

public class ArcadeQuest : GenericQuest
{
	public override void setup(GenericQuestGiver giver)
	{
		string text = "arcQ" + Random.Range(1, 11);
		if (storyTexts == null || storyTexts.Count == 0)
		{
			storyTexts = new List<string>(new string[2] { text, "arcA1" });
		}
		if (giverMesh == null)
		{
			giverMesh = VNLUtil.getInstance().APCMeshes[Random.Range(0, VNLUtil.getInstance().APCMeshes.Length)];
		}
		base.setup(giver);
	}
}
