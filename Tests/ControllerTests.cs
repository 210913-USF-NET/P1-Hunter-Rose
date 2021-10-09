using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebUI.Controllers;
using BL;
using Models;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class ControllerTests
    {
        [Fact]
        public void StoreControllerIndexShouldReturnListOfStores()
        {
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.StoreLocation()).Returns(
                   new List<Stores>()
                   {
                        new Stores() {
                            Id = 1,
                            Name = "HFMPB",
                            Location = "Charlotte"
                        },
                        new Stores()
                        {
                            Id = 2,
                            Name = "HFMPB",
                            Location = "Las Vegas"
                        }
                   }
               );
            var controller = new StoreController(mockBL.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Stores>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
        [Fact]
        public void CustomerControllerIndexShouldReturnListOfStores()
        {
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.ListOfCustomers()).Returns(
                   new List<Customer>()
                   {
                        new Customer() {
                            Id = 1,
                            Name = "Hunter"
                        },
                        new Customer()
                        {
                            Id = 2,
                            Name = "HFMPB"
                        }
                   }
               );
            var controller = new CustomerController(mockBL.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
    }
}
