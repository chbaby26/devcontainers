namespace ExpenseAPITest
{
    // 這是用來 ExpenseAPI/Controllers/ExpenseController.cs 的測試
    // 只要測試 post 方法
    // 使用 AAA 模式
    // 每一個方法至少提供 3 個測試案例 1個正向 1個反向 1個邊界
    // 測試名稱都要用 should 作為開頭    
    
    using ExpenseAPI.Models;
    using ExpenseAPI.Controllers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ExpenseControllerTest
    {
        private readonly ExpenseContext _context;

        public ExpenseControllerTest()
        {
            var options = new DbContextOptionsBuilder<ExpenseContext>()
                .UseInMemoryDatabase(databaseName: "ExpenseList")
                .Options;
            _context = new ExpenseContext(options);
        }

        [Fact]
        public async Task should_create_new_expense()
        {
            // Arrange
            var controller = new ExpenseController(_context);
            var expense = new Expense
            {
                Id = 1,
                Amount = 100,
                Description = "test",
                Date = DateTime.Now
            };

            // Act
            var result = await controller.PostExpense(expense);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<Expense>(createdAtActionResult.Value);
            Assert.Equal(expense.Id, model.Id);
            Assert.Equal(expense.Amount, model.Amount);
            Assert.Equal(expense.Description, model.Description);
            Assert.Equal(expense.Date, model.Date);
        }

        [Fact]
        public async Task should_return_bad_request_when_amount_is_zero()
        {
            // Arrange
            var controller = new ExpenseController(_context);
            var expense = new Expense
            {
                Id = 1,
                Amount = 0,
                Description = "test",
                Date = DateTime.Now
            };

            // Act
            var result = await controller.PostExpense(expense);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Amount should be greater than 0", badRequestResult.Value);
        }

        [Fact]
        public async Task should_return_bad_request_when_amount_is_negative()
        {
            // Arrange
            var controller = new ExpenseController(_context);
            var expense = new Expense
            {
                Id = 1,
                Amount = -100,
                Description = "test",
                Date = DateTime.Now
            };

            // Act
            var result = await controller.PostExpense(expense);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Amount should be greater than 0", badRequestResult.Value);
        }
    }
}using ExpenseAPI.Models;
using ExpenseAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class ExpenseControllerTest
{
    private readonly ExpenseContext _context;

    public ExpenseControllerTest()
    {
        var options = new DbContextOptionsBuilder<ExpenseContext>()
            .UseInMemoryDatabase(databaseName: "ExpenseList")
            .Options;
        _context = new ExpenseContext(options);
    }

    [Fact]
    public async Task should_create_new_expense()
    {
        // Arrange
        var controller = new ExpenseController(_context);
        var expense = new Expense
        {
            Id = 1,
            Amount = 100,
            Description = "test",
            Date = DateTime.Now
        };

        // Act
        var result = await controller.PostExpense(expense);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var model = Assert.IsAssignableFrom<Expense>(createdAtActionResult.Value);
        Assert.Equal(expense.Id, model.Id);
        Assert.Equal(expense.Amount, model.Amount);
        Assert.Equal(expense.Description, model.Description);
        Assert.Equal(expense.Date, model.Date);
    }

    [Fact]
    public async Task should_return_bad_request_when_amount_is_zero()
    {
        // Arrange
        var controller = new ExpenseController(_context);
        var expense = new Expense
        {
            Id = 1,
            Amount = 0,
            Description = "test",
            Date = DateTime.Now
        };

        // Act
        var result = await controller.PostExpense(expense);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Amount should be greater than 0", badRequestResult.Value);
    }

    [Fact]
    public async Task should_return_bad_request_when_amount_is_negative()
    {
        // Arrange
        var controller = new ExpenseController(_context);
        var expense = new Expense
        {
            Id = 1,
            Amount = -100,
            Description = "test",
            Date = DateTime.Now
        };

        // Act
        var result = await controller.PostExpense(expense);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Amount should be greater than 0", badRequestResult.Value);
    }

    [Fact]
    public async Task should_return_bad_request_when_description_is_lunch()
    {
        // Arrange
        var controller = new ExpenseController(_context);
        var expense = new Expense
        {
            Id = 1,
            Amount = 100,
            Description = "午餐",
            Date = DateTime.Now
        };

        // Act
        var result = await controller.PostExpense(expense);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("不可以買午餐", badRequestResult.Value);
    }
}