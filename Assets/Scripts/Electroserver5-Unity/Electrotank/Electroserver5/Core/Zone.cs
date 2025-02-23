using System.Collections.Generic;

namespace Electrotank.Electroserver5.Core
{
	public class Zone : IEntity
	{
		private readonly MultipleIndexStore<Room> roomStore = new MultipleIndexStore<Room>();

		private readonly List<Room> joinedRooms = new List<Room>();

		public int Id { get; internal set; }

		public string Name { get; internal set; }

		public ICollection<Room> Rooms
		{
			get
			{
				return roomStore.Values;
			}
		}

		public ICollection<Room> JoinedRooms
		{
			get
			{
				return joinedRooms;
			}
		}

		public Room RoomById(int roomId)
		{
			return roomStore.ById(roomId);
		}

		public Room RoomByName(string roomName)
		{
			return roomStore.ByName(roomName);
		}

		public override string ToString()
		{
			return string.Format("[Zone: Id={0}, Name={1}, Rooms={2}]", Id, Name, Rooms);
		}

		internal void AddRoom(Room room)
		{
			room.ZoneId = Id;
			roomStore.Add(room);
		}

		internal void RemoveRoom(int roomId)
		{
			roomStore.Remove(roomId);
		}

		internal void AddJoinedRoom(Room room)
		{
			if (!joinedRooms.Contains(room))
			{
				joinedRooms.Add(room);
			}
		}

		internal void RemoveJoinedRoom(Room room)
		{
			if (joinedRooms.Contains(room))
			{
				joinedRooms.Remove(room);
			}
		}
	}
}
