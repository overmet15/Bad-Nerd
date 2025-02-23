using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
	public void play(string anim)
	{
		string text = anim.Split(',')[0];
		string text2 = anim.Split(',')[1];
		string text3 = anim.Split(',')[2];
		GameObject gameObject = GameObject.Find(text);
		gameObject.GetComponent<Animation>()[text2].wrapMode = ((!text3.Equals("1")) ? WrapMode.ClampForever : WrapMode.Loop);
		gameObject.GetComponent<Animation>().CrossFade(text2);
	}
}
