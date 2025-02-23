using UnityEngine;

public class Map : MonoBehaviour
{
	public float cameraSize = 300f;

	public float cameraAngle;

	private Animation[] roomQuestMarkers;

	private void Awake()
	{
		Transform transform = base.transform.Find("Panel");
		roomQuestMarkers = transform.GetComponentsInChildren<Animation>();
	}

	public void initMarkers()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("exclaimationMark");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			Animation component = gameObject.GetComponent<Animation>();
			component.enabled = true;
			gameObject.transform.parent.rotation = Quaternion.identity;
			gameObject.transform.parent.Rotate(Vector3.right, 90f);
			component["exclaimationSpin"].speed = 1f / Time.timeScale;
			component.Play();
		}
		PlayerAttackComponent instance = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		string questMarker = instance.getQuestMarker();
		Debug.Log("questMarker==" + questMarker);
		questMarker = makeEpisodeAgnostic(questMarker);
		Debug.Log("episode agnostic questMarker==" + questMarker);
		Animation[] array3 = roomQuestMarkers;
		foreach (Animation animation in array3)
		{
			if (questMarker == null || questMarker.Length == 0)
			{
				animation.gameObject.active = false;
				continue;
			}
			animation.gameObject.active = true;
			animation["exclaimationSpin"].speed = 1f / Time.timeScale;
			animation.Play();
			bool flag = false;
			if (questMarker.Contains("|"))
			{
				string[] array4 = questMarker.Split('|');
				string[] array5 = array4;
				foreach (string text in array5)
				{
					Debug.Log("???" + text + "Mark==" + animation.gameObject.name);
					if ((text + "Mark").EndsWith(animation.gameObject.name))
					{
						Debug.Log("YES" + text + "Mark==" + animation.gameObject.name);
						flag = true;
						break;
					}
				}
			}
			else
			{
				Debug.Log("???" + animation.gameObject.name + " == " + questMarker + "Mark");
				if (animation.gameObject.name.Equals(questMarker + "Mark"))
				{
					Debug.Log("YES" + animation.gameObject.name + " == " + questMarker + "Mark");
					flag = true;
				}
			}
			if (!flag || animation.gameObject.name.Replace("Mark", string.Empty).Equals(makeEpisodeAgnostic(Application.loadedLevelName)))
			{
				animation.gameObject.active = false;
			}
		}
	}

	private string makeEpisodeAgnostic(string original)
	{
		return VNLUtil.getInstance().getEpisodeAgnosticSceneName(original);
	}
}
