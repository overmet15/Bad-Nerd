using System;
using System.Net.Sockets;

namespace Electrotank.Electroserver5.Core
{
	internal class CrossDomainReader
	{
		private class State
		{
			internal readonly byte[] buffer;

			internal readonly CrossDomainReader reader;

			internal State(int size, CrossDomainReader reader)
			{
				buffer = new byte[size];
				this.reader = reader;
			}
		}

		private readonly SocketConnection connection;

		internal CrossDomainReader(SocketConnection connection)
		{
			this.connection = connection;
		}

		internal void Read()
		{
			State state = new State(128, this);
			connection.Socket.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None, Callback, state);
		}

		private void Callback(IAsyncResult result)
		{
			State state = (State)result.AsyncState;
			int num = state.reader.connection.Socket.EndReceive(result);
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (state.buffer[i] == 0)
				{
					num2 = i;
					break;
				}
			}
			if (num2 >= 0)
			{
				if (num2 + 1 >= num)
				{
					new AsyncReader(connection).Start();
				}
				else
				{
					new AsyncReader(connection).Start(state.buffer, num2 + 1, num - num2 - 1);
				}
			}
			else
			{
				state.reader.Read();
			}
		}
	}
}
