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
    public class Transaction 
    {
        [DataMember]
        [Required]
        public string Id { get; set; }
        [DataMember]
        [Required]
        public string AccountNo { get; set; }
        [DataMember]
        [Required]
        public DateTime transDate { get; set; }
        [DataMember]
        [Required]
        public double Amount { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    [DataContract]
    [Serializable]
    public class TransactionResponse
    {
        private List<Transaction> _transactions = new List<Transaction>();

        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public List<Transaction> Transactions
        {
            get
            {
                if (_transactions != null)
                {
                    return _transactions;
                }

                return _transactions;
            }
            set
            {
                _transactions = value;
            }
        }
    }
}