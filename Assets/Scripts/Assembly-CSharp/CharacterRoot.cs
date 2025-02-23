using UnityEngine;

public class CharacterRoot : MonoBehaviour
{
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		Debug.Log("character root fully initialized");
	}
}
