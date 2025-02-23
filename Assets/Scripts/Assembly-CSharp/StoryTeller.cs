using System.Collections.Generic;
using UnityEngine;

public class StoryTeller : MonoBehaviour
{
	public List<string> storyTexts;

	public string levelToGoTo;

	private int numberOfTextsShown;

	private int numberOfTextsOKed;

	public bool pause;

	public void showPanel(int numberOfTextsToShow)
	{
		displayDialog(storyTexts.GetRange(numberOfTextsShown, numberOfTextsToShow).ToArray());
		numberOfTextsShown += numberOfTextsToShow;
	}

	protected IMessageUI displayDialog(string[] msgs)
	{
		return VNLUtil.getInstance().displayMessage(msgs, delegate
		{
			numberOfTextsOKed += msgs.Length;
			onOK();
		}, false, pause, null).setTitle("Story");
	}

	protected virtual void onOK()
	{
		if (numberOfTextsOKed == storyTexts.Count)
		{
			VNLUtil.getInstance<LevelLoader>().loadLevel(levelToGoTo);
		}
	}

	public void playActionMusic()
	{
		VNLUtil.getInstance().playActionMusic();
	}

	public void playRelaxMusicMusic()
	{
		VNLUtil.getInstance().changeEpisodeMusic(0);
		VNLUtil.getInstance().playNormalMusicForced(1);
	}
}
