using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Watchman.BusinessLogic.Models.Users
{
    public abstract class User<TKey> : User<TKey, TKey, TKey, TKey, TKey, TKey, TKey, TKey>
        where TKey : IEquatable<TKey>
    { }
    public abstract class User<TKey, TPatientKey, THealthMeasurementKey, TPatientHealthKey, TActivityStateKey, TSignKey, TWatchmanKey, TPersonalInfoKey> : IIdentifiedEntity<TKey>
        where TKey : IEquatable<TKey>
        where TPatientKey : IEquatable<TPatientKey>
        where THealthMeasurementKey : IEquatable<THealthMeasurementKey>
        where TPatientHealthKey : IEquatable<TPatientHealthKey>
        where TActivityStateKey : IEquatable<TActivityStateKey>
        where TSignKey : IEquatable<TSignKey>
        where TWatchmanKey : IEquatable<TWatchmanKey>
    {
        public TKey Id { get; set; }        
        public TKey PatientId { get; set; }        
        public TKey WatcmanId { get; set; }        
        public TKey PersonalInformationId { get; set; }       
    }
}
