using Employee.Web.UI.Controllers;
using Employee.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Employee.Web.UI.Test.XUnit
{
    public class EmployeeControllerTests
    {
        [Fact]
        public async Task Index_should_return_default_view()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var result = await employeeController.Index();

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task Index_should_return_11_employees()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Index();

                //Assert
                var model = (List<Models.Employee>)((ViewResult)actionResult).Model;
                Assert.Equal(11, model.Count);
            }
        }

        [Fact]
        public async Task Details_should_return_details_of_employee()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Details(1009);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(actionResult);

                var model = (Models.Employee)((ViewResult)viewResult).Model;
                Assert.Equal(1009, model.Id);
            }
        }

        [Fact]
        public async Task Details_should_return_employee_not_found()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Details(1349);

                //Assert
                var notFoundObjectResult = Assert.IsType<NotFoundResult>(actionResult);
            }
        }

        [Fact]
        public async Task CreateGet_should_return_view_page()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = employeeController.Create();

                //Assert
                var viewResult = Assert.IsType<ViewResult>(actionResult);
            }
        }

        [Fact]
        public async Task CreatePost_should_return_redirect_to_index()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                var newEmployee = new Models.Employee
                {
                    Id = 5000,
                    Fullname = $"fullname5000",
                    Email = $"email5000@example.com",
                    Department = $"department5000",
                    Phone = $"987654795000",
                    Address = $"Address Postal 5000"
                };

                //Act
                var actionResult = await employeeController.Create(newEmployee);

                //Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
                Assert.Null(redirectToActionResult.ControllerName);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
        }

        [Fact]
        public async Task Edit_should_return_details_of_employee()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Edit(1009);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(actionResult);
                var model = (Models.Employee)((ViewResult)viewResult).Model;
                Assert.Equal(1009, model.Id);
            }
        }

        [Fact]
        public async Task Edit_should_return_employee_not_found()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Edit(1349);

                //Assert
                var notFoundObjectResult = Assert.IsType<NotFoundResult>(actionResult);
            }
        }

        [Fact]
        public async Task EditPost_should_return_not_found()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                var newEmployee = new Models.Employee
                {
                    Id = 1003,
                    Fullname = $"fullname5000",
                    Email = $"email5000@example.com",
                    Department = $"department5000",
                    Phone = $"987654795000",
                    Address = $"Address Postal 5000"
                };

                //Act
                var actionResult = await employeeController.Edit(1349, newEmployee);

                //Assert
                var notFoundObjectResult = Assert.IsType<NotFoundResult>(actionResult);
            }
        }

        [Fact]
        public async Task EditPost_should_return_redirect_to_index()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                var selectedEmployee = employeeContext.Employee.Where(x => x.Id == 1003).FirstOrDefault();
                selectedEmployee.Fullname = $"fullname5000";
                selectedEmployee.Email = $"email5000@example.com";
                selectedEmployee.Department = $"department5000";
                selectedEmployee.Phone = $"987654795000";
                selectedEmployee.Address = $"Address Postal 5000";

                //Act
                var actionResult = await employeeController.Edit(1003, selectedEmployee);

                //Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
                Assert.Null(redirectToActionResult.ControllerName);
                Assert.Equal("Index", redirectToActionResult.ActionName);
            }
        }

        [Fact]
        public async Task Delete_with_null_parameter_should_return_employee_not_found()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Delete(null);

                //Assert
                var notFoundObjectResult = Assert.IsType<NotFoundResult>(actionResult);
            }
        }

        [Fact]
        public async Task Delete_should_return_employee_not_found()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Delete(1500);

                //Assert
                var notFoundObjectResult = Assert.IsType<NotFoundResult>(actionResult);
            }
        }

        [Fact]
        public async Task Delete_should_return_employee_view()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.Delete(1003);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(actionResult);
                var model = (Models.Employee)((ViewResult)viewResult).Model;
                Assert.Equal(1003, model.Id);
            }
        }

        [Fact]
        public async Task DeleteConfirmed_should_return_index_view()
        {
            //Arrange
            var employeeContext = await GetDatabaseContext();
            using (var employeeController = new EmployeesController(employeeContext))
            {
                //Act
                var actionResult = await employeeController.DeleteConfirmed(1003);

                //Assert
                var result = Assert.IsType<RedirectToActionResult>(actionResult);
                Assert.Equal("Index", result.ActionName);
            }
        }

        private async Task<WebAppContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<WebAppContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new WebAppContext(options);
            databaseContext.Database.EnsureCreated();

            if (await databaseContext.Employee.CountAsync() <= 0)
            {
                for (int i = 1000; i <= 1010; i++)
                {
                    databaseContext.Employee.Add(new Models.Employee
                    {
                        Id = i,
                        Fullname = $"fullname{i}",
                        Email = $"email{i}@example.com",
                        Department = $"department{i}",
                        Phone = $"98765479{i}",
                        Address = $"Address Postal {i}"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}
