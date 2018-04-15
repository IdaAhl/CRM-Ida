using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace CRM_Ida
{
    public class TestSql
    {
        private string conString = @"Server = (localdb)\mssqllocaldb; Database = CRM-Ida; Trusted_Connection = True";


        public void ReadName()
        {
            var sql = @"SELECT Customer.ID, FirstName, LastName, Epost, Number As PhoneNumber
FROM Customer
LEFT JOIN PhoneNumber ON Customer.ID = PhoneNumber.CustomerID ";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ID = reader.GetInt32(0);

                    var firstName = reader.GetSqlString(1);
                    if (!firstName.IsNull)
                        firstName = reader.GetString(1);

                    var lastName = reader.GetSqlString(2);
                    if (!lastName.IsNull)
                        lastName = reader.GetString(2);

                    var epost = reader.GetSqlString(3);
                    if (!epost.IsNull)
                        epost = reader.GetString(3);

                    var phoneNumber = reader.GetSqlString(4);
                    if (!phoneNumber.IsNull)
                        phoneNumber = reader.GetString(4);

                    System.Console.WriteLine($"Kund: {ID} {firstName} {lastName} {epost} {phoneNumber}");
                };

            }
            Console.ReadLine();
        }

        public Customer MakeCustomerFromId(int id)
        {
            var sql = $@"SELECT FirstName, LastName, Epost, Number AS PhoneNumber FROM Customer 
LEFT JOIN PhoneNumber ON Customer.ID = PhoneNumber.CustomerID
WHERE Customer.ID = '{id}'";

            var customer = new Customer();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var firstName = reader.GetSqlString(0);
                    customer.FirstName = !firstName.IsNull ? reader.GetString(0) : " ";
                    //om firstname är null så sparas " ". Är detta smart. 

                    var lastName = reader.GetSqlString(1);
                    customer.LastName = !lastName.IsNull ? reader.GetString(1) : " ";

                    var epost = reader.GetSqlString(2);
                    customer.Epost = !epost.IsNull ? reader.GetString(2) : " ";

                    var phoneNumber = reader.GetSqlString(3);
                    if (!phoneNumber.IsNull)
                        customer.PhoneNumber.Add(reader.GetString(3));
                }
            }
            return customer;
        }

        public void DeletePhoneNumber(Customer customer, int id)
        {
            var sql = $@"DELETE FROM PhoneNumber WHERE CustomerID = @id";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("ID", id));
                command.ExecuteNonQuery();
            }





            
        }

        public void UpdateCustomerToDatabase(Customer customer, int id)
        {
            var sql = $@"UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Epost = @Epost WHERE ID = @id ";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                command.Parameters.Add(new SqlParameter("Epost", customer.Epost));
                command.Parameters.Add(new SqlParameter("ID", id));
                command.ExecuteNonQuery();
            }



        }
        
        public int InstertCustomerToDatabase(Customer customer)
        {
            var customerId = 0;
            var sql = @"INSERT INTO Customer(FirstName, LastName, Epost) 
OUTPUT INSERTED.ID
VALUES (@FirstName, @LastName, @Epost)";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("FirstName", customer.FirstName));
                command.Parameters.Add(new SqlParameter("LastName", customer.LastName));
                command.Parameters.Add(new SqlParameter("Epost", customer.Epost));
                //epost är null
                customerId = (int)command.ExecuteScalar();
            }
            return customerId;
        }

        public void InsertCustomerPhoneNumberToDatabase(Customer customer, Int32 customerId)
        {
            var sql = @"INSERT INTO PhoneNumber(Number, CustomerID) VALUES (@Number, @CustomerID)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                for (int i = 0; i < customer.PhoneNumber.Count; i++)
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("Number", customer.PhoneNumber[i]));
                    command.Parameters.Add(new SqlParameter("CustomerID", customerId));

                    command.ExecuteNonQuery();

                }
                
            }

        }

        public void InsertCustomerPhoneNumberToDatabaseFromList(List<PhoneNumber> phoneNumber, Int32 customerId)
        {
            var sql = @"INSERT INTO PhoneNumber(Number, CustomerID) VALUES (@Number, @CustomerID)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                for (int i = 0; i < phoneNumber.Count; i++)
                {
                    
                    command.Parameters.Add(new SqlParameter("Number", phoneNumber[i].Name));
                    command.Parameters.Add(new SqlParameter("CustomerID", customerId));
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                }
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