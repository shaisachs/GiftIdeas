using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;

namespace giftideas.Controllers
{
    [Route("api/v1/[controller]")]
    public class RecipientsController : BaseController<Recipient>
    {
        private readonly GiftIdeasContext _context;

        public RecipientsController(GiftIdeasContext context)
            : base(context, (c) => c.Recipients, "GetRecipient", (h1, h2) => h2)
        {
            _context = context;
        }
        
        [HttpGet]
        public override IEnumerable<Recipient> GetAll()
        {
            return base.GetAll();
        }

        [HttpGet("{id}", Name = "GetRecipient")]
        public override IActionResult GetById(long id)
        {
            return base.GetById(id);
        }

        [HttpPost]
        public override IActionResult Create([FromBody] Recipient item)
        {
            return base.Create(item);
        }

        [HttpPut("{id}")]
        public override IActionResult Update(long id, [FromBody] Recipient newItem)
        {
            return base.Update(id, newItem);
        }

        [HttpDelete("{id}")]
        public override IActionResult Delete(long id)
        {
            return base.Delete(id);
        }        
    }
}