using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace giftideas.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class GiftIdeasController : BaseController<GiftIdea>
    {
        private readonly GiftIdeasContext _context;

        public GiftIdeasController(GiftIdeasContext context)
            : base(context, (c) => c.GiftIdeas, "GetGiftIdea")
        {
            _context = context;
        }
        
        [HttpGet]
        public BaseModelCollection<GiftIdea> GetAll(long? holidayId = null, long? recipientId = null)
        {
            Func<GiftIdea, bool> additionalFilter = (t) =>
                (holidayId.HasValue ? t.HolidayId == holidayId.Value : true) &&
                (recipientId.HasValue ? t.RecipientId == recipientId.Value : true);

            return base.GetAll(additionalFilter: additionalFilter);
        }

        [HttpGet("{id}", Name = "GetGiftIdea")]
        public override IActionResult GetById(long id)
        {
            return base.GetById(id);
        }

        [HttpPost]
        public override IActionResult Create([FromBody] GiftIdea item)
        {
            return base.Create(item);
        }

        [HttpPut("{id}")]
        public override IActionResult Update(long id, [FromBody] GiftIdea newItem)
        {
            return base.Update(id, newItem);
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(long id)
        {
            return base.Delete(id);
        }

        protected override GiftIdea UpdateExistingItem(GiftIdea existingItem, GiftIdea newItem) 
        {
            existingItem.GiftDescription = newItem.GiftDescription;
            existingItem.HolidayId = newItem.HolidayId;
            existingItem.RecipientId = newItem.RecipientId;

            return existingItem;
        }

    }
}