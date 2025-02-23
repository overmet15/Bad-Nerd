namespace Electrotank.Electroserver5.Api
{
	public class EsObjectDataHolder
	{
		private DataType dataType;

		private object rawValue;

		private int intValue;

		private string stringValue;

		private double doubleValue;

		private float floatValue;

		private bool booleanValue;

		private byte byteValue;

		private char charValue;

		private long longValue;

		private short shortValue;

		private int[] intArrayValue;

		private Number numberValue;

		private string[] stringArrayValue;

		private double[] doubleArrayValue;

		private float[] floatArrayValue;

		private bool[] booleanArrayValue;

		private byte[] byteArrayValue;

		private char[] charArrayValue;

		private long[] longArrayValue;

		private short[] shortArrayValue;

		private Number[] numberArrayValue;

		private EsObject esObjectValue;

		private EsObject[] esObjectArrayValue;

		public string Name { get; set; }

		public EsObjectDataHolder(string name, int value)
		{
			Name = name;
			setDataType(DataType.Integer);
			setIntValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, string value)
		{
			Name = name;
			setDataType(DataType.String);
			setStringValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, double value)
		{
			Name = name;
			setDataType(DataType.Double);
			setDoubleValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, float value)
		{
			Name = name;
			setDataType(DataType.Float);
			setFloatValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, bool value)
		{
			Name = name;
			setDataType(DataType.Boolean);
			setBooleanValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, byte value)
		{
			Name = name;
			setDataType(DataType.Byte);
			setByteValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, char value)
		{
			Name = name;
			setDataType(DataType.Character);
			setCharValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, long value)
		{
			Name = name;
			setDataType(DataType.Long);
			setLongValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, short value)
		{
			Name = name;
			setDataType(DataType.Short);
			setShortValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, Number value)
		{
			Name = name;
			setDataType(DataType.Number);
			setNumberValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, int[] value)
		{
			Name = name;
			setDataType(DataType.IntegerArray);
			setIntArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, string[] value)
		{
			Name = name;
			setDataType(DataType.StringArray);
			setStringArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, double[] value)
		{
			Name = name;
			setDataType(DataType.DoubleArray);
			setDoubleArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, float[] value)
		{
			Name = name;
			setDataType(DataType.FloatArray);
			setFloatArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, bool[] value)
		{
			Name = name;
			setDataType(DataType.BooleanArray);
			setBooleanArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, byte[] value)
		{
			Name = name;
			setDataType(DataType.ByteArray);
			setByteArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, char[] value)
		{
			Name = name;
			setDataType(DataType.CharacterArray);
			setCharArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, long[] value)
		{
			Name = name;
			setDataType(DataType.LongArray);
			setLongArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, short[] value)
		{
			Name = name;
			setDataType(DataType.ShortArray);
			setShortArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, Number[] value)
		{
			Name = name;
			setDataType(DataType.NumberArray);
			setNumberArrayValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, EsObject value)
		{
			Name = name;
			setDataType(DataType.EsObject);
			setEsObjectValue(value);
			setRawValue(value);
		}

		public EsObjectDataHolder(string name, EsObject[] value)
		{
			Name = name;
			setDataType(DataType.EsObjectArray);
			setEsObjectArrayValue(value);
			setRawValue(value);
		}

		public DataType getDataType()
		{
			return dataType;
		}

		public void setDataType(DataType dataType)
		{
			this.dataType = dataType;
		}

		public int getIntValue()
		{
			return intValue;
		}

		public void setIntValue(int intValue)
		{
			this.intValue = intValue;
		}

		public string getStringValue()
		{
			return stringValue;
		}

		public void setStringValue(string stringValue)
		{
			this.stringValue = stringValue;
		}

		public double getDoubleValue()
		{
			return doubleValue;
		}

		public void setDoubleValue(double doubleValue)
		{
			this.doubleValue = doubleValue;
		}

		public float getFloatValue()
		{
			return floatValue;
		}

		public void setFloatValue(float floatValue)
		{
			this.floatValue = floatValue;
		}

		public bool getBooleanValue()
		{
			return booleanValue;
		}

		public void setBooleanValue(bool booleanValue)
		{
			this.booleanValue = booleanValue;
		}

		public byte getByteValue()
		{
			return byteValue;
		}

		public void setByteValue(byte byteValue)
		{
			this.byteValue = byteValue;
		}

		public char getCharValue()
		{
			return charValue;
		}

		public void setCharValue(char charValue)
		{
			this.charValue = charValue;
		}

		public long getLongValue()
		{
			return longValue;
		}

		public void setLongValue(long longValue)
		{
			this.longValue = longValue;
		}

		public short getShortValue()
		{
			return shortValue;
		}

		public void setShortValue(short shortValue)
		{
			this.shortValue = shortValue;
		}

		public EsObject getEsObjectValue()
		{
			return esObjectValue;
		}

		public void setEsObjectValue(EsObject esObjectValue)
		{
			this.esObjectValue = esObjectValue;
		}

		public EsObject[] getEsObjectArrayValue()
		{
			return esObjectArrayValue;
		}

		public void setEsObjectArrayValue(EsObject[] esObjectArrayValue)
		{
			this.esObjectArrayValue = esObjectArrayValue;
		}

		public int[] getIntArrayValue()
		{
			return intArrayValue;
		}

		public void setIntArrayValue(int[] intArrayValue)
		{
			this.intArrayValue = intArrayValue;
		}

		public string[] getStringArrayValue()
		{
			return stringArrayValue;
		}

		public void setStringArrayValue(string[] stringArrayValue)
		{
			this.stringArrayValue = stringArrayValue;
		}

		public double[] getDoubleArrayValue()
		{
			return doubleArrayValue;
		}

		public void setDoubleArrayValue(double[] doubleArrayValue)
		{
			this.doubleArrayValue = doubleArrayValue;
		}

		public float[] getFloatArrayValue()
		{
			return floatArrayValue;
		}

		public void setFloatArrayValue(float[] floatArrayValue)
		{
			this.floatArrayValue = floatArrayValue;
		}

		public bool[] getBooleanArrayValue()
		{
			return booleanArrayValue;
		}

		public void setBooleanArrayValue(bool[] booleanArrayValue)
		{
			this.booleanArrayValue = booleanArrayValue;
		}

		public byte[] getByteArrayValue()
		{
			return byteArrayValue;
		}

		public void setByteArrayValue(byte[] byteArrayValue)
		{
			this.byteArrayValue = byteArrayValue;
		}

		public char[] getCharArrayValue()
		{
			return charArrayValue;
		}

		public void setCharArrayValue(char[] charArrayValue)
		{
			this.charArrayValue = charArrayValue;
		}

		public long[] getLongArrayValue()
		{
			return longArrayValue;
		}

		public void setLongArrayValue(long[] longArrayValue)
		{
			this.longArrayValue = longArrayValue;
		}

		public short[] getShortArrayValue()
		{
			return shortArrayValue;
		}

		public void setShortArrayValue(short[] shortArrayValue)
		{
			this.shortArrayValue = shortArrayValue;
		}

		public object getRawValue()
		{
			return rawValue;
		}

		public void setRawValue(object rawValue)
		{
			this.rawValue = rawValue;
		}

		public Number getNumberValue()
		{
			return numberValue;
		}

		public void setNumberValue(Number numberValue)
		{
			this.numberValue = numberValue;
		}

		public void setNumberValue(double numberValue)
		{
			this.numberValue = new Number(numberValue);
		}

		public Number[] getNumberArrayValue()
		{
			return numberArrayValue;
		}

		public void setNumberArrayValue(Number[] numberArrayValue)
		{
			this.numberArrayValue = numberArrayValue;
		}
	}
}
