using EmployeesApp.Application.Employees.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesApp.Web.Views.Employees;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Web.Tests
{
    public class ValidationTests
    {
        [Fact]
        public void ValidateCreate()
        {
            var service = new Mock<IEmployeeService>();
            // service.Setup(e => e.Add())
            var model = new CreateVM
            {
                Name = "Adam B",
                Email = "adam.b@gmail",
                BotCheck = 4
            };
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            Assert.True(isValid);

        }
        [Fact]
        public void ValidateCreate_InvalidModel()
        {
            var service = new Mock<IEmployeeService>();
            // service.Setup(e => e.Add())
            var model = new CreateVM
            {
                Name = "Adam B",
                Email = "adam.b@gmail",
                BotCheck = 3
            };
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            //Assert.True(isValid, $"Antal fel {results.Count}");
            Assert.False(isValid);

        }
    }
}
