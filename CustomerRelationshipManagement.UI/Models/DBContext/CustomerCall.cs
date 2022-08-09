using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class CustomerCall
    {
        public int CustomerCallId { get; set; }
        public int CustomerNo { get; set; }
        public DateTime? DateOfCall { get; set; }
        public DateTime? TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public virtual Customer CustomerNoNavigation { get; set; }
    }
}
