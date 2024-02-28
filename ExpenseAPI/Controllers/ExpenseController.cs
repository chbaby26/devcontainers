// 這個 API Controller 會使用 Expense.cs 這個 Entity 來對應資料庫中的 Table
// 這個 API Controller 會使用 ExpenseContext.cs 這個 DbContext 來對應資料庫
// API 路徑是 /api/expense

using ExpenseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: api/Expense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenseItems()
        {
            return await _context.ExpenseItems.ToListAsync();
        }

        // GET: api/Expense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            var expense = await _context.ExpenseItems.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        /// <summary>
        /// 新增支出
        /// </summary>
        /// <param name="expense"></param>
        /// <returns>
        /// 回傳新增的支出
        /// </returns>
        /// <response code="201">回傳新增的支出</response>
        /// <response code="400">如果 description 是 午餐，則會回傳 400 Bad Request 並且回覆 【不可以買午餐】</response>
        /// <response code="500">如果發生錯誤，則回傳 500 Internal Server Error 並且回覆錯誤訊息</response>
        /// <remarks>
        /// Sample curl:
        /// curl -X POST -H "Content-Type: application/json" -d "{\"date\":\"2019-01-01\",\"description\":\"買早餐\",\"amount\":50,\"category\":\"食\"}" http://localhost:5000/api/expense
        /// Sample request:
        ///
        ///     POST /api/expense
        ///     {
        ///        "date": "2019-01-01",
        ///        "description": "買早餐",
        ///        "amount": 50,
        ///        "category": "食"
        ///     }
        ///
        /// </remarks>

        
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense expense)
        {   
            if (expense.Description == "午餐")
            {
                return BadRequest("不可以買午餐");
            }
            _context.ExpenseItems.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expense/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var expense = await _context.ExpenseItems.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.ExpenseItems.Remove(expense);
            await _context.SaveChangesAsync();

            return expense;
        }

        private bool ExpenseExists(int id)
        {
            return _context.ExpenseItems.Any(e => e.Id == id);
        }
    }
}
