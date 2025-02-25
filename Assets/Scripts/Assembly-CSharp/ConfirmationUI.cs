public class ConfirmationUI : MessageUI
{
	private VNLUtil.NoNameMethod onCancel;

	public void setMessage(string txt, VNLUtil.NoNameMethod onOK, VNLUtil.NoNameMethod onCancel)
	{
		setMessage(new string[1] { txt }, onOK, onCancel);
	}

	public void setMessage(string[] txt, VNLUtil.NoNameMethod onOK, VNLUtil.NoNameMethod onCancel)
	{
		setMessage(txt, onOK, true, null);
		this.onCancel = onCancel;
	}

	public void clickedNO()
	{
		if (isActiveUI())
		{
			close();
			if (onCancel != null)
			{
				VNLUtil.getInstance().doStartCoRoutine(onCancel);
			}
		}
	}

	protected override void Update()
	{
		base.Update();
	//	if (GameStart.isZeemoteConnected && ZeemoteInput.GetButtonUp(1, 1))
	//	{
		//	clickedNO();
		//}
	}
}
