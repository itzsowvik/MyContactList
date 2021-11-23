using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Database
    {
        private static SqlConnection connection = new SqlConnection(@"Data Source=SOWVIK-DESKTOP\SOWVIKSERVER;Initial Catalog=contactlist;Integrated Security=True");

       


        public static bool AddNewContact(string Name, string Email, string Phone, string Address)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            string query = string.Format("insert into contactdetail (name, email, phone, address) values('{0}','{1}','{2}','{3}')" ,Name, Email, Phone, Address);
            SqlCommand cmd = new SqlCommand(query, connection);
            int RowsAffected = cmd.ExecuteNonQuery();
            connection.Close();

            if (RowsAffected > 0)
            {
                return true;
            }

            return false;
        }

       public static DataTable ReadData()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            DataTable dt = new DataTable();

            string query = string.Format("select * from contactdetail");

            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            connection.Close();

            return dt;
        }
        public static bool UpdateContact(int id, string name, string email, string phone, string address)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            string query = string.Format("update contactdetail set name='{0}', email = '{1}', phone = '{2}', address = '{3}' where id ={4}", name, email, phone, address, id);
            SqlCommand cmd = new SqlCommand(query,connection);

            int RowsAffected = -1;

            RowsAffected =  cmd.ExecuteNonQuery();
            connection.Close();

            if(RowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool DeleteContact(int id)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            string query = string.Format("delete from contactdetail where id = {0}", id);
            SqlCommand cmd = new SqlCommand(query,connection);
            int RowsAffected = cmd.ExecuteNonQuery();

            connection.Close();

            if(RowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        
    }
}
