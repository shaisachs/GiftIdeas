using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using giftideas.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace giftideas.Controllers
{
    public abstract class BaseController<T> : Controller where T : BaseModel
    {
        private readonly GiftIdeasContext _context;
        private readonly Func<GiftIdeasContext, DbSet<T>> _dbsetGetter;
        private readonly string _getSingularRouteName;
        private readonly Func<T, T, T> _existingItemUpdater;

        public BaseController(GiftIdeasContext context, 
            Func<GiftIdeasContext, DbSet<T>> dbsetGetter,
            string getSingularRouteName,
            Func<T, T, T> existingItemUpdater)
        {
            _context = context;
            _dbsetGetter = dbsetGetter;
            _getSingularRouteName = getSingularRouteName;
            _existingItemUpdater = existingItemUpdater;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbsetGetter(_context).ToList();
        }

        public virtual IActionResult GetById(long id)
        {
            var item = _dbsetGetter(_context).FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        public virtual IActionResult Create(T item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _dbsetGetter(_context).Add(item);
            _context.SaveChanges();

            return CreatedAtRoute(_getSingularRouteName, new { id = item.Id }, item);
        }

        public virtual IActionResult Update(long id, T newItem)
        {
            if (newItem == null || newItem.Id != id)
            {
                return BadRequest();
            }

            var existingItem = _dbsetGetter(_context).FirstOrDefault(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem = _existingItemUpdater(existingItem, newItem);

            _dbsetGetter(_context).Update(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        public virtual IActionResult Delete(long id)
        {
            var existingItem = _dbsetGetter(_context).FirstOrDefault(t => t.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _dbsetGetter(_context).Remove(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }        
    }
}