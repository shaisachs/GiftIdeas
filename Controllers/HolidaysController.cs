using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace giftideas.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class HolidaysController : BaseController<Holiday>
    {
        private readonly GiftIdeasContext _context;

        public HolidaysController(GiftIdeasContext context)
            : base(context, (c) => c.Holidays, "GetHoliday")
        {
            _context = context;
        }
        
        [HttpGet]
        public BaseModelCollection<Holiday> GetAll()
        {
            return base.GetAllBase();
        }

        [HttpGet("{id}", Name = "GetHoliday")]
        public IActionResult GetById(long id)
        {
            return base.GetByIdBase(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Holiday item)
        {
            return base.CreateBase(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Holiday newItem)
        {
            return base.UpdateBase(id, newItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return base.DeleteBase(id);
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