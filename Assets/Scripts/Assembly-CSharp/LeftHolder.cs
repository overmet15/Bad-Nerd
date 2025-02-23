using UnityEngine;

public class LeftHolder : MonoBehaviour
{
	private Transform myTransform;

	private Transform markerTransform;

	private Camera hudCamera;

	public bool left;

	public string camName;

	private void Start()
	{
		myTransform = base.transform;
		markerTransform = base.transform.Find("LeftMarker").transform;
		hudCamera = GameObject.Find(camName).GetComponent<Camera>();
	}

	private void Update()
	{
		Vector3 vector = hudCamera.WorldToScreenPoint(markerTransform.position);
		if (left)
		{
			if (vector.x > 0f)
			{
				myTransform.Translate(Vector3.left * 0.1f);
				return;
			}
			Object.Destroy(markerTransform.gameObject);
			base.enabled = false;
			base.gameObject.active = false;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				base.gameObject.active = true;
			});
			return;
		}
		float pixelWidth = hudCamera.pixelWidth;
		if (vector.x < pixelWidth)
		{
			myTransform.Translate(Vector3.right * 0.1f);
			return;
		}
		Object.Destroy(markerTransform.gameObject);
		base.enabled = false;
		base.gameObject.active = false;
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			base.gameObject.active = true;
		});
	}
}
