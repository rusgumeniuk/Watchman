using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;

namespace Watchman.BusinessLogic.Models.Converters
{
    public class PatientActivityConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        { ContractResolver = new PatientActivityResolver() };


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            switch ((float)jo.GetValue("changeFactor"))
            {
                case 1.2f:
                    return JsonConvert.DeserializeObject<SleepActivityState>(jo.ToString(), _settings);
                case 1f:
                    return JsonConvert.DeserializeObject<CasualActivityState>(jo.ToString(), _settings);
                case 0.75f:
                    return JsonConvert.DeserializeObject<SportActivityState>(jo.ToString(), _settings);
                default:
                    throw new Exception();
            }
            throw new NotImplementedException();
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
