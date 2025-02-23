using System.Collections.Generic;
using Thrift.Protocol;

namespace Electrotank.Electroserver5.Api
{
	public class FindZoneAndRoomByNameResponse : EsResponse
	{
		private List<int[]> RoomAndZoneList_;

		public List<int[]> RoomAndZoneList
		{
			get
			{
				return RoomAndZoneList_;
			}
			set
			{
				RoomAndZoneList_ = value;
				RoomAndZoneList_Set_ = true;
			}
		}

		private bool RoomAndZoneList_Set_ { get; set; }

		public FindZoneAndRoomByNameResponse()
		{
			base.MessageType = MessageType.FindZoneAndRoomByNameResponse;
		}

		public FindZoneAndRoomByNameResponse(TBase t)
		{
			FromThrift(t);
		}

		public override TBase ToThrift()
		{
			ThriftFindZoneAndRoomByNameResponse thriftFindZoneAndRoomByNameResponse = new ThriftFindZoneAndRoomByNameResponse();
			if (RoomAndZoneList_Set_ && RoomAndZoneList != null)
			{
				List<List<int>> list = new List<List<int>>();
				foreach (int[] roomAndZone in RoomAndZoneList)
				{
					List<int> list2 = new List<int>();
					int[] array = roomAndZone;
					foreach (int num in array)
					{
						int item = num;
						list2.Add(item);
					}
					list.Add(list2);
				}
				thriftFindZoneAndRoomByNameResponse.RoomAndZoneList = list;
			}
			return thriftFindZoneAndRoomByNameResponse;
		}

		public override TBase NewThrift()
		{
			return new ThriftFindZoneAndRoomByNameResponse();
		}

		public override void FromThrift(TBase t_)
		{
			ThriftFindZoneAndRoomByNameResponse thriftFindZoneAndRoomByNameResponse = (ThriftFindZoneAndRoomByNameResponse)t_;
			if (!thriftFindZoneAndRoomByNameResponse.__isset.roomAndZoneList || thriftFindZoneAndRoomByNameResponse.RoomAndZoneList == null)
			{
				return;
			}
			RoomAndZoneList = new List<int[]>();
			foreach (List<int> roomAndZone in thriftFindZoneAndRoomByNameResponse.RoomAndZoneList)
			{
				int[] array = new int[roomAndZone.Count];
				for (int i = 0; i < roomAndZone.Count; i++)
				{
					int num = roomAndZone[i];
					array[i] = num;
				}
				RoomAndZoneList.Add(array);
			}
		}
	}
}
