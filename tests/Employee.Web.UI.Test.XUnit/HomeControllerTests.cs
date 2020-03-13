using Employee.Web.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Employee.Web.UI.Test.XUnit
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_should_return_default_view()
        {
            //Arrange
            var homeController = new HomeController();
            
            //Act
            var result = homeController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void About_should_return_about_view()
        {
            //Arrange
            var homeController = new HomeController();

            //Act
           
            var result = homeController.About();
            var viewResult = result as ViewResult;
            viewResult.ViewData["Message"] = "sample view data";

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.Same("sample view data", viewResult.ViewData["Message"]);
        }

        [Fact]
        public void Contact_should_return_contact_view()
        {
            //Arrange
            var homeController = new HomeController();

            //Act

            var result = homeController.Contact();
            var viewResult = result as ViewResult;
            viewResult.ViewData["Message"] = "sample view data";

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.Same("sample view data", viewResult.ViewData["Message"]);
        }
    }
}
