using UnityEngine;

public class LoaderScene : MonoBehaviour
{
	public string sceneToLoad;

	private void Start()
	{
		VNLUtil.getInstance<LevelLoader>().loadLevel(sceneToLoad);
	}
}
