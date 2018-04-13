using System;
using System.Collections.Generic;
using System.Text;

namespace CRM_Ida
{
    public class Product
    {
        public string Name { get; set; }
        public int SqlProductId { get; set; }

        public Product MakeProduct()
        {
            Product newProduct = new Product();
            Validate validate = new Validate();

            Console.Clear();
            Console.WriteLine("Skapa en ny produkt");
            newProduct.Name = validate.InputFromUser(stringTypes.ProductName);
            return newProduct;
        }

        public Product ChangeProduct(Product product)
        {
            Console.Write(@"Vad vill du ändra? 1 Namn , etc?");
            var inputChange = Console.ReadLine();
            Validate validate = new Validate();

            switch (inputChange)
            {
                case "1":
                    product.Name = validate.InputFromUser(stringTypes.ProductName);
                    break;
            }
            return product;
        }



    }


}
