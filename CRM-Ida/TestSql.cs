using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace CRM_Ida
{
    public class TestSql
    {
        private string conString = @"Server = (localdb)\mssqllocaldb; Database = CRM-Ida; Trusted_Connection = True";

        public void ReadName()
        {
            var sql = @"SELECT ID, FirstName, LastName
                    FROM Customer";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var firstName = reader.GetString(1);
                    var lastName = reader.GetString(2);
                    var ID = reader.GetInt32(0);
                    System.Console.WriteLine($"Kund: {ID} {firstName} {lastName}");
                };

            }

            Console.ReadLine();
        }

        public Customer MakeCustomerFromId(int id)
        {
            var sql = $@"SELECT FirstName, LastName, Epost, PhoneNumber FROM Customer WHERE ID = '{id}'";
            var customer = new Customer();

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customer.FirstName = reader.GetString(0);
                    customer.LastName = reader.GetString(1);
                    customer.Epost = reader.GetString(2);
                    customer.PhoneNumber = reader.GetString(3);
                }
            }
            return customer;
        }



        public void UpdateCustomerToDatabase(Customer customer, int id)
        {

            var sql = $@"UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Epost = @Epost, PhoneNumber = @PhoneNumber  WHERE ID = @id ";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                command.Parameters.Add(new SqlParameter("Epost", customer.Epost));
                command.Parameters.Add(new SqlParameter("PhoneNumber", customer.PhoneNumber));
                command.Parameters.Add(new SqlParameter("ID", id));
                command.ExecuteNonQuery();
            }

        }


        public void InstertCustomerToDatabase(Customer customer)
        {
            var sql = @"INSERT INTO Customer(FirstName, LastName, Epost, PhoneNumber) VALUES (@FirstName, @LastName, @Epost, @PhoneNumber)";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                command.Parameters.Add(new SqlParameter("Epost", customer.Epost));
                command.Parameters.Add(new SqlParameter("PhoneNumber", customer.PhoneNumber));
                command.ExecuteNonQuery();
            }

        }

        public void DeleteCustomerFormDatabase(int id)
        {
            var sql = $@"DELETE FROM Customer WHERE ID = @id";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("ID", id));
                command.ExecuteNonQuery();
            }

        }



    }
}