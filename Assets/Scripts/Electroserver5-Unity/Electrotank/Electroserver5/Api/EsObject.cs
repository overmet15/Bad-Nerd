using System;
using System.Collections;
using System.Collections.Generic;

namespace Electrotank.Electroserver5.Api
{
	public class EsObject : EsObjectRO, IEnumerable
	{
		public static readonly EsObjectRO emptyObject = new EsObject();

		private IDictionary<string, EsObjectDataHolder> data;

		public EsObject()
		{
			data = new Dictionary<string, EsObjectDataHolder>();
		}

		public IEnumerator GetEnumerator()
		{
			foreach (EsObjectDataHolder value in data.Values)
			{
				yield return value;
			}
		}

		public void addAll(EsObjectRO esObject)
		{
			foreach (EsObjectDataHolder item in esObject)
			{
				if (data.ContainsKey(item.Name))
				{
					data.Remove(item.Name);
				}
				data.Add(item.Name, item);
			}
		}

		public int getSize()
		{
			return data.Values.Count;
		}

		public DataType getDataType(string name)
		{
			return getHolderForName(name).getDataType();
		}

		public void setInteger(string name, int value)
		{
			validate("Integer", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setString(string name, string value)
		{
			validate("string", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setDouble(string name, double value)
		{
			validate("Double", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setFloat(string name, float value)
		{
			validate("Float", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setBoolean(string name, bool value)
		{
			validate("Boolean", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setByte(string name, byte value)
		{
			validate("Byte", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setChar(string name, char value)
		{
			validate("Char", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setLong(string name, long value)
		{
			validate("Long", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setShort(string name, short value)
		{
			validate("Short", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setEsObject(string name, EsObject value)
		{
			validate("EsObject", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setNumber(string name, Number value)
		{
			validate("Number", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setNumber(string name, double value)
		{
			validate("Number", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setNumber(string name, float value)
		{
			validate("Number", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, (double)value));
		}

		public void setNumber(string name, long value)
		{
			validate("Number", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, (double)value));
		}

		public void setNumber(string name, int value)
		{
			validate("Number", name);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, (double)value));
		}

		public void setIntegerArray(string name, int[] value)
		{
			validate("IntegerArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setStringArray(string name, string[] value)
		{
			validate("stringArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setDoubleArray(string name, double[] value)
		{
			validate("DoubleArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setFloatArray(string name, float[] value)
		{
			validate("FloatArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setBooleanArray(string name, bool[] value)
		{
			validate("BooleanArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setByteArray(string name, byte[] value)
		{
			validate("ByteArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setCharArray(string name, char[] value)
		{
			validate("CharArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setLongArray(string name, long[] value)
		{
			validate("LongArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setShortArray(string name, short[] value)
		{
			validate("ShortArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setEsObjectArray(string name, EsObject[] value)
		{
			validate("EsObjectArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			for (int i = 0; i < value.Length; i++)
			{
				validate("EsObject", name + "[" + i + "]", value[i]);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		public void setNumberArray(string name, Number[] value)
		{
			validate("NumberArray", name, value);
			if (data.ContainsKey(name))
			{
				data.Remove(name);
			}
			for (int i = 0; i < value.Length; i++)
			{
				validate("Number", name + "[" + i + "]", value[i]);
			}
			data.Add(name, new EsObjectDataHolder(name, value));
		}

		private void validate(string type, string name)
		{
			if (name == null)
			{
				throw new ArgumentException("Attempted to call set" + type + " with a null name!");
			}
		}

		private void validate(string type, string name, object variable)
		{
			validate(type, name);
			if (variable == null)
			{
				throw new ArgumentException("Attempted to call set" + type + " with a null value for " + name + "!");
			}
		}

		private EsObjectDataHolder getHolderForName(string name)
		{
			EsObjectDataHolder esObjectDataHolder = data[name];
			if (esObjectDataHolder == null)
			{
				throw new Exception("Unable to locate variable named '" + name + "'");
			}
			return esObjectDataHolder;
		}

		public int getInteger(string name)
		{
			return getHolderForName(name).getIntValue();
		}

		public int getInteger(string name, int defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getInteger(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public string getString(string name)
		{
			return getHolderForName(name).getStringValue();
		}

		public string getString(string name, string defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getString(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public double getDouble(string name)
		{
			return getHolderForName(name).getDoubleValue();
		}

		public double getDouble(string name, double defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getDouble(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public float getFloat(string name)
		{
			return getHolderForName(name).getFloatValue();
		}

		public float getFloat(string name, float defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getFloat(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public bool getBoolean(string name)
		{
			return getHolderForName(name).getBooleanValue();
		}

		public bool getBoolean(string name, bool defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getBoolean(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public byte getByte(string name)
		{
			return getHolderForName(name).getByteValue();
		}

		public byte getByte(string name, byte defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getByte(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public char getChar(string name)
		{
			return getHolderForName(name).getCharValue();
		}

		public char getChar(string name, char defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getChar(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public long getLong(string name)
		{
			return getHolderForName(name).getLongValue();
		}

		public long getLong(string name, long defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getLong(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public short getShort(string name)
		{
			return getHolderForName(name).getShortValue();
		}

		public short getShort(string name, short defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getShort(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public EsObject getEsObject(string name)
		{
			return getHolderForName(name).getEsObjectValue();
		}

		public EsObject getEsObject(string name, EsObject defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getEsObject(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public Number getNumber(string name)
		{
			return getHolderForName(name).getNumberValue();
		}

		public Number getNumber(string name, Number defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getNumber(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public int[] getIntegerArray(string name)
		{
			return getHolderForName(name).getIntArrayValue();
		}

		public int[] getIntegerArray(string name, int[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getIntegerArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public string[] getStringArray(string name)
		{
			return getHolderForName(name).getStringArrayValue();
		}

		public string[] getStringArray(string name, string[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getStringArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public double[] getDoubleArray(string name)
		{
			return getHolderForName(name).getDoubleArrayValue();
		}

		public double[] getDoubleArray(string name, double[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getDoubleArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public float[] getFloatArray(string name)
		{
			return getHolderForName(name).getFloatArrayValue();
		}

		public float[] getFloatArray(string name, float[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getFloatArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public bool[] getBooleanArray(string name)
		{
			return getHolderForName(name).getBooleanArrayValue();
		}

		public bool[] getBooleanArray(string name, bool[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getBooleanArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public byte[] getByteArray(string name)
		{
			return getHolderForName(name).getByteArrayValue();
		}

		public byte[] getByteArray(string name, byte[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getByteArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public char[] getCharArray(string name)
		{
			return getHolderForName(name).getCharArrayValue();
		}

		public char[] getCharArray(string name, char[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getCharArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public long[] getLongArray(string name)
		{
			return getHolderForName(name).getLongArrayValue();
		}

		public long[] getLongArray(string name, long[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getLongArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public short[] getShortArray(string name)
		{
			return getHolderForName(name).getShortArrayValue();
		}

		public short[] getShortArray(string name, short[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getShortArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public EsObject[] getEsObjectArray(string name)
		{
			return getHolderForName(name).getEsObjectArrayValue();
		}

		public EsObject[] getEsObjectArray(string name, EsObject[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getEsObjectArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public Number[] getNumberArray(string name)
		{
			return getHolderForName(name).getNumberArrayValue();
		}

		public Number[] getNumberArray(string name, Number[] defaultValue)
		{
			if (variableExists(name))
			{
				try
				{
					return getNumberArray(name);
				}
				catch (Exception)
				{
					return defaultValue;
				}
			}
			return defaultValue;
		}

		public void removeVariable(string name)
		{
			data.Remove(name);
		}

		public void removeAll()
		{
			data.Clear();
		}

		public object getRawVariable(string name)
		{
			return getHolderForName(name).getRawValue();
		}

		public bool variableExists(string name)
		{
			return data.ContainsKey(name);
		}

		public override string ToString()
		{
			string empty = string.Empty;
			return tostring(empty);
		}

		public string tostring(string tabs)
		{
			string text = "{EsObject:\n";
			tabs += "\t";
			foreach (KeyValuePair<string, EsObjectDataHolder> datum in data)
			{
				EsObjectDataHolder value = datum.Value;
				string name = value.Name;
				string text2 = text;
				text = text2 + tabs + name + ":" + value.getDataType().ToString() + " = ";
				string text3 = tabs + "\t";
				switch (value.getDataType())
				{
				case DataType.EsObject:
					text += ((EsObject)value.getRawValue()).tostring(tabs);
					break;
				case DataType.Integer:
				case DataType.String:
				case DataType.Double:
				case DataType.Float:
				case DataType.Boolean:
				case DataType.Byte:
				case DataType.Character:
				case DataType.Long:
				case DataType.Short:
					text += value.getRawValue().ToString();
					break;
				case DataType.Number:
				{
					Number numberValue = value.getNumberValue();
					text += numberValue.getValue();
					break;
				}
				case DataType.BooleanArray:
				{
					text = text + "\n" + text3 + "[\n";
					bool[] booleanArrayValue = value.getBooleanArrayValue();
					for (int num8 = 0; num8 < booleanArrayValue.Length; num8++)
					{
						string text9 = text;
						bool flag = booleanArrayValue[num8];
						text = text9 + text3 + flag;
						text = ((num8 == booleanArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.CharacterArray:
				{
					text = text + "\n" + text3 + "[\n";
					char[] charArrayValue = value.getCharArrayValue();
					for (int num4 = 0; num4 < charArrayValue.Length; num4++)
					{
						string text7 = text;
						char c = charArrayValue[num4];
						text = text7 + text3 + c;
						text = ((num4 == charArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.DoubleArray:
				{
					text = text + "\n" + text3 + "[\n";
					double[] doubleArrayValue = value.getDoubleArrayValue();
					for (int m = 0; m < doubleArrayValue.Length; m++)
					{
						text = text + text3 + doubleArrayValue[m];
						text = ((m == doubleArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.ByteArray:
				{
					text = text + "\n" + text3 + "[\n";
					byte[] byteArrayValue = value.getByteArrayValue();
					for (int j = 0; j < byteArrayValue.Length; j++)
					{
						string text4 = text;
						byte b = byteArrayValue[j];
						text = text4 + text3 + b;
						text = ((j == byteArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.FloatArray:
				{
					text = text + "\n" + text3 + "[\n";
					float[] floatArrayValue = value.getFloatArrayValue();
					for (int num7 = 0; num7 < floatArrayValue.Length; num7++)
					{
						text = text + text3 + floatArrayValue[num7];
						text = ((num7 == floatArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.IntegerArray:
				{
					text = text + "\n" + text3 + "[\n";
					int[] intArrayValue = value.getIntArrayValue();
					for (int num5 = 0; num5 < intArrayValue.Length; num5++)
					{
						string text8 = text;
						int num6 = intArrayValue[num5];
						text = text8 + text3 + num6;
						text = ((num5 == intArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.LongArray:
				{
					text = text + "\n" + text3 + "[\n";
					long[] longArrayValue = value.getLongArrayValue();
					for (int num2 = 0; num2 < longArrayValue.Length; num2++)
					{
						string text6 = text;
						long num3 = longArrayValue[num2];
						text = text6 + text3 + num3;
						text = ((num2 == longArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.NumberArray:
				{
					text = text + "\n" + text3 + "[\n";
					Number[] numberArrayValue = value.getNumberArrayValue();
					for (int n = 0; n < numberArrayValue.Length; n++)
					{
						Number number = numberArrayValue[n];
						text = text + text3 + number.getValue();
						text = ((n == numberArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.ShortArray:
				{
					text = text + "\n" + text3 + "[\n";
					short[] shortArrayValue = value.getShortArrayValue();
					for (int l = 0; l < shortArrayValue.Length; l++)
					{
						string text5 = text;
						short num = shortArrayValue[l];
						text = text5 + text3 + num;
						text = ((l == shortArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.StringArray:
				{
					text = text + "\n" + text3 + "[\n";
					string[] stringArrayValue = value.getStringArrayValue();
					for (int k = 0; k < stringArrayValue.Length; k++)
					{
						text = text + text3 + stringArrayValue[k].ToString();
						text = ((k == stringArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				case DataType.EsObjectArray:
				{
					text = text + "\n" + text3 + "[\n";
					EsObject[] esObjectArrayValue = value.getEsObjectArrayValue();
					for (int i = 0; i < esObjectArrayValue.Length; i++)
					{
						EsObject esObject = esObjectArrayValue[i];
						text = text + text3 + esObject.tostring(tabs);
						text = ((i == esObjectArrayValue.Length - 1) ? (text + "\n" + text3 + "]") : (text + ",\n"));
					}
					break;
				}
				}
				text += "\n";
			}
			text = text.Substring(0, text.Length - 1);
			return text + "\n" + tabs + "}";
		}

		public EsObject shallowClone()
		{
			EsObject esObject = new EsObject();
			esObject.addAll(this);
			return esObject;
		}

		public EsObject deepClone()
		{
			EsObject esObject = shallowClone();
			foreach (KeyValuePair<string, EsObjectDataHolder> datum in data)
			{
				EsObjectDataHolder value = datum.Value;
				string name = value.Name;
				switch (value.getDataType())
				{
				case DataType.EsObject:
				{
					EsObject esObjectValue = value.getEsObjectValue();
					esObject.setEsObject(name, esObjectValue.deepClone());
					break;
				}
				case DataType.Number:
				{
					double value2 = value.getNumberValue().getValue();
					esObject.setNumber(name, new Number(value2));
					break;
				}
				case DataType.BooleanArray:
				{
					bool[] booleanArrayValue = value.getBooleanArrayValue();
					esObject.setBooleanArray(name, (bool[])booleanArrayValue.Clone());
					break;
				}
				case DataType.CharacterArray:
				{
					char[] charArrayValue = value.getCharArrayValue();
					esObject.setCharArray(name, (char[])charArrayValue.Clone());
					break;
				}
				case DataType.DoubleArray:
				{
					double[] doubleArrayValue = value.getDoubleArrayValue();
					esObject.setDoubleArray(name, (double[])doubleArrayValue.Clone());
					break;
				}
				case DataType.ByteArray:
				{
					byte[] byteArrayValue = value.getByteArrayValue();
					esObject.setByteArray(name, (byte[])byteArrayValue.Clone());
					break;
				}
				case DataType.FloatArray:
				{
					float[] floatArrayValue = value.getFloatArrayValue();
					esObject.setFloatArray(name, (float[])floatArrayValue.Clone());
					break;
				}
				case DataType.IntegerArray:
				{
					int[] intArrayValue = value.getIntArrayValue();
					esObject.setIntegerArray(name, (int[])intArrayValue.Clone());
					break;
				}
				case DataType.LongArray:
				{
					long[] longArrayValue = value.getLongArrayValue();
					esObject.setLongArray(name, (long[])longArrayValue.Clone());
					break;
				}
				case DataType.NumberArray:
				{
					Number[] numberArrayValue = value.getNumberArrayValue();
					int num2 = numberArrayValue.Length;
					Number[] array2 = new Number[num2];
					for (int j = 0; j < numberArrayValue.Length; j++)
					{
						array2[j] = new Number(numberArrayValue[j].getValue());
					}
					esObject.setNumberArray(name, array2);
					break;
				}
				case DataType.ShortArray:
				{
					short[] shortArrayValue = value.getShortArrayValue();
					esObject.setShortArray(name, (short[])shortArrayValue.Clone());
					break;
				}
				case DataType.StringArray:
				{
					string[] stringArrayValue = value.getStringArrayValue();
					esObject.setStringArray(name, (string[])stringArrayValue.Clone());
					break;
				}
				case DataType.EsObjectArray:
				{
					EsObject[] esObjectArrayValue = value.getEsObjectArrayValue();
					int num = esObjectArrayValue.Length;
					EsObject[] array = new EsObject[num];
					for (int i = 0; i < esObjectArrayValue.Length; i++)
					{
						EsObject esObject2 = esObjectArrayValue[i];
						array[i] = esObject2.deepClone();
					}
					esObject.setEsObjectArray(name, array);
					break;
				}
				}
			}
			return esObject;
		}
	}
}
