using SimpleFramework.Utils.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFramework.ConsoleTests
{
    public class XmlTest
    {

        public void Tests()
        {

            var list = new List<Product>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Product()
                {
                    ProductId = i,
                    Name = "Name " +i, Category = new Category()
                    {
                        CategoryId = i,
                        Name = "Category " +i
                    }
                });
            }

            string xml = XmlHelper.SerializeToXml(list);

            var listObject = XmlHelper.DeserializeXml<List<Product>>(xml);

        }

    }


    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
       

    }

    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }





}
