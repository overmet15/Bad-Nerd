using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class FlattenedEsObjectRO : EsEntity
	{
		private byte[] EncodedEntries_;

		public byte[] EncodedEntries
		{
			get
			{
				return EncodedEntries_;
			}
			set
			{
				EncodedEntries_ = value;
				EncodedEntries_Set_ = true;
			}
		}

		private bool EncodedEntries_Set_ { get; set; }

		public FlattenedEsObjectRO()
		{
		}

		public FlattenedEsObjectRO(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftFlattenedEsObjectRO thriftFlattenedEsObjectRO = new ThriftFlattenedEsObjectRO();
			if (EncodedEntries_Set_ && EncodedEntries != null)
			{
				List<byte> list = new List<byte>();
				byte[] encodedEntries = EncodedEntries;
				foreach (byte b in encodedEntries)
				{
					byte item = b;
					list.Add(item);
				}
				thriftFlattenedEsObjectRO.EncodedEntries = list;
			}
			return thriftFlattenedEsObjectRO;
		}

		public override TBase NewThrift()
		{
			return new ThriftFlattenedEsObjectRO();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftFlattenedEsObjectRO thriftFlattenedEsObjectRO = (ThriftFlattenedEsObjectRO)t_;
			if (thriftFlattenedEsObjectRO.__isset.encodedEntries && thriftFlattenedEsObjectRO.EncodedEntries != null)
			{
				EncodedEntries = new byte[thriftFlattenedEsObjectRO.EncodedEntries.Count];
				for (int i = 0; i < thriftFlattenedEsObjectRO.EncodedEntries.Count; i++)
				{
					byte b = thriftFlattenedEsObjectRO.EncodedEntries[i];
					EncodedEntries[i] = b;
				}
			}
		}
	}
}
