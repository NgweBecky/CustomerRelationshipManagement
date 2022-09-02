using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerCalls = new HashSet<CustomerCall>();
        }

        public int CustomerNo { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerSurname { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<CustomerCall> CustomerCalls { get; set; }
    }
}
