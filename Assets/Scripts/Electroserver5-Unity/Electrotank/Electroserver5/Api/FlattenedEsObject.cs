using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class FlattenedEsObject : EsEntity
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

		public FlattenedEsObject()
		{
		}

		public FlattenedEsObject(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftFlattenedEsObject thriftFlattenedEsObject = new ThriftFlattenedEsObject();
			if (EncodedEntries_Set_ && EncodedEntries != null)
			{
				List<byte> list = new List<byte>();
				byte[] encodedEntries = EncodedEntries;
				foreach (byte b in encodedEntries)
				{
					byte item = b;
					list.Add(item);
				}
				thriftFlattenedEsObject.EncodedEntries = list;
			}
			return thriftFlattenedEsObject;
		}

		public override TBase NewThrift()
		{
			return new ThriftFlattenedEsObject();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftFlattenedEsObject thriftFlattenedEsObject = (ThriftFlattenedEsObject)t_;
			if (thriftFlattenedEsObject.__isset.encodedEntries && thriftFlattenedEsObject.EncodedEntries != null)
			{
				EncodedEntries = new byte[thriftFlattenedEsObject.EncodedEntries.Count];
				for (int i = 0; i < thriftFlattenedEsObject.EncodedEntries.Count; i++)
				{
					byte b = thriftFlattenedEsObject.EncodedEntries[i];
					EncodedEntries[i] = b;
				}
			}
		}
	}
}
