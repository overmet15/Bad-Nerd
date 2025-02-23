using System;
using UnityEngine;

public class MapCam : MonoBehaviour
{
	private MyJoystick lControl;

	private Camera mapCamera;

	private PlayerAttackComponent player;

	private Vector3 lastTouchPos;

	private void Start()
	{
		VNLUtil.getInstance().activeUIID = 40;
		player = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		Vector3 position = player.transform.position;
		position.y = base.transform.position.y;
		base.transform.position = position;
		lControl = GameObject.Find("LeftJoystick").GetComponent<MyJoystick>();
		lControl.enabled = false;
		mapCamera = GetComponent<Camera>();
		Map map = VNLUtil.getInstance<QuestInitializer>().map;
		VNLUtil.toggleAll(map.transform, true);
		mapCamera.orthographicSize = map.cameraSize;
		mapCamera.transform.Rotate(Vector3.forward, map.cameraAngle);
		PlayerAttackComponent playerAttackComponent = player;
		playerAttackComponent.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Combine(playerAttackComponent.onGoToScene, new VNLUtil.NoNameMethod(closeMap));
		VNLUtil.getInstance().pause();
		player.toggleMapArrow(true);
		VNLUtil.getInstance<Map>("map").initMarkers();
		player.togglePlayerCamera(false);
	}

	public void closeMap()
	{
		player.togglePlayerCamera(true);
		Map map = VNLUtil.getInstance<QuestInitializer>().map;
		VNLUtil.toggleAll(map.transform, false);
		GameObject[] array = GameObject.FindGameObjectsWithTag("exclaimationMark");
		GameObject[] array2 = array;
		foreach (GameObject gameObject in array2)
		{
			gameObject.GetComponent<Animation>().enabled = false;
		}
		PlayerAttackComponent playerAttackComponent = player;
		playerAttackComponent.onGoToScene = (VNLUtil.NoNameMethod)Delegate.Remove(playerAttackComponent.onGoToScene, new VNLUtil.NoNameMethod(closeMap));
		VNLUtil.getInstance().resume();
		UnityEngine.Object.Destroy(GameObject.Find("mapObjects"));
	}

	public void zoomIn()
	{
		if (!(mapCamera.orthographicSize <= 50f))
		{
			mapCamera.orthographicSize -= 50f;
		}
	}

	public void zoomOut()
	{
		if (!(mapCamera.orthographicSize >= 900f))
		{
			mapCamera.orthographicSize += 50f;
		}
	}

	private void OnDestroy()
	{
		VNLUtil.getInstance().activeUIID = 0;
		player.toggleMapArrow(false);
		lControl.enabled = true;
	}

	private void Update()
	{
		if (Input.touchCount == 0)
		{
			return;
		}
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Moved)
		{
			Vector3 vector = new Vector3(touch.position.x, touch.position.y, 0f);
			if (lastTouchPos != Vector3.zero)
			{
				Vector3 vector2 = lastTouchPos - vector;
				mapCamera.transform.Translate(vector2 * 2f);
			}
			lastTouchPos = vector;
		}
		else
		{
			lastTouchPos = Vector3.zero;
		}
	}
}
