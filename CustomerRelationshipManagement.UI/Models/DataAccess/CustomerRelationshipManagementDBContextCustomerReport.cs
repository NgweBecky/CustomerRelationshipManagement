using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationshipManagement.UI.Models.DBContext;
using Microsoft.EntityFrameworkCore;

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class CustomerRelationshipManagementDBContext : DbContext
    {
        public CallByName CustomerCallList(string id)
        {
            SqlParameter roleid = new SqlParameter("@id", id?? (object)DBNull.Value);
            var x = CallByNames.FromSqlRaw("EXEC [dbo].[spCustomerCallList] @id", roleid).AsEnumerable().FirstOrDefault();
            return x;
        }
    }
}
