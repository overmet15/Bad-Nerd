using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class PublicMessageRequest : EsRequest
	{
		private int ZoneId_;

		private int RoomId_;

		private string Message_;

		private EsObject EsObject_;

		public int ZoneId
		{
			get
			{
				return ZoneId_;
			}
			set
			{
				ZoneId_ = value;
				ZoneId_Set_ = true;
			}
		}

		private bool ZoneId_Set_ { get; set; }

		public int RoomId
		{
			get
			{
				return RoomId_;
			}
			set
			{
				RoomId_ = value;
				RoomId_Set_ = true;
			}
		}

		private bool RoomId_Set_ { get; set; }

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

		public PublicMessageRequest()
		{
			base.MessageType = MessageType.PublicMessageRequest;
		}

		public PublicMessageRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftPublicMessageRequest thriftPublicMessageRequest = new ThriftPublicMessageRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftPublicMessageRequest.ZoneId = zoneId;
			}
			if (RoomId_Set_)
			{
				int roomId = RoomId;
				thriftPublicMessageRequest.RoomId = roomId;
			}
			if (Message_Set_ && Message != null)
			{
				string message = Message;
				thriftPublicMessageRequest.Message = message;
			}
			if (EsObject_Set_ && EsObject != null)
			{
				ThriftFlattenedEsObject esObject = EsObjectCodec.FlattenEsObject(EsObject).ToThrift() as ThriftFlattenedEsObject;
				thriftPublicMessageRequest.EsObject = esObject;
			}
			return thriftPublicMessageRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftPublicMessageRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftPublicMessageRequest thriftPublicMessageRequest = (ThriftPublicMessageRequest)t_;
			if (thriftPublicMessageRequest.__isset.zoneId)
			{
				ZoneId = thriftPublicMessageRequest.ZoneId;
			}
			if (thriftPublicMessageRequest.__isset.roomId)
			{
				RoomId = thriftPublicMessageRequest.RoomId;
			}
			if (thriftPublicMessageRequest.__isset.message && thriftPublicMessageRequest.Message != null)
			{
				Message = thriftPublicMessageRequest.Message;
			}
			if (thriftPublicMessageRequest.__isset.esObject && thriftPublicMessageRequest.EsObject != null)
			{
				EsObject = EsObjectCodec.UnflattenEsObject(new FlattenedEsObject(thriftPublicMessageRequest.EsObject));
			}
		}
	}
}
