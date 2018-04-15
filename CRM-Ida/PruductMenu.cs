using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Ida
{
    class PruductMenu
    {
        public void ShowProductMenu()
        {
            bool quitMenu = true;
            ProductSqlQuery productSqlQuery = new ProductSqlQuery();
            TestSql customerSqlQuery = new TestSql();

            while (quitMenu)
            {
                Console.Clear();
                Console.Write(@"Produkter!

Vad vill du göra?
1) Skapa en produkt
2) Ändra en produkt
3) Ta bort en produkt 
4) Visa alla produkter i en lista
5) Lägga till en favoritkoppling mellan kund och produkt
6) Visa kunder och deras favoritprodukter
7) Ta bort en favoritkoppling mellan kund och produkt
++) Gå tillbaka till huvudmenyn
");
                var choise = Console.ReadLine();

                switch (choise)
                {
                    case "1":
                        Product product = new Product();
                        var newProduct = product.MakeProduct();
                        productSqlQuery.InstertProductIntoDatabase(newProduct);
                        Console.ReadLine();
                        break;

                    case "2":
                        productSqlQuery.ProductList();
                        Console.Write("Vilken produkt vill du ändra?");
                        var input = Int32.Parse(Console.ReadLine());
                        var newProductFromId = productSqlQuery.MakeProductFromId(input);
                        var changedProduct = newProductFromId.ChangeProduct(newProductFromId);
                        productSqlQuery.UpdateProductIntoDatabase(changedProduct, input);
                        break;

                    case "3":
                        break;

                    case "4":
                        productSqlQuery.ProductList();
                        Console.ReadLine();
                        break;

                    case "5":
                        productSqlQuery.ProductList();
                        customerSqlQuery.ReadName();
                        Console.Write("Vilkem kund ska få en favorit?");
                        var inputCustomer = Int32.Parse(Console.ReadLine());
                        Console.Write("Vilkem produkt är en favorit?");
                        var inputProduct = Int32.Parse(Console.ReadLine());

                        productSqlQuery.InserFavoriteProductIntoDatabase(inputProduct, inputCustomer);
                        break;
                    case "6":
                        productSqlQuery.CustomerFavoriteProductList();
                        Console.ReadLine();
                        break;

                    case "7":
                        productSqlQuery.CustomerFavoriteProductList();
                        Console.Write("Från vilken kund ska vi ta bort en favorit?");
                        var changeCustomer = Int32.Parse(Console.ReadLine());
                        Console.Write("Vilken produkt ska vi ta bort?");
                        var changeProduct = Int32.Parse(Console.ReadLine());
                        productSqlQuery.DeleteFavoriteProductFrom(changeCustomer, changeProduct);
                        Console.ReadLine();



                        break;

                    default:
                        quitMenu = false;
                        break;
                }
            }
        }
    }
}
