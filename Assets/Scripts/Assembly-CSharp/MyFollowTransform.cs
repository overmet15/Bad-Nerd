using UnityEngine;

public class MyFollowTransform : MonoBehaviour
{
	public Transform targetTransform;

	public bool faceForward;

	private Transform thisTransform;

	public float rotateTime = 1f;

	private void Start()
	{
		thisTransform = base.transform;
	}

	private void Update()
	{
		thisTransform.position = targetTransform.position;
	}
}
