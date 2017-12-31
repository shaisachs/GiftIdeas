using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace giftideas.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(AuthenticationSchemes = "RapidApi")]
    public class RecipientsController : BaseController<Recipient>
    {
        private readonly GiftIdeasContext _context;

        public RecipientsController(GiftIdeasContext context)
            : base(context, (c) => c.Recipients, "GetRecipient")
        {
            _context = context;
        }
        
        [HttpGet]
        public BaseModelCollection<Recipient> GetAll()
        {
            return base.GetAllBase();
        }

        [HttpGet("{id}", Name = "GetRecipient")]
        public IActionResult GetById(long id)
        {
            return base.GetByIdBase(id);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Recipient item)
        {
            return base.CreateBase(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Recipient newItem)
        {
            return base.UpdateBase(id, newItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            return base.DeleteBase(id);
        }

        protected override Recipient UpdateExistingItem(Recipient existingItem, Recipient newItem) 
        {
            existingItem.Name = newItem.Name;
            return existingItem;
        }        
    }
}