using UnityEngine;

public class AbstractUI : MonoBehaviour
{
	public AudioClip openSound;

	public AudioClip closeSound;

	protected virtual void Awake()
	{
		playOpenSound();
	}

	protected void playOpenSound()
	{
		VNLUtil.getInstance().playAudio(openSound, true);
	}

	protected void playCloseSound()
	{
		VNLUtil.getInstance().playAudio(closeSound, true);
	}
}
