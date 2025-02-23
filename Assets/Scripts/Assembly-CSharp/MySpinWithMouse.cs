using UnityEngine;

[AddComponentMenu("NGUI/Examples/Spin With Mouse")]
public class MySpinWithMouse : MonoBehaviour
{
	public Transform target;

	public string targetName;

	public float speed = 1f;

	public bool horizontal;

	public bool reverse;

	public bool disableWhenNotInNavViewAndClicked;

	private Transform mTrans;

	private void Start()
	{
		mTrans = base.transform;
		if (target == null && targetName != null && targetName.Length > 0)
		{
			target = GameObject.Find(targetName).transform;
		}
	}

	private void OnDrag(Vector2 delta)
	{
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.None;
		float num = ((!horizontal) ? delta.y : delta.x);
		if (reverse)
		{
			num *= -1f;
		}
		if (target != null)
		{
			target.localRotation = Quaternion.Euler(0f, -0.5f * num * speed, 0f) * target.localRotation;
		}
		else
		{
			mTrans.localRotation = Quaternion.Euler(0f, -0.5f * num * speed, 0f) * mTrans.localRotation;
		}
	}
}
