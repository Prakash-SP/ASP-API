using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplication2.Models
{
    public class EmpData
    {
        [DataMember(Order = 0)]
        public string Id { get; set; }
        [DataMember(Order = 1)]
        public string Name { get; set; }
        [DataMember(Order = 2)]
        public string Dob { get; set; }
        [DataMember(Order = 3)]
        public string Gender { get; set; }
        [DataMember(Order = 4)]
        public string Email { get; set; }
        [DataMember(Order = 5)]
        public string Post { get; set; }
        [DataMember(Order = 6)]
        public long MobileNo { get; set; }
        [DataMember(Order = 7)]
        public string Image { get; set; }
        [DataMember(Order = 8)]
        public string ImageName { get; set; }
    }
}