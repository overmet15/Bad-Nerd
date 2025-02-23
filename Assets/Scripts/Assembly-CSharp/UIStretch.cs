using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	public enum Style
	{
		None = 0,
		Horizontal = 1,
		Vertical = 2,
		Both = 3,
		BasedOnHeight = 4
	}

	public Camera uiCamera;

	public UIWidget widgetContainer;

	public UIPanel panelContainer;

	public Style style;

	public Vector2 relativeSize = Vector2.one;

	private Transform mTrans;

	private UIRoot mRoot;

	private void OnEnable()
	{
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	private void Update()
	{
		if (style == Style.None)
		{
			return;
		}
		if (mTrans == null)
		{
			mTrans = base.transform;
		}
		Rect rect = default(Rect);
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
			Vector3 vector = widgetContainer.pivotOffset;
			vector.y -= widgetContainer.relativeSize.y;
			vector.x *= widgetContainer.relativeSize.x * localScale.x;
			vector.y *= widgetContainer.relativeSize.y * localScale.y;
			rect.x = localPosition.x + vector.x;
			rect.y = localPosition.y + vector.y;
			rect.width = localScale.x;
			rect.height = localScale.y;
		}
		else
		{
			if (!(uiCamera != null))
			{
				return;
			}
			rect = uiCamera.pixelRect;
		}
		float num = rect.width;
		float num2 = rect.height;
		if (mRoot != null && !mRoot.automatic && num2 > 1f)
		{
			float num3 = (float)mRoot.manualHeight / num2;
			num *= num3;
			num2 *= num3;
		}
		Vector3 localScale2 = mTrans.localScale;
		if (style == Style.BasedOnHeight)
		{
			localScale2.x = relativeSize.x * num2;
			localScale2.y = relativeSize.y * num2;
		}
		else
		{
			if (style == Style.Both || style == Style.Horizontal)
			{
				localScale2.x = relativeSize.x * num;
			}
			if (style == Style.Both || style == Style.Vertical)
			{
				localScale2.y = relativeSize.y * num2;
			}
		}
		if (mTrans.localScale != localScale2)
		{
			mTrans.localScale = localScale2;
		}
	}
}
