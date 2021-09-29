using System;
using Xunit;
using Models;
using System.Collections.Generic;

namespace Tests
{
    public class UnitTest1
    {

        [Theory]
        [InlineData("john")]
        [InlineData("smith")]
        [InlineData("Christopher")]
        public void CustomerNameTest1(string name)
        {

        Customer test = new Models.Customer();
        test.Name = name;
        Assert.Equal(name, test.Name);
        }

        [Fact]
        public void ProductNameTest2()
        {
        Product test2 = new Models.Product();
        Assert.NotNull(test2);
        string test2Name = "cheese";
        test2.Name = test2Name;
        Assert.Equal(test2Name, test2.Name);
        }
        [Theory]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(300)]
        public void LineItemQuantityTest(int quantity)
        {
        LineItem test = new Models.LineItem();
        Assert.NotNull(test);
        test.Quantity = quantity;
        Assert.Equal(quantity, test.Quantity);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void LineItemQuantityId(int id)
        {
            LineItem test = new Models.LineItem();
            Assert.NotNull(test);
            test.Id = id;
            Assert.Equal(id, test.Id);
        }
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void InventoryId(int id)
        {
            Inventory test = new Inventory();
            Assert.NotNull(test);
            test.Id = id;
            Assert.Equal(id, test.Id);
        }
    }
}
