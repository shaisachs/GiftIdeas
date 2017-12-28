using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;

namespace giftideas.Controllers
{
    [Route("api/v1/[controller]")]
    public class HolidaysController : BaseController<Holiday>
    {
        private readonly GiftIdeasContext _context;

        public HolidaysController(GiftIdeasContext context)
            : base(context, (c) => c.Holidays, "GetHoliday")
        {
            _context = context;
        }
        
        [HttpGet]
        public override BaseModelCollection<Holiday> GetAll()
        {
            return base.GetAll();
        }

        [HttpGet("{id}", Name = "GetHoliday")]
        public override IActionResult GetById(long id)
        {
            return base.GetById(id);
        }

        [HttpPost]
        public override IActionResult Create([FromBody] Holiday item)
        {
            return base.Create(item);
        }

        [HttpPut("{id}")]
        public override IActionResult Update(long id, [FromBody] Holiday newItem)
        {
            return base.Update(id, newItem);
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(long id)
        {
            return base.Delete(id);
        }
        
        protected override Holiday UpdateExistingItem(Holiday existingItem, Holiday newItem) 
        {
            existingItem.Name = newItem.Name;
            existingItem.Month = newItem.Month;
            existingItem.Day = newItem.Day;

            return existingItem;
        }
    }
}