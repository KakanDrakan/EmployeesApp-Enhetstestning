using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Application.Employees.Services;
using EmployeesApp.Domain.Entities;
using Moq;

namespace EmployeeApp.Application.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void GetAll_NoInput_AllEmployees()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(e => e.GetAll()).Returns([new Employee()
            {
                Id = 562,
                Name = "Anders Hejlsberg",
                Email = "Anders.Hejlsberg@outlook.com",
            }, new Employee()
        {
            Id = 62,
            Name = "Kathleen Dollard",
            Email = "k.d@outlook.com",
        }]);

            var employeeService = new EmployeeService(employeeRepository.Object);
            var result = employeeService.GetAll();

            Assert.IsType<Employee[]>(result);
            Assert.Equal(2, result.Length);
            Assert.Equal("Anders Hejlsberg", result[0].Name);
            Assert.Equal("Kathleen Dollard", result[1].Name);
        }
        [Fact]
        public void GetById_ValidId_ReturnsEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();

            employeeRepository.Setup(e => e.GetById(562)).Returns(new Employee()
            {
                Id = 562,
                Name = "Anders Hejlsberg",
                Email = "test@test.com",

            });

            var employeeService = new EmployeeService(employeeRepository.Object);

            var result = employeeService.GetById(562);
            Assert.IsType<Employee>(result);
            Assert.Equal(562, result.Id);
        }
        [Fact]
        public void GetById_InvalidId_ThrowsArgumentException()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository.Setup(e => e.GetById(999)).Returns((Employee?)null);

            var employeeService = new EmployeeService(employeeRepository.Object);
            Assert.Throws<ArgumentException>(() => employeeService.GetById(999));
        }
        [Fact]
        public void Add_ValidEmployee_AddsEmployee()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            var employeeService = new EmployeeService(employeeRepository.Object);
            var employee = new Employee
            {
                Id = 1,
                Name = "John doe",
                Email = "test@test.com"
            };
            employeeService.Add(employee);
            employeeRepository.Verify(e => e.Add(It.Is<Employee>(e =>
                e.Name == "John doe" && e.Email == "test@test.com")));
        }
        [Fact]
        public void CheckIsVIP_ValidEmployee_ReturnsTrueIfVIP()
        {
            var employeeRepository = new Mock<IEmployeeRepository>();
            var employeeService = new EmployeeService(employeeRepository.Object);
            var employee = new Employee
            {
                Id = 1,
                Name = "Anders Hejlsberg",
                Email = "anders@test.com"

            };
            var result = employeeService.CheckIsVIP(employee);
            Assert.True(result);

        }
    }
}
