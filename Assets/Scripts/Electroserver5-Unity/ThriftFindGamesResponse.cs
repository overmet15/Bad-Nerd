using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

[Serializable]
public class ThriftFindGamesResponse : TBase
{
	[Serializable]
	public struct Isset
	{
		public bool games;
	}

	private List<ThriftServerGame> _games;

	public Isset __isset;

	public List<ThriftServerGame> Games
	{
		get
		{
			return _games;
		}
		set
		{
			__isset.games = true;
			_games = value;
		}
	}

	public void Read(TProtocol iprot)
	{
		iprot.ReadStructBegin();
		while (true)
		{
			TField tField = iprot.ReadFieldBegin();
			if (tField.Type == TType.Stop)
			{
				break;
			}
			short iD = tField.ID;
			if (iD == 1)
			{
				if (tField.Type == TType.List)
				{
					Games = new List<ThriftServerGame>();
					TList tList = iprot.ReadListBegin();
					for (int i = 0; i < tList.Count; i++)
					{
						ThriftServerGame thriftServerGame = new ThriftServerGame();
						thriftServerGame = new ThriftServerGame();
						thriftServerGame.Read(iprot);
						Games.Add(thriftServerGame);
					}
					iprot.ReadListEnd();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tField.Type);
				}
			}
			else
			{
				TProtocolUtil.Skip(iprot, tField.Type);
			}
			iprot.ReadFieldEnd();
		}
		iprot.ReadStructEnd();
	}

	public void Write(TProtocol oprot)
	{
		TStruct struc = new TStruct("ThriftFindGamesResponse");
		oprot.WriteStructBegin(struc);
		TField field = default(TField);
		if (Games != null && __isset.games)
		{
			field.Name = "games";
			field.Type = TType.List;
			field.ID = 1;
			oprot.WriteFieldBegin(field);
			oprot.WriteListBegin(new TList(TType.Struct, Games.Count));
			foreach (ThriftServerGame game in Games)
			{
				game.Write(oprot);
				oprot.WriteListEnd();
			}
			oprot.WriteFieldEnd();
		}
		oprot.WriteFieldStop();
		oprot.WriteStructEnd();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("ThriftFindGamesResponse(");
		stringBuilder.Append("Games: ");
		stringBuilder.Append(Games);
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
