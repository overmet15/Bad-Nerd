public class QueuedMessageHolder : IMessageUI
{
	private string title;

	private bool block;

	public IMessageUI setTitle(string title)
	{
		this.title = title;
		return this;
	}

	public string getQueuedTitle()
	{
		return title;
	}

	public IMessageUI setBlock(bool block)
	{
		this.block = block;
		return this;
	}

	public bool getQueuedBlock()
	{
		return block;
	}
}
