using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;

using Watchman.BusinessLogic.Models.PatientStates.ActivityStates;

namespace Watchman.BusinessLogic.Models.Converters
{
    public class PatientActivityResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(PatientActivityState<Guid>).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }
}
