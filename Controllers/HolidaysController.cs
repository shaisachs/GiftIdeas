using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;

namespace giftideas.Controllers
{
    [Route("api/v1/[controller]")]
    public class HolidaysController : Controller
    {
        private readonly GiftIdeasContext _context;

        public HolidaysController(GiftIdeasContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<Holiday> GetAll()
        {
            return _context.Holidays.ToList();
        }

        [HttpGet("{id}", Name = "GetSingular")]
        public IActionResult GetById(long id)
        {
            var item = _context.Holidays.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Holiday item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.Holidays.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetSingular", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Holiday newItem)
        {
            if (newItem == null || newItem.Id != id)
            {
                return BadRequest();
            }

            var existingItem = _context.Holidays.FirstOrDefault(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = newItem.Name;
            existingItem.Month = newItem.Month;
            existingItem.Day = newItem.Day;

            _context.Holidays.Update(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var existingItem = _context.Holidays.FirstOrDefault(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _context.Holidays.Remove(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }        
    }
}