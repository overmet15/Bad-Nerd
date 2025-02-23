using System;
using System.Collections.Generic;
using System.IO;

namespace Electrotank.Electroserver5.Api.Helper
{
	public class EsObjectCodec
	{
		private static Dictionary<DataType, byte> dataTypeToCharacterIndicator;

		private static Dictionary<byte, DataType> characterIndicatorToDataType;

		static EsObjectCodec()
		{
			dataTypeToCharacterIndicator = new Dictionary<DataType, byte>();
			characterIndicatorToDataType = new Dictionary<byte, DataType>(Comparers.Bytes);
			dataTypeToCharacterIndicator.Add(DataType.Integer, 48);
			dataTypeToCharacterIndicator.Add(DataType.String, 49);
			dataTypeToCharacterIndicator.Add(DataType.Double, 50);
			dataTypeToCharacterIndicator.Add(DataType.Float, 51);
			dataTypeToCharacterIndicator.Add(DataType.Boolean, 52);
			dataTypeToCharacterIndicator.Add(DataType.Byte, 53);
			dataTypeToCharacterIndicator.Add(DataType.Character, 54);
			dataTypeToCharacterIndicator.Add(DataType.Long, 55);
			dataTypeToCharacterIndicator.Add(DataType.Short, 56);
			dataTypeToCharacterIndicator.Add(DataType.EsObject, 57);
			dataTypeToCharacterIndicator.Add(DataType.EsObjectArray, 97);
			dataTypeToCharacterIndicator.Add(DataType.IntegerArray, 98);
			dataTypeToCharacterIndicator.Add(DataType.StringArray, 99);
			dataTypeToCharacterIndicator.Add(DataType.DoubleArray, 100);
			dataTypeToCharacterIndicator.Add(DataType.FloatArray, 101);
			dataTypeToCharacterIndicator.Add(DataType.BooleanArray, 102);
			dataTypeToCharacterIndicator.Add(DataType.ByteArray, 103);
			dataTypeToCharacterIndicator.Add(DataType.CharacterArray, 104);
			dataTypeToCharacterIndicator.Add(DataType.LongArray, 105);
			dataTypeToCharacterIndicator.Add(DataType.ShortArray, 106);
			dataTypeToCharacterIndicator.Add(DataType.Number, 107);
			dataTypeToCharacterIndicator.Add(DataType.NumberArray, 108);
			foreach (DataType key in dataTypeToCharacterIndicator.Keys)
			{
				characterIndicatorToDataType.Add(dataTypeToCharacterIndicator[key], key);
			}
		}

		public static FlattenedEsObject FlattenEsObject(EsObject esObject)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamMessageWriter writer = new StreamMessageWriter(memoryStream);
			encode(writer, esObject);
			memoryStream.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, array.Length);
			FlattenedEsObject flattenedEsObject = new FlattenedEsObject();
			flattenedEsObject.EncodedEntries = array;
			return flattenedEsObject;
		}

		public static FlattenedEsObjectRO FlattenEsObject(EsObjectRO esObject)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamMessageWriter writer = new StreamMessageWriter(memoryStream);
			encode(writer, esObject);
			memoryStream.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			byte[] array = new byte[memoryStream.Length];
			memoryStream.Read(array, 0, array.Length);
			FlattenedEsObjectRO flattenedEsObjectRO = new FlattenedEsObjectRO();
			flattenedEsObjectRO.EncodedEntries = array;
			return flattenedEsObjectRO;
		}

		public static EsObject UnflattenEsObjectRO(FlattenedEsObjectRO fEsObject)
		{
			MemoryStream buffer = new MemoryStream(fEsObject.EncodedEntries);
			StreamMessageReader reader = new StreamMessageReader(buffer);
			return decode(reader);
		}

		public static EsObject UnflattenEsObject(FlattenedEsObject fEsObject)
		{
			MemoryStream buffer = new MemoryStream(fEsObject.EncodedEntries);
			StreamMessageReader reader = new StreamMessageReader(buffer);
			return decode(reader);
		}

		public static void encode(StreamMessageWriter writer, EsObjectRO o)
		{
			encode(writer, (EsObject)o);
		}

		public static void encode(StreamMessageWriter writer, EsObject o)
		{
			byte byteIn = 0;
			writer.WriteByte(byteIn);
			writer.WriteLength(o.getSize());
			foreach (EsObjectDataHolder item in o)
			{
				string name = item.Name;
				encodeObjectEntry(writer, item);
			}
		}

		private static void encodeObjectEntry(StreamMessageWriter writer, EsObjectDataHolder entry)
		{
			DataType dataType = entry.getDataType();
			writer.WriteByte(dataTypeToCharacterIndicator[dataType]);
			writer.WriteString(entry.Name);
			switch (dataType)
			{
			case DataType.Integer:
				writer.WriteInteger(entry.getIntValue());
				break;
			case DataType.String:
				writer.WriteString(entry.getStringValue());
				break;
			case DataType.Double:
				writer.WriteDouble(entry.getDoubleValue());
				break;
			case DataType.Float:
				writer.WriteFloat(entry.getFloatValue());
				break;
			case DataType.Boolean:
				writer.WriteBoolean(entry.getBooleanValue());
				break;
			case DataType.Byte:
				writer.WriteByte(entry.getByteValue());
				break;
			case DataType.Character:
				writer.WriteCharacter(entry.getCharValue());
				break;
			case DataType.Long:
				writer.WriteLong(entry.getLongValue());
				break;
			case DataType.Short:
				writer.WriteShort(entry.getShortValue());
				break;
			case DataType.Number:
				writer.WriteDouble(entry.getNumberValue().getValue());
				break;
			case DataType.EsObject:
				encode(writer, entry.getEsObjectValue());
				break;
			case DataType.EsObjectArray:
			{
				EsObject[] esObjectArrayValue = entry.getEsObjectArrayValue();
				writer.WriteLength(esObjectArrayValue.Length);
				EsObject[] array = esObjectArrayValue;
				foreach (EsObject o in array)
				{
					encode(writer, o);
				}
				break;
			}
			case DataType.IntegerArray:
				writer.WriteIntegerArray(entry.getIntArrayValue());
				break;
			case DataType.StringArray:
				writer.WriteStringArray(entry.getStringArrayValue());
				break;
			case DataType.DoubleArray:
				writer.WriteDoubleArray(entry.getDoubleArrayValue());
				break;
			case DataType.FloatArray:
				writer.WriteFloatArray(entry.getFloatArrayValue());
				break;
			case DataType.BooleanArray:
				writer.WriteBooleanArray(entry.getBooleanArrayValue());
				break;
			case DataType.ByteArray:
				writer.WriteByteArray(entry.getByteArrayValue());
				break;
			case DataType.CharacterArray:
				writer.WriteCharacterArray(entry.getCharArrayValue());
				break;
			case DataType.LongArray:
				writer.WriteLongArray(entry.getLongArrayValue());
				break;
			case DataType.ShortArray:
				writer.WriteShortArray(entry.getShortArrayValue());
				break;
			case DataType.NumberArray:
			{
				Number[] numberArrayValue = entry.getNumberArrayValue();
				writer.WriteLength(numberArrayValue.Length);
				foreach (Number number in numberArrayValue)
				{
					writer.WriteDouble(number.getValue());
				}
				break;
			}
			default:
				throw new Exception("Unable to encode data type " + dataType);
			}
		}

		public static EsObject decode(StreamMessageReader reader)
		{
			byte b = reader.NextByte();
			int num = reader.NextLength();
			EsObject esObject = new EsObject();
			for (int i = 0; i < num; i++)
			{
				byte key = reader.NextByte();
				DataType dataType = characterIndicatorToDataType[key];
				string name = reader.NextString();
				switch (dataType)
				{
				case DataType.Integer:
					esObject.setInteger(name, reader.NextInteger());
					break;
				case DataType.String:
					esObject.setString(name, reader.NextString());
					break;
				case DataType.Double:
					esObject.setDouble(name, reader.NextDouble());
					break;
				case DataType.Float:
					esObject.setFloat(name, reader.NextFloat());
					break;
				case DataType.Boolean:
					esObject.setBoolean(name, reader.NextBoolean());
					break;
				case DataType.Byte:
					esObject.setByte(name, reader.NextByte());
					break;
				case DataType.Character:
					esObject.setChar(name, reader.NextCharacter());
					break;
				case DataType.Long:
					esObject.setLong(name, reader.NextLong());
					break;
				case DataType.Short:
					esObject.setShort(name, reader.NextShort());
					break;
				case DataType.Number:
					esObject.setNumber(name, reader.NextDouble());
					break;
				case DataType.EsObject:
					esObject.setEsObject(name, decode(reader));
					break;
				case DataType.EsObjectArray:
				{
					int num2 = reader.NextLength();
					EsObject[] array3 = new EsObject[num2];
					for (int k = 0; k < num2; k++)
					{
						array3[k] = decode(reader);
					}
					esObject.setEsObjectArray(name, array3);
					break;
				}
				case DataType.IntegerArray:
					esObject.setIntegerArray(name, reader.nextIntegerArray());
					break;
				case DataType.StringArray:
					esObject.setStringArray(name, reader.NextStringArray());
					break;
				case DataType.DoubleArray:
					esObject.setDoubleArray(name, reader.NextDoubleArray());
					break;
				case DataType.FloatArray:
					esObject.setFloatArray(name, reader.NextFloatArray());
					break;
				case DataType.BooleanArray:
					esObject.setBooleanArray(name, reader.nextBooleanArray());
					break;
				case DataType.ByteArray:
					esObject.setByteArray(name, reader.NextByteArray());
					break;
				case DataType.CharacterArray:
					esObject.setCharArray(name, reader.NextCharacterArray());
					break;
				case DataType.LongArray:
					esObject.setLongArray(name, reader.NextLongArray());
					break;
				case DataType.ShortArray:
					esObject.setShortArray(name, reader.NextShortArray());
					break;
				case DataType.NumberArray:
				{
					double[] array = reader.NextDoubleArray();
					Number[] array2 = new Number[array.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = new Number(array[j]);
					}
					esObject.setNumberArray(name, array2);
					break;
				}
				default:
					throw new Exception("Unable to encode data type " + dataType);
				}
			}
			return esObject;
		}
	}
}
