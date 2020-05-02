using Core.Common.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ME.AccntRedis.Models
{
    [DataContract]
	[Serializable]
    public class CustomerRequest 
    {
        [DataMember]
        [Required]
        public string customerId { get; set; }
        [DataMember]
        [Required]
        public double initialCredit { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Customer
    {
        [DataMember]
        [Required]
        // this is the customerId by definition
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        [Required]
        public string Surname { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}