using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class UpdateUserVariableRequest : EsRequest
	{
		private string Name_;

		private EsObject Value_;

		public string Name
		{
			get
			{
				return Name_;
			}
			set
			{
				Name_ = value;
				Name_Set_ = true;
			}
		}

		private bool Name_Set_ { get; set; }

		public EsObject Value
		{
			get
			{
				return Value_;
			}
			set
			{
				Value_ = value;
				Value_Set_ = true;
			}
		}

		private bool Value_Set_ { get; set; }

		public UpdateUserVariableRequest()
		{
			base.MessageType = MessageType.UpdateUserVariableRequest;
		}

		public UpdateUserVariableRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftUpdateUserVariableRequest thriftUpdateUserVariableRequest = new ThriftUpdateUserVariableRequest();
			if (Name_Set_ && Name != null)
			{
				string name = Name;
				thriftUpdateUserVariableRequest.Name = name;
			}
			if (Value_Set_ && Value != null)
			{
				ThriftFlattenedEsObject value = EsObjectCodec.FlattenEsObject(Value).ToThrift() as ThriftFlattenedEsObject;
				thriftUpdateUserVariableRequest.Value = value;
			}
			return thriftUpdateUserVariableRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftUpdateUserVariableRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftUpdateUserVariableRequest thriftUpdateUserVariableRequest = (ThriftUpdateUserVariableRequest)t_;
			if (thriftUpdateUserVariableRequest.__isset.name && thriftUpdateUserVariableRequest.Name != null)
			{
				Name = thriftUpdateUserVariableRequest.Name;
			}
			if (thriftUpdateUserVariableRequest.__isset.value && thriftUpdateUserVariableRequest.Value != null)
			{
				Value = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftUpdateUserVariableRequest.Value));
			}
		}
	}
}
