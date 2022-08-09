using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CustomerRelationshipManagement.UI.Models.DBContext
{
    public partial class CustomerRelationshipManagementDBContext : DbContext
    {
        //For user login
        public Registration GetUser(string name)
        {
            try
            {
                SqlParameter username = new SqlParameter("@name", name ?? (object)DBNull.Value);
                return (Registration)Registrations.FromSqlRaw("EXEC [dbo].[spValidateAccount] @name", username).AsEnumerable().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Role GetRole(int id)
        {
            try
            {
                SqlParameter roleid = new SqlParameter("@id",id);
                SqlParameter rolename = new SqlParameter
                {
                    ParameterName= "@rolename",
                    SqlDbType=SqlDbType.VarChar,
                    Direction= ParameterDirection.Output
                };
                return (Role)Roles.FromSqlRaw("EXEC [dbo].[spGetRole] @id ", roleid).AsEnumerable().FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //For Manage Customer
        public List<Customer> CustomerView()
        {
            return Customers.FromSqlRaw("EXEC [dbo].[spCustomerView]").ToList();
        }
        public Customer CustomerDetails(string id)
        {
            SqlParameter roleid = new SqlParameter("@id",Convert.ToInt16(id));
            return Customers.FromSqlRaw("EXEC [dbo].[spCustomerDetails] @id",roleid).AsEnumerable().FirstOrDefault();
        }
        public Customer CustomerEdit(Customer customer)
        {
            var x = customer;
            SqlParameter id = new SqlParameter("@id", customer.CustomerNo);
            SqlParameter name = new SqlParameter("@name", customer.CustomerName ?? (object)DBNull.Value);
            SqlParameter surname = new SqlParameter("@surname", customer.CustomerSurname ?? (object)DBNull.Value);
            SqlParameter address = new SqlParameter("@address", customer.Address ?? (object)DBNull.Value);
            SqlParameter postcode = new SqlParameter("@postcode", customer.PostCode ?? (object)DBNull.Value);
            SqlParameter country = new SqlParameter("@country", customer.Country ?? (object)DBNull.Value);
            SqlParameter dob = new SqlParameter("@dob", customer.DateOfBirth ?? (object)DBNull.Value);

            return Customers.FromSqlRaw("EXEC [dbo].[spCustomerEdit] @id, @name, @surname, @address, @postcode, @country, @dob",
                id, name,surname, address, postcode, country, dob).AsEnumerable().FirstOrDefault();
        }
        public Customer CustomerDelete(string id)
        {
            SqlParameter roleid = new SqlParameter("@id", Convert.ToInt16(id));
            return Customers.FromSqlRaw("EXEC [dbo].[spCustomerDelete] @id", roleid).AsEnumerable().FirstOrDefault();
        }
        public Customer CustomerCreate(Customer customer)
        {
            SqlParameter name = new SqlParameter("@name", customer.CustomerName ?? (object)DBNull.Value);
            SqlParameter surname = new SqlParameter("@surname", customer.CustomerSurname ?? (object)DBNull.Value);
            SqlParameter address = new SqlParameter("@address", customer.Address ?? (object)DBNull.Value);
            SqlParameter postcode = new SqlParameter("@postcode", customer.PostCode ?? (object)DBNull.Value);
            SqlParameter country = new SqlParameter("@country", customer.Country ?? (object)DBNull.Value);
            SqlParameter dob = new SqlParameter("@dob", customer.DateOfBirth ?? (object)DBNull.Value);

            return Customers.FromSqlRaw("EXEC [dbo].[spCustomerCreate] @name, @surname, @address, @postcode, @country, @dob",
                name, surname, address, postcode, country, dob).AsEnumerable().FirstOrDefault();
        }
    }
}
