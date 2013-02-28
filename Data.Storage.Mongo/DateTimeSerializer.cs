using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Codell.Pies.Data.Storage.Mongo
{
    internal class DateTimeSerializer : BsonBaseSerializer
    {
        public override object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            VerifyTypes(nominalType, actualType, typeof(DateTime));
            var currentBsonType = bsonReader.CurrentBsonType;
            if (currentBsonType == BsonType.Null)
            {
                bsonReader.ReadNull();
                return null;
            }
            if (currentBsonType == BsonType.String)
            {
                return DateTime.Parse(bsonReader.ReadString());
            }
            if (currentBsonType == BsonType.Document)
            {
                bsonReader.ReadStartDocument();
                var value = DateTime.Parse(bsonReader.FindStringElement(ElementNames.Raw));
                bsonReader.ReadEndDocument();
                return value;
            }

            var native = new DateTimeOffsetSerializer();
            return native.Deserialize(bsonReader, nominalType, actualType, options);
        }

        public override void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            if (value == null)
            {
                bsonWriter.WriteNull();
            }
            else
            {
                bsonWriter.WriteStartDocument();
                var bsonDateTime = new BsonDateTime((DateTime)value);
                bsonWriter.WriteDateTime(ElementNames.Value, bsonDateTime.MillisecondsSinceEpoch);
                bsonWriter.WriteString(ElementNames.Raw, value.ToString());
                bsonWriter.WriteEndDocument();
            }
        }   
      
        public static class ElementNames
        {
            /// <summary>
            /// Mongo UTC converted date time
            /// </summary>
            public const string Value = "Value";

            /// <summary>
            /// Raw date time that is not converted to UTC
            /// </summary>
            public const string Raw = "Raw";
        }
    }
}