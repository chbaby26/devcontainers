// 使用 ExpenseContext 來產生預設資料

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ExpenseAPI.Models
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExpenseContext(
                serviceProvider.GetRequiredService<DbContextOptions<ExpenseContext>>()))
            {
                // Look for any expenses.
                if (context.ExpenseItems.Any())
                {
                    return;   // Data was already seeded
                }

                //最少產生4筆資料
                //每一個的 Category要不一樣, 從食衣住行 去挑選
                context.ExpenseItems.AddRange(
                    new Expense
                    {
                        Date = DateTime.Parse("2019-01-01"),
                        Description = "買早餐",
                        Amount = 50,
                        Category = "食"
                    },
                    new Expense
                    {
                        Date = DateTime.Parse("2019-01-02"),
                        Description = "買衣服",
                        Amount = 500,
                        Category = "衣"
                    },
                    new Expense
                    {
                        Date = DateTime.Parse("2019-01-03"),
                        Description = "買房子",
                        Amount = 5000000,
                        Category = "住"
                    },
                    new Expense
                    {
                        Date = DateTime.Parse("2019-01-04"),
                        Description = "買車",
                        Amount = 500000,
                        Category = "行"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}