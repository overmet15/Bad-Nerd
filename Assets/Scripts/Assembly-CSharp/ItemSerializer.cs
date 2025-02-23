using System.Collections;
using UnityEngine;

public class ItemSerializer
{
	public static bool IS_GAME_STATE_MODE;

	private static Hashtable map;

	public static void clearMap()
	{
		map = new Hashtable();
	}

	public static Hashtable getMap()
	{
		return map;
	}

	public static string getMapAsString()
	{
		string text = map.toJson();
		Debug.Log("getMapAsString() === " + text);
		return text;
	}

	public static void setStringToMap(string json)
	{
		Debug.Log("setStringToMap() === " + json);
		map = json.hashtableFromJson();
	}

	public static void SetInt(string key, int val)
	{
		if (IS_GAME_STATE_MODE)
		{
			Debug.Log("saving " + key + " = " + val);
			PlayerPrefs.SetInt(key, val);
		}
		else
		{
			Debug.Log("serializing " + key + " = " + val);
			map.Add(key, val);
		}
	}

	public static void SetString(string key, string val)
	{
		if (IS_GAME_STATE_MODE)
		{
			Debug.Log("saving " + key + " = " + val);
			PlayerPrefs.SetString(key, val);
		}
		else
		{
			Debug.Log("serializing " + key + " = " + val);
			map.Add(key, val);
		}
	}

	public static void SetFloat(string key, float val)
	{
		if (IS_GAME_STATE_MODE)
		{
			Debug.Log("saving " + key + " = " + val);
			PlayerPrefs.SetFloat(key, val);
		}
		else
		{
			Debug.Log("serializing " + key + " = " + val);
			map.Add(key, val);
		}
	}

	public static int GetInt(string key)
	{
		if (IS_GAME_STATE_MODE)
		{
			Debug.Log("loading " + key + " = " + PlayerPrefs.GetInt(key));
			return PlayerPrefs.GetInt(key);
		}
		Debug.Log("de-serializing " + key + " = " + map[key]);
		return int.Parse(map[key].ToString());
	}

	public static string GetString(string key)
	{
		if (IS_GAME_STATE_MODE)
		{
			Debug.Log("loading " + key + " = " + PlayerPrefs.GetString(key));
			return PlayerPrefs.GetString(key);
		}
		Debug.Log("de-serializing " + key + " = " + map[key]);
		return (string)map[key];
	}

	public static float GetFloat(string key)
	{
		if (IS_GAME_STATE_MODE)
		{
			Debug.Log("loading " + key + " = " + PlayerPrefs.GetFloat(key));
			return PlayerPrefs.GetFloat(key);
		}
		Debug.Log("de-serializing " + key + " = " + map[key]);
		return float.Parse(map[key].ToString());
	}
}
