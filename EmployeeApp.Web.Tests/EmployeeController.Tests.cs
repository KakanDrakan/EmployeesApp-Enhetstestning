using Moq;
using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using EmployeesApp.Web.Views;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Views.Employees;

namespace EmployeeApp.Web.Tests
{
    public class EmployeesControllerTests
    {
        [Fact]
        public void Index_NoParams_ReturnsViewResult()
        {
            var employeeService = new Mock<IEmployeeService>();
            employeeService.Setup(e => e.GetAll()).Returns(new[]
            {
                new Employee
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = ""
                },
                new Employee
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = ""
                }
            });

            var controller = new EmployeesController(employeeService.Object);
            var result = controller.Index();
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ValidModel_RedirectsToIndex()
        {
            var employeeService = new Mock<IEmployeeService>();
            var controller = new EmployeesController(employeeService.Object);
            var viewModel = new EmployeesApp.Web.Views.Employees.CreateVM
            {
                Name = "John Doe",
                Email = "email@emial.com",
                BotCheck = 4
            };
            var result = controller.Create(viewModel);
            Assert.NotNull(result);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectResult.ActionName);
        }
        [Fact]
        public void Details_ValidId_ReturnsViewResult()
        {
            var employeeService = new Mock<IEmployeeService>();

            employeeService.Setup(e => e.GetById(1)).Returns (new Employee { Name = "Adam B", Email="adam.b@gmail", Id = 1 });
            var controller = new EmployeesController(employeeService.Object);

            var result = controller.Details(1);
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsType<DetailsVM>(viewResult.Model);
        }
        
    }
}
