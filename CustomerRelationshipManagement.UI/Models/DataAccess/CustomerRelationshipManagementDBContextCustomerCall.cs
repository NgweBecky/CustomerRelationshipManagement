using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRelationshipManagement.UI.Models.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class CustomerRelationshipManagementDBContext : DbContext
    {
        //For Manage Customer
        public List<CallByName> CustomerCallView()
        {
            return CallByNames.FromSqlRaw("EXEC [dbo].[spCustomerCallView]").ToList();
        }
        public CallByName CustomerCallDetails(string id)
        {
            SqlParameter roleid = new SqlParameter("@id", Convert.ToInt16(id));
            var x= CallByNames.FromSqlRaw("EXEC [dbo].[spCustomerCallDetails] @id", roleid).AsEnumerable().FirstOrDefault();
            return x;
        }
        public CallByName CustomerCallEdit(CallByName customer)
        {
            SqlParameter name = new SqlParameter("@id", customer.CustomerCall_id);
            SqlParameter date = new SqlParameter("@date", customer.DateOfCall ?? (object)DBNull.Value);
            SqlParameter time = new SqlParameter("@time", customer.TimeOfCall ?? (object)DBNull.Value);
            SqlParameter subj = new SqlParameter("@subj", customer.Subject ?? (object)DBNull.Value);
            SqlParameter desc = new SqlParameter("@desc", customer.Description ?? (object)DBNull.Value);

            return CallByNames.FromSqlRaw("EXEC [dbo].[spCustomerCallEdit] @id, @date, @time, @subj, @desc",
                name, date, time, subj, desc).AsEnumerable().FirstOrDefault();
        }
        public CallByName CustomerCallDelete(string id)
        {
            SqlParameter roleid = new SqlParameter("@id", Convert.ToInt16(id));
            return CallByNames.FromSqlRaw("EXEC [dbo].[spCustomercallDelete] @id", roleid).AsEnumerable().FirstOrDefault();
        }
        public CallByName CustomerCallCreate(CallByName customer)
        {

            SqlParameter name = new SqlParameter("@name", customer.CustomerName);
            SqlParameter date = new SqlParameter("@date", customer.DateOfCall ?? (object)DBNull.Value);
            SqlParameter time = new SqlParameter("@time", customer.TimeOfCall ?? (object)DBNull.Value);
            SqlParameter subj = new SqlParameter("@subj", customer.Subject ?? (object)DBNull.Value);
            SqlParameter desc = new SqlParameter("@desc", customer.Description ?? (object)DBNull.Value);

            return CallByNames.FromSqlRaw("EXEC [dbo].[spCustomerCallCreate] @name, @date, @time, @subj, @desc",
                name, date, time, subj, desc).AsEnumerable().FirstOrDefault();
        }
    }
}
