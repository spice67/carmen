using Core.Common.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ME.Account.Web.Models.api
{
    [DataContract]
    [Serializable]
    public class CustomerAccount 
    {
        [DataMember]
        [Required]
        public string CustomerId { get; set; }
        [DataMember]
        [Required]
        // this is the accountno by definition
        public string AccountNo { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}