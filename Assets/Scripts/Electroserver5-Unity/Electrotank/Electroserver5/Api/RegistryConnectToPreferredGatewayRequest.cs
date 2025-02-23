using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class RegistryConnectToPreferredGatewayRequest : EsRequest
	{
		private int ZoneId_;

		private string Host_;

		private int Port_;

		private Protocol? ProtocolToUse_;

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

		public string Host
		{
			get
			{
				return Host_;
			}
			set
			{
				Host_ = value;
				Host_Set_ = true;
			}
		}

		private bool Host_Set_ { get; set; }

		public int Port
		{
			get
			{
				return Port_;
			}
			set
			{
				Port_ = value;
				Port_Set_ = true;
			}
		}

		private bool Port_Set_ { get; set; }

		public Protocol? ProtocolToUse
		{
			get
			{
				return ProtocolToUse_;
			}
			set
			{
				ProtocolToUse_ = value;
				ProtocolToUse_Set_ = true;
			}
		}

		private bool ProtocolToUse_Set_ { get; set; }

		public RegistryConnectToPreferredGatewayRequest()
		{
			base.MessageType = MessageType.RegistryConnectToPreferredGatewayRequest;
		}

		public RegistryConnectToPreferredGatewayRequest(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftRegistryConnectToPreferredGatewayRequest thriftRegistryConnectToPreferredGatewayRequest = new ThriftRegistryConnectToPreferredGatewayRequest();
			if (ZoneId_Set_)
			{
				int zoneId = ZoneId;
				thriftRegistryConnectToPreferredGatewayRequest.ZoneId = zoneId;
			}
			if (Host_Set_ && Host != null)
			{
				string host = Host;
				thriftRegistryConnectToPreferredGatewayRequest.Host = host;
			}
			if (Port_Set_)
			{
				int port = Port;
				thriftRegistryConnectToPreferredGatewayRequest.Port = port;
			}
			if (ProtocolToUse_Set_ && ProtocolToUse.HasValue)
			{
				ThriftProtocol protocolToUse = (ThriftProtocol)(object)ThriftUtil.EnumConvert(typeof(ThriftProtocol), (Enum)(object)ProtocolToUse);
				thriftRegistryConnectToPreferredGatewayRequest.ProtocolToUse = protocolToUse;
			}
			return thriftRegistryConnectToPreferredGatewayRequest;
		}

		public override TBase NewThrift()
		{
			return new ThriftRegistryConnectToPreferredGatewayRequest();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftRegistryConnectToPreferredGatewayRequest thriftRegistryConnectToPreferredGatewayRequest = (ThriftRegistryConnectToPreferredGatewayRequest)t_;
			if (thriftRegistryConnectToPreferredGatewayRequest.__isset.zoneId)
			{
				ZoneId = thriftRegistryConnectToPreferredGatewayRequest.ZoneId;
			}
			if (thriftRegistryConnectToPreferredGatewayRequest.__isset.host && thriftRegistryConnectToPreferredGatewayRequest.Host != null)
			{
				Host = thriftRegistryConnectToPreferredGatewayRequest.Host;
			}
			if (thriftRegistryConnectToPreferredGatewayRequest.__isset.port)
			{
				Port = thriftRegistryConnectToPreferredGatewayRequest.Port;
			}
			if (thriftRegistryConnectToPreferredGatewayRequest.__isset.protocolToUse)
			{
				ProtocolToUse = (Protocol)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(Protocol?)), thriftRegistryConnectToPreferredGatewayRequest.ProtocolToUse);
			}
		}
	}
}
