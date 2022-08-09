using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public class CallByName
    {
        public string CustomerName { get; set; }
        public int CustomerCall_id { get; set; }
        public int CustomerNo { get; set; }
        public DateTime? DateOfCall { get; set; }
        public DateTime? TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
