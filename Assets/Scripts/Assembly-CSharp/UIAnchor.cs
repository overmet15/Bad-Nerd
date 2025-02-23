using UnityEngine;

[AddComponentMenu("NGUI/UI/Anchor")]
[ExecuteInEditMode]
public class UIAnchor : MonoBehaviour
{
	public enum Side
	{
		BottomLeft = 0,
		Left = 1,
		TopLeft = 2,
		Top = 3,
		TopRight = 4,
		Right = 5,
		BottomRight = 6,
		Bottom = 7,
		Center = 8
	}

	private bool mIsWindows;

	public Camera uiCamera;

	public UIWidget widgetContainer;

	public UIPanel panelContainer;

	public Side side = Side.Center;

	public bool halfPixelOffset = true;

	public float depthOffset;

	public Vector2 relativeOffset = Vector2.zero;

	private void OnEnable()
	{
		mIsWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.WindowsEditor;
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	private void Update()
	{
		Rect rect = default(Rect);
		bool flag = false;
		if (panelContainer != null)
		{
			if (panelContainer.clipping == UIDrawCall.Clipping.None)
			{
				rect.xMin = (float)(-Screen.width) * 0.5f;
				rect.yMin = (float)(-Screen.height) * 0.5f;
				rect.xMax = 0f - rect.xMin;
				rect.yMax = 0f - rect.yMin;
			}
			else
			{
				Vector4 clipRange = panelContainer.clipRange;
				rect.x = clipRange.x - clipRange.z * 0.5f;
				rect.y = clipRange.y - clipRange.w * 0.5f;
				rect.width = clipRange.z;
				rect.height = clipRange.w;
			}
		}
		else if (widgetContainer != null)
		{
			Transform cachedTransform = widgetContainer.cachedTransform;
			Vector3 localScale = cachedTransform.localScale;
			Vector3 localPosition = cachedTransform.localPosition;
			Vector2 relativeSize = widgetContainer.relativeSize;
			Vector3 vector = widgetContainer.pivotOffset;
			vector.y -= relativeSize.y;
			vector.x *= relativeSize.x * localScale.x;
			vector.y *= relativeSize.y * localScale.y;
			rect.x = localPosition.x + vector.x;
			rect.y = localPosition.y + vector.y;
			rect.width = relativeSize.x * localScale.x;
			rect.height = relativeSize.y * localScale.y;
		}
		else
		{
			if (!(uiCamera != null))
			{
				return;
			}
			flag = true;
			rect = uiCamera.pixelRect;
		}
		float x = (rect.xMin + rect.xMax) * 0.5f;
		float y = (rect.yMin + rect.yMax) * 0.5f;
		Vector3 vector2 = new Vector3(x, y, depthOffset);
		if (side != Side.Center)
		{
			if (side == Side.Right || side == Side.TopRight || side == Side.BottomRight)
			{
				vector2.x = rect.xMax;
			}
			else if (side == Side.Top || side == Side.Center || side == Side.Bottom)
			{
				vector2.x = x;
			}
			else
			{
				vector2.x = rect.xMin;
			}
			if (side == Side.Top || side == Side.TopRight || side == Side.TopLeft)
			{
				vector2.y = rect.yMax;
			}
			else if (side == Side.Left || side == Side.Center || side == Side.Right)
			{
				vector2.y = y;
			}
			else
			{
				vector2.y = rect.yMin;
			}
		}
		float width = rect.width;
		float height = rect.height;
		vector2.x += relativeOffset.x * width;
		vector2.y += relativeOffset.y * height;
		if (flag)
		{
			if (uiCamera.orthographic)
			{
				vector2.x = Mathf.RoundToInt(vector2.x);
				vector2.y = Mathf.RoundToInt(vector2.y);
				if (halfPixelOffset && mIsWindows)
				{
					vector2.x -= 0.5f;
					vector2.y += 0.5f;
				}
			}
			vector2 = uiCamera.ScreenToWorldPoint(vector2);
			if (base.transform.position != vector2)
			{
				base.transform.position = vector2;
			}
		}
		else
		{
			vector2.x = Mathf.RoundToInt(vector2.x);
			vector2.y = Mathf.RoundToInt(vector2.y);
			if (base.transform.localPosition != vector2)
			{
				base.transform.localPosition = vector2;
			}
		}
	}
}
