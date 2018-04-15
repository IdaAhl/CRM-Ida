using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text;

namespace CRM_Ida
{
    class ProductSqlQuery
    {
        private string conString = @"Server = (localdb)\mssqllocaldb; Database = CRM-Ida; Trusted_Connection = True";

        public int InstertProductIntoDatabase(Product product)
        {
            var customerId = 0;
            var sql = @"INSERT INTO Product(Name) 
OUTPUT INSERTED.ID
VALUES (@Name)";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Name", product.Name));
               
                customerId = (int)command.ExecuteScalar();
            }
            return customerId;
        }

        public Product MakeProductFromId(int id)
        {
            var sql = $@"SELECT Name AS ProduktName FROM Product 
WHERE Product.ID = '{id}'";

            var product = new Product();
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader.GetSqlString(0);
                    product.Name = !name.IsNull ? reader.GetString(0) : " ";
                }
            }
            return product;
        }

        public void UpdateProductIntoDatabase(Product product, int id)
        {
            var sql = $@"UPDATE Product SET Name = @Name WHERE Product.ID = @id";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("Name", product.Name));
                command.Parameters.Add(new SqlParameter("ID", id));
                command.ExecuteNonQuery();
            }



        }

        public void InserFavoriteProductIntoDatabase(int productId, int customerId)
        {
            var sql = @"INSERT INTO FavoriteProducts(ProductID, CustomerID) VALUES (@ProductID, @CustomerID)";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(new SqlParameter("ProductID", productId ));
                command.Parameters.Add(new SqlParameter("CustomerID", customerId));

                command.ExecuteNonQuery();
            }

        }

        public void ProductList()
        {
            var sql = @"SELECT Product.ID, Name FROM Product";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var ID = reader.GetInt32(0);

                    var Name = reader.GetSqlString(1);
                    if (!Name.IsNull)
                        Name = reader.GetString(1);
                    System.Console.WriteLine($"ProduktId: {ID} ProduktNamn: {Name} ");
                };
            }
        }

        public void CustomerFavoriteProductList()
        {
            var sql = @"SELECT Customer.ID, FirstName, Product.ID, Name AS ProduktName 
FROM Customer
INNER JOIN FavoriteProducts ON Customer.ID = FavoriteProducts.CustomerID
INNER JOIN Product ON FavoriteProducts.ProductID = Product.ID
ORDER BY Customer.ID";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine(@"Kund, Favoritprodukt");

                while (reader.Read())
                {
                    var customerId = reader.GetInt32(0);

                    var firstName = reader.GetSqlString(1);
                    if (!firstName.IsNull)
                        firstName = reader.GetString(1);

                    var productId = reader.GetInt32(2);

                    var name = reader.GetSqlString(3);
                    if (!name.IsNull)
                        name = reader.GetString(3);

                    System.Console.WriteLine($"{customerId} {firstName}, {productId} {name} ");
                };
            }

        }

        public void DeleteFavoriteProductFrom(int customerId, int productId)
        {
            var sql = $@"DELETE FROM FavoriteProducts WHERE ProductID = {productId} AND CustomerID = {customerId}";
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                //command.Parameters.Add(new SqlParameter("CustomerID", customerId));
                //command.Parameters.Add(new SqlParameter("ProductID", productId));
                command.ExecuteNonQuery();
            }

        }
    }
}
