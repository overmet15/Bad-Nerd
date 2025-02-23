namespace Thrift.Protocol
{
	public struct TSet
	{
		private TType elementType;

		private int count;

		public TType ElementType
		{
			get
			{
				return elementType;
			}
			set
			{
				elementType = value;
			}
		}

		public int Count
		{
			get
			{
				return count;
			}
			set
			{
				count = value;
			}
		}

		public TSet(TType elementType, int count)
		{
			this.elementType = elementType;
			this.count = count;
		}
	}
}
