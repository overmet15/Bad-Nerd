using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;

namespace Electrotank.Electroserver5.Core
{
	public class UnityWebClient
	{
		private ILog log = LogManager.GetLogger(typeof(UnityWebClient));

		private IPEndPoint endPoint;

		private TcpClient tcp;

		private EventHandler<UnityUploadDataCompletedEventArgs> m_UploadDataCompleted;

		public event EventHandler<UnityUploadDataCompletedEventArgs> UploadDataCompleted
		{
			add
			{
				EventHandler<UnityUploadDataCompletedEventArgs> eventHandler = this.m_UploadDataCompleted;
				EventHandler<UnityUploadDataCompletedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref this.m_UploadDataCompleted, (EventHandler<UnityUploadDataCompletedEventArgs>)Delegate.Combine(eventHandler2, value), eventHandler);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler<UnityUploadDataCompletedEventArgs> eventHandler = this.m_UploadDataCompleted;
				EventHandler<UnityUploadDataCompletedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					eventHandler = Interlocked.CompareExchange(ref this.m_UploadDataCompleted, (EventHandler<UnityUploadDataCompletedEventArgs>)Delegate.Remove(eventHandler2, value), eventHandler);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public UnityWebClient()
		{
			log.Debug("Instantiated UnityWebClient");
		}

		public void UploadDataAsync(Uri uri, byte[] data)
		{
			if (endPoint == null)
			{
				endPoint = new IPEndPoint(Dns.GetHostAddresses(uri.Host)[0], uri.Port);
			}
			ThreadPool.QueueUserWorkItem(delegate
			{
				UploadDataAsyncThread(uri, data);
			});
		}

		private void UploadDataAsyncThread(Uri uri, byte[] data)
		{
			UnityUploadDataCompletedEventArgs e = null;
			try
			{
				log.Debug("UploadDataAsyncThread Start");
				ConnectToServer();
				SendHeaderAndData(uri, data);
				e = ReadResponse();
			}
			catch (Exception ex)
			{
				log.ErrorFormat("An error occurred in the UploadDataAsyncThread: {0}", ex.Message);
			}
			finally
			{
				if (tcp != null)
				{
					tcp.Close();
					tcp = null;
				}
			}
			log.Debug("Dispatching done event");
			OnUploadDataCompleted(e);
		}

		private void ConnectToServer()
		{
			try
			{
				tcp = new TcpClient();
				tcp.Connect(endPoint);
			}
			catch (ArgumentNullException ex)
			{
				log.Error("Null endpoint");
				throw ex;
			}
			catch (SocketException ex2)
			{
				log.ErrorFormat("An error occurred when accessing the socket: {0}", ex2.Message);
				throw ex2;
			}
			catch (ObjectDisposedException ex3)
			{
				log.Error("TcpClient is closed");
				throw ex3;
			}
		}

		private void SendHeaderAndData(Uri uri, byte[] data)
		{
			log.DebugFormat("Sending data ({0} bytes)", data.Length);
			string s = "POST " + uri.AbsolutePath + " HTTP/1.0\r\nContent-Length: " + data.Length + "\r\nHost: " + uri.Host + ":" + uri.Port + "\r\n\r\n";
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			try
			{
				NetworkStream stream = tcp.GetStream();
				stream.Write(bytes, 0, bytes.Length);
				stream.Write(data, 0, data.Length);
			}
			catch (ArgumentNullException ex)
			{
				log.Error("Buffer provided to network stream for writing was null");
				throw ex;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				log.ErrorFormat("Argument out of range: {0}", ex2.Message);
				throw ex2;
			}
			catch (ObjectDisposedException ex3)
			{
				log.ErrorFormat("Stream is closed or network read failure: {0}", ex3.Message);
				throw ex3;
			}
			catch (InvalidOperationException ex4)
			{
				log.Error("TcpClient is not connected to a remote host");
				throw ex4;
			}
			catch (IOException ex5)
			{
				log.ErrorFormat("Failure writing to the network or when accessing socket: {0}", ex5.Message);
				throw ex5;
			}
		}

		private UnityUploadDataCompletedEventArgs ReadResponse()
		{
			log.Debug("Reading response");
			byte[] array = new byte[4096];
			int num = 0;
			NetworkStream networkStream = null;
			try
			{
				networkStream = tcp.GetStream();
				while (true)
				{
					int num2 = networkStream.Read(array, num, array.Length - num);
					if (num2 == 0)
					{
						break;
					}
					num += num2;
					if (num == array.Length)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					Thread.Sleep(1);
				}
			}
			catch (ArgumentOutOfRangeException ex)
			{
				log.Error("Argument is out of range");
				throw ex;
			}
			catch (ArgumentException ex2)
			{
				log.Error("Read buffer is null");
				throw ex2;
			}
			catch (IOException)
			{
				log.Debug("Socket is closed");
			}
			catch (ObjectDisposedException)
			{
				log.Debug("The TcpClient has been closed");
			}
			catch (InvalidOperationException)
			{
				log.Debug("The TcpClient is not connected to a remote host");
			}
			log.Debug("All data has been received");
			int num3 = -1;
			for (int i = 0; i < num - 3; i++)
			{
				if (array[i] == 13 && array[i + 1] == 10 && array[i + 2] == 13 && array[i + 3] == 10)
				{
					num3 = i + 4;
					break;
				}
			}
			if (num3 == -1)
			{
				return new UnityUploadDataCompletedEventArgs(null);
			}
			byte[] array3 = new byte[num - num3];
			Array.Copy(array, num3, array3, 0, num - num3);
			return new UnityUploadDataCompletedEventArgs(array3);
		}

		protected virtual void OnUploadDataCompleted(UnityUploadDataCompletedEventArgs e)
		{
			EventHandler<UnityUploadDataCompletedEventArgs> uploadDataCompleted = this.UploadDataCompleted;
			if (uploadDataCompleted != null)
			{
				uploadDataCompleted(this, e);
			}
		}

		public void CancelAsync()
		{
			log.Debug("CancelAsync");
			if (tcp != null)
			{
				tcp.Close();
			}
		}
	}
}
