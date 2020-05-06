using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Converters
{
    public class HealthMeasurementConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        { ContractResolver = new HealthMeasurementResolver() };


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            //TODO
            return JsonConvert.DeserializeObject<HeartAndPressureHealthState>(jo.ToString(), Settings);
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(HealthMeasurement<Guid, Guid>));
        }

        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();//canWrite => false so we can't write and don't need to implement
        }
    }
}
