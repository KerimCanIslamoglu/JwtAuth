using JwtAuth.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JwtAuth.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")] // We define the routing that our controller going to use
    [ApiController] // We need to specify the type of the controller to let .Net core know
    public class TodoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetItems()
        {
            var items = _context.TodoDatas.ToList();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.TodoDatas.FirstOrDefaultAsync(z => z.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(TodoData data)
        {
            if (ModelState.IsValid)
            {
                await _context.TodoDatas.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItem", new { data.Id }, data);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, TodoData item)
        {
            if (id != item.Id)
                return BadRequest();

            var existItem = await _context.TodoDatas.FirstOrDefaultAsync(z => z.Id == id);

            if (existItem == null)
                return NotFound();

            existItem.Title = item.Title;
            existItem.Details = item.Details;
            existItem.Done = item.Done;

            await _context.SaveChangesAsync();

            // Following up the REST standart on update we need to return NoContent
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var existItem = await _context.TodoDatas.FirstOrDefaultAsync(z => z.Id == id);

            if (existItem == null)
                return NotFound();

            _context.TodoDatas.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }
    }
}
