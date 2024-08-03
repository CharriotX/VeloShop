using Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        //[TestMethod]
        //public void Can_Add_New_Lines()
        //{
        //    // arrange 
        //    Product product1 = new Product { Id = 1, Name = "Product1" };
        //    Product product2 = new Product { Id = 2, Name = "Product2" };

        //    // arrange
        //    Cart cart = new Cart();

        //    // act
        //    //cart.AddItem(product1, 1);
        //    //cart.AddItem(product2, 1);
        //    //cart.AddItem(product1, 5);
        //    //List<CartItem> results = cart.Items.OrderBy(c => c.Product.Id).ToList();

        //    // accert
        //    Assert.AreEqual(results.Count(), 2);
        //    Assert.AreEqual(results[0].Quantity, 6);
        //    Assert.AreEqual(results[1].Quantity, 1);
        //}

        //[TestMethod]
        //public void Can_Remove_Line()
        //{
        //    //arange
        //    Product product1 = new Product { Id = 1, Name = "Product1"};
        //    Product product2 = new Product { Id = 2, Name = "Product2"};
        //    Product product3 = new Product { Id = 3, Name = "Product3" };
        //    Product product4 = new Product { Id = 4, Name = "Product4" };

        //    Cart cart = new Cart();
        //    cart.AddItem(product1, 1);
        //    cart.AddItem(product2, 1);
        //    cart.AddItem(product1, 5);
        //    cart.AddItem(product3, 2);
        //    cart.AddItem(product2, 3);
        //    //cart.AddItem(product4, 1);

        //    //act
        //    cart.RemoveItem(product1);

        //    //accert
        //    Assert.AreEqual(cart.Items.Where(x => x.Product == product1).Count(), 0);
        //    Assert.AreEqual(cart.Items.Count(), 2);
        //}

        //[TestMethod]
        //public void Can_Calc_Total()
        //{
        //    //arange
        //    Product product1 = new Product { Id = 1, Name = "Product1", Price = 100 };
        //    Product product2 = new Product { Id = 2, Name = "Product2", Price = 251 };
        //    Product product3 = new Product { Id = 3, Name = "Product3", Price = 49 };

        //    Cart cart = new Cart();
        //    cart.AddItem(product1, 3);
        //    cart.AddItem(product2, 1);
        //    cart.AddItem(product3, 2);

        //    //act
        //    decimal result = cart.ComputeTotalValue();

        //    //accert
        //    Assert.AreEqual(result, 649);
        //}

        //[TestMethod]
        //public void Can_Clear()
        //{
        //    //arange
        //    Product product1 = new Product { Id = 1, Name = "Product1", Price = 100 };
        //    Product product2 = new Product { Id = 2, Name = "Product2", Price = 251 };
        //    Product product3 = new Product { Id = 3, Name = "Product3", Price = 49 };

        //    Cart cart = new Cart();

        //    //act
        //    cart.AddItem(product1, 3);
        //    cart.AddItem(product2, 1);
        //    cart.AddItem(product3, 2);
        //    cart.Clear();

        //    //accert
        //    Assert.AreEqual(cart.Items.Count(), 0);
        //}
    }
}
