using Baidu.Yingyan.Fence;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Baidu.Yingyan.Converters
{
    /// <summary>
    /// 围栏信息转换
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class FenceBaseInfoConverter : JsonConverter
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FenceBaseData);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var t = jsonObject["shape"].Value<string>();
            FenceShapeEnums type = FenceShapeEnums.circle;
            FenceBaseData value = null;
            Enum.TryParse(t, true, out type);

            switch (type)
            {
                case FenceShapeEnums.polygon:
                    value = new FenceCreatePolygonFenceParam();
                    break;

                case FenceShapeEnums.polyline:
                    value = new FenceCreatePolylineFenceParam();
                    break;

                case FenceShapeEnums.circle:
                default:
                    value = new FenceCreateCircleFenceParam();
                    break;

                case FenceShapeEnums.district:
                    value = new FenceCreateDistrictFenceParam();
                    break;
            }

            serializer.Populate(jsonObject.CreateReader(), value);
            return value;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}