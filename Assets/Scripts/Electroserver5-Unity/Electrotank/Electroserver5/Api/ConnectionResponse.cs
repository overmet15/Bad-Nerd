using System;
using Electrotank.Electroserver5.Api.Helper;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class ConnectionResponse : EsResponse
	{
		private bool Successful_;

		private int HashId_;

		private ErrorType? Error_;

		private ProtocolConfiguration ProtocolConfiguration_;

		private string ServerVersion_;

		public bool Successful
		{
			get
			{
				return Successful_;
			}
			set
			{
				Successful_ = value;
				Successful_Set_ = true;
			}
		}

		private bool Successful_Set_ { get; set; }

		public int HashId
		{
			get
			{
				return HashId_;
			}
			set
			{
				HashId_ = value;
				HashId_Set_ = true;
			}
		}

		private bool HashId_Set_ { get; set; }

		public ErrorType? Error
		{
			get
			{
				return Error_;
			}
			set
			{
				Error_ = value;
				Error_Set_ = true;
			}
		}

		private bool Error_Set_ { get; set; }

		public ProtocolConfiguration ProtocolConfiguration
		{
			get
			{
				return ProtocolConfiguration_;
			}
			set
			{
				ProtocolConfiguration_ = value;
				ProtocolConfiguration_Set_ = true;
			}
		}

		private bool ProtocolConfiguration_Set_ { get; set; }

		public string ServerVersion
		{
			get
			{
				return ServerVersion_;
			}
			set
			{
				ServerVersion_ = value;
				ServerVersion_Set_ = true;
			}
		}

		private bool ServerVersion_Set_ { get; set; }

		public ConnectionResponse()
		{
			base.MessageType = MessageType.ConnectionResponse;
		}

		public ConnectionResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftConnectionResponse thriftConnectionResponse = new ThriftConnectionResponse();
			if (Successful_Set_)
			{
				bool successful = Successful;
				thriftConnectionResponse.Successful = successful;
			}
			if (HashId_Set_)
			{
				int hashId = HashId;
				thriftConnectionResponse.HashId = hashId;
			}
			if (Error_Set_ && Error.HasValue)
			{
				ThriftErrorType error = (ThriftErrorType)(object)ThriftUtil.EnumConvert(typeof(ThriftErrorType), (Enum)(object)Error);
				thriftConnectionResponse.Error = error;
			}
			if (ProtocolConfiguration_Set_ && ProtocolConfiguration != null)
			{
				ThriftProtocolConfiguration protocolConfiguration = ProtocolConfiguration.ToThrift() as ThriftProtocolConfiguration;
				thriftConnectionResponse.ProtocolConfiguration = protocolConfiguration;
			}
			if (ServerVersion_Set_ && ServerVersion != null)
			{
				string serverVersion = ServerVersion;
				thriftConnectionResponse.ServerVersion = serverVersion;
			}
			return thriftConnectionResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftConnectionResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftConnectionResponse thriftConnectionResponse = (ThriftConnectionResponse)t_;
			if (thriftConnectionResponse.__isset.successful)
			{
				Successful = thriftConnectionResponse.Successful;
			}
			if (thriftConnectionResponse.__isset.hashId)
			{
				HashId = thriftConnectionResponse.HashId;
			}
			if (thriftConnectionResponse.__isset.error)
			{
				Error = (ErrorType)(object)ThriftUtil.EnumConvert(Nullable.GetUnderlyingType(typeof(ErrorType?)), thriftConnectionResponse.Error);
			}
			if (thriftConnectionResponse.__isset.protocolConfiguration && thriftConnectionResponse.ProtocolConfiguration != null)
			{
				ProtocolConfiguration = new ProtocolConfiguration(thriftConnectionResponse.ProtocolConfiguration);
			}
			if (thriftConnectionResponse.__isset.serverVersion && thriftConnectionResponse.ServerVersion != null)
			{
				ServerVersion = thriftConnectionResponse.ServerVersion;
			}
		}
	}
}
