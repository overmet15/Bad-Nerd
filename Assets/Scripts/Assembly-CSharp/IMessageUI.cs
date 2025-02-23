public interface IMessageUI
{
	IMessageUI setTitle(string title);

	string getQueuedTitle();

	IMessageUI setBlock(bool block);

	bool getQueuedBlock();
}
