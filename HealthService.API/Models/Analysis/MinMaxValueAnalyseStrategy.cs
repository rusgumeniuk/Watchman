using HealthService.API.Models.Users;

using System;
using System.Collections.Generic;
using System.Linq;

using Watchman.BusinessLogic.Models.Analysis;
using Watchman.BusinessLogic.Models.Signs;
using Watchman.BusinessLogic.Models.Users;

namespace HealthService.API.Models.Analysis
{
    public class MinMaxValueAnalyseStrategy : IAnalysisStrategy
    {
        public IAnalysisResult<Guid, Guid, Guid> Analyze<TPatientKey>(HealthMeasurement<Guid, Guid> healthState, Patient<TPatientKey> patient) where TPatientKey : IEquatable<TPatientKey>
        {
            AnalysisResult result = new AnalysisResult() { HealthState = healthState as HeartAndPressureHealthState };
            foreach (var sign in healthState.Signs)
            {
                if (IsIgnorableSign(patient, sign))
                    continue;

                var valueWithActivityRate = sign.Value * patient?.CurrentActivityState?.ChangeFactor ?? 1;

                //TODO: create property\entity\repository which will store min max values (or create min max in signs again?)
                switch (sign.GetType().Name)
                {
                    case "SYS":
                        {
                            if (valueWithActivityRate < 95)
                                result.Notices.Add(new HealthNotice() { Sign = sign as Sign, Comment = "U have some small sys" });
                            else if (valueWithActivityRate > 180)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"Call to your doctor, because u may get hypertensive crisis" });
                            else if (valueWithActivityRate > 130)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"U have high blood pressure" });
                            break;
                        }
                    case "DIA":
                        {
                            if (valueWithActivityRate < 40)
                                result.Notices.Add(new HealthNotice() { Sign = sign as Sign, Comment = "U have some small sys" });
                            else if (valueWithActivityRate > 120)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"Call to your doctor, because u may get hypertensive crisis" });
                            else if (valueWithActivityRate > 80)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"U have high blood pressure" });
                            break;
                        }
                    case "HeartRate":
                        {
                            if (valueWithActivityRate < 60)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = "U have some small heart rate" });
                            else if (valueWithActivityRate > 150)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"Call to your emergency, because u may get heart issue" });
                            else if (valueWithActivityRate > 100)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"U have high heart rate" });
                            break;
                        }

                    default:
                        break;
                }
            }
            return result;
        }
        public IAnalysisResult<Guid, Guid, Guid> AnalyzeLast<TPatientKey>(Patient<TPatientKey> patient) where TPatientKey : IEquatable<TPatientKey>
        {
            var last = patient.HealthMeasurements.ElementAt(GetIndexOfItemWithNewDate((patient as PatientProfile).HealthMeasurements));
            AnalysisResult result = new AnalysisResult() { HealthState = last as HeartAndPressureHealthState };
            foreach (var sign in last.Signs)
            {
                if (IsIgnorableSign(patient, sign as Sign))
                    continue;

                var valueWithActivityRate = sign.Value * (patient?.CurrentActivityState?.ChangeFactor ?? 1);

                //TODO: create property\entity\repository which will store min max values (or create min max in signs again?)
                switch (sign.GetType().Name)
                {
                    case "SYS":
                        {
                            if (valueWithActivityRate < 95)
                                result.Notices.Add(new HealthNotice() { Sign = sign as Sign, Comment = "U have some small sys" });
                            else if (valueWithActivityRate > 180)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"Call to your doctor, because u may get hypertensive crisis" });
                            else if (valueWithActivityRate > 130)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"U have high blood pressure" });
                            break;
                        }
                    case "DIA":
                        {
                            if (valueWithActivityRate < 40)
                                result.Notices.Add(new HealthNotice() { Sign = sign as Sign, Comment = "U have some small sys" });
                            else if (valueWithActivityRate > 120)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"Call to your doctor, because u may get hypertensive crisis" });
                            else if (valueWithActivityRate > 80)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"U have high blood pressure" });
                            break;
                        }
                    case "HeartRate":
                        {
                            if (valueWithActivityRate < 60)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = "U have some small heart rate" });
                            else if (valueWithActivityRate > 150)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"Call to your emergency, because u may get heart issue" });
                            else if (valueWithActivityRate > 100)
                                result.Threats.Add(new HealthThreat() { Sign = sign as Sign, Comment = $"U have high heart rate" });
                            break;
                        }

                    default:
                        break;
                }
            }
            return result;
        }

        private int GetIndexOfItemWithNewDate(IEnumerable<HealthMeasurement<Guid, Guid>> list)
        {
            if (list.Count() < 1)
                return -1;
            int index = 0;
            for (int i = 0; i < list.Count() - 1; ++i)
            {
                if (list.ElementAt(i).MeasurementTime <= list.ElementAt(i + 1).MeasurementTime)
                    index = i + 1;
            }
            return index;
        }
        private bool IsIgnorableSign<TPatientKey>(Patient<TPatientKey> patient, Sign<Guid> sign) where TPatientKey : IEquatable<TPatientKey>
        {
            return patient.IgnorableSignPair.FirstOrDefault(pair => pair.PatientId.Equals(patient.Id) && pair.SignType.Equals(sign.GetType().ToString())) != null;
        }
    }
}
