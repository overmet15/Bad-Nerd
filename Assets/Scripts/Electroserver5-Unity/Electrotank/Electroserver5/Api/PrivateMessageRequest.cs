using System.Collections.Generic;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PrivateMessageRequest : EsRequest
	{
		private string Message_;

		private string[] UserNames_;

		private EsObject EsObject_;

		public string Message
		{
			get
			{
				return Message_;
			}
			set
			{
				Message_ = value;
				Message_Set_ = true;
			}
		}

		private bool Message_Set_ { get; set; }

		public string[] UserNames
		{
			get
			{
				return UserNames_;
			}
			set
			{
				UserNames_ = value;
				UserNames_Set_ = true;
			}
		}

		private bool UserNames_Set_ { get; set; }

		public EsObject EsObject
		{
			get
			{
				return EsObject_;
			}
			set
			{
				EsObject_ = value;
				EsObject_Set_ = true;
			}
		}

		private bool EsObject_Set_ { get; set; }

		public PrivateMessageRequest()
		{
			base.MessageType = MessageType.PrivateMessageRequest;
		}

		public PrivateMessageRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPrivateMessageRequest thriftPrivateMessageRequest = new ThriftPrivateMessageRequest();
			if (Message_Set_ && Message != null)
			{
				string message = Message;
				thriftPrivateMessageRequest.Message = message;
			}
			if (UserNames_Set_ && UserNames != null)
			{
				List<string> list = new List<string>();
				string[] userNames = UserNames;
				foreach (string text in userNames)
				{
					string item = text;
					list.Add(item);
				}
				thriftPrivateMessageRequest.UserNames = list;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObject esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObject;
				thriftPrivateMessageRequest.EsObject = esObject;
			}
			return thriftPrivateMessageRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftPrivateMessageRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPrivateMessageRequest thriftPrivateMessageRequest = (ThriftPrivateMessageRequest)t_;
			if (thriftPrivateMessageRequest.__isset.message && thriftPrivateMessageRequest.Message != null)
			{
				Message = thriftPrivateMessageRequest.Message;
			}
			if (thriftPrivateMessageRequest.__isset.userNames && thriftPrivateMessageRequest.UserNames != null)
			{
				UserNames = new string[thriftPrivateMessageRequest.UserNames.Count];
				for (int i = 0; i < thriftPrivateMessageRequest.UserNames.Count; i++)
				{
					string text = thriftPrivateMessageRequest.UserNames[i];
					UserNames[i] = text;
				}
			}
			if (thriftPrivateMessageRequest.__isset.esObject && thriftPrivateMessageRequest.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPrivateMessageRequest.EsObject));
			}
		}
	}
}
