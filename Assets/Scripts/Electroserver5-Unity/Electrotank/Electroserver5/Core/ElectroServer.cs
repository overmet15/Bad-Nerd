namespace Electrotank.Electroserver5.Core
{
	public class ElectroServer
	{
		public EsEngine Engine { get; protected set; }

		public ManagerHelper ManagerHelper { get; protected set; }

		public ElectroServer()
		{
			Engine = new EsEngineDefault();
			ManagerHelper = new ManagerHelper(Engine);
		}
	}
}
