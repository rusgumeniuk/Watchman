using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;
using Watchman.BusinessLogic.Models.Signs;

namespace Watchman.BusinessLogic.Models.Converters
{
    public class SignConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
        { ContractResolver = new SignResolver() };


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (!jo.TryGetValue("type", out var val))
            {
                val = jo.GetValue("Type");
            }
            switch ((string)val)
            {
                case "DIA":
                    return JsonConvert.DeserializeObject<DIA>(jo.ToString(), Settings);
                case "SYS":
                    return JsonConvert.DeserializeObject<SYS>(jo.ToString(), Settings);
                case "HeartRate":
                    return JsonConvert.DeserializeObject<HeartRate>(jo.ToString(), Settings);
                default:
                    throw new ArgumentException($"Unknown type {val}");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(PatientActivityState<Guid>));
        }

        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();//canWrite => false so we can't write and don't need to implement
        }
    }
}
