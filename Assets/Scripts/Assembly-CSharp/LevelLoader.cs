using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
	private UISlider bar;

	private AsyncOperation async;

	private void Awake()
	{
		bar = GameObject.Find("LevelLoaderSlider").GetComponent<UISlider>();
	}

	private void Update()
	{
		if (async != null)
		{
			bar.sliderValue = async.progress;
		}
	}

	public void loadLevel(string level)
	{
		VNLUtil.getInstance().activeUIID = -1;
		StartCoroutine(loadTheLevel(level));
	}

	private IEnumerator loadTheLevel(string level)
	{
		async = Application.LoadLevelAsync(level);
		yield return async;
	}
}
