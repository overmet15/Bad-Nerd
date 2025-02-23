using UnityEngine;

public class SaveSpot : MonoBehaviour
{
	private bool detecting = true;

	public void OnCollisionEnter(Collision collision)
	{
		if (!detecting)
		{
			return;
		}
		PlayerAttackComponent player = collision.gameObject.GetComponent<PlayerAttackComponent>();
		if (player != null)
		{
			detecting = false;
			VNLUtil.getInstance().doStartCoRoutine(delegate
			{
				detecting = true;
			}, 3f);
			VNLUtil.getInstance().displayConfirmation("callHome", delegate
			{
				string loadedLevelName = Application.loadedLevelName;
				player.save(loadedLevelName);
				VNLUtil.getInstance().displayMessage("gameSaved", true);
			}, null);
		}
	}
}
