using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class Registration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? RoleId { get; set; }
    }
}
