using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ByList
{
    public class DataMember
    {
        [DataMember(Name = "ItemCode", Order = 0)]
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string MobNo { get; set; }
        public string Dob { get; set; }
    }

    public class Item
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Pack { get; set; }
        public string Qty { get; set; }
        public bool Status { get; set; }        
    }

    public class AcmData
    {
        public string Code { get; set; }
        public string AlterCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LStatus { get; set; }
    }

    public class CourseData
    {
        public string Id { get; set; }
        public string Course { get; set; }
        public string Year { get; set; }
        public string Earning { get; set; }
    }
}