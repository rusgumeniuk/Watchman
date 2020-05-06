using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using Watchman.BusinessLogic.Models.PatientStates.HealthStates;

namespace Watchman.BusinessLogic.Models.Converters
{
    class HealthStateConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        { ContractResolver = new HealthStateResolver() };


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (!jo.TryGetValue("healthAuditSecondPeriod", out var val))
            {
                val = jo.GetValue("HealthAuditSecondPeriod");
            }
            switch ((ushort)val)
            {
                case 60:
                    return JsonConvert.DeserializeObject<NormalHealthState>(jo.ToString(), Settings);
                case 5:
                    return JsonConvert.DeserializeObject<ThreateningHealthState>(jo.ToString(), Settings);
                default:
                    throw new ArgumentException($"Unknown type {val}");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(PatientHealthState<Guid>));
        }

        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();//canWrite => false so we can't write and don't need to implement
        }
    }
}
