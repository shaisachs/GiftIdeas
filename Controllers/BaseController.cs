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

        public BaseController(GiftIdeasContext context, 
            Func<GiftIdeasContext, DbSet<T>> dbsetGetter,
            string getSingularRouteName)
        {
            _context = context;
            _dbsetGetter = dbsetGetter;
            _getSingularRouteName = getSingularRouteName;
        }

        public virtual BaseModelCollection<T> GetAll(Func<T, bool> additionalFilter = null)
        {
            Func<T, bool> predicate =
                (t) => IsOwnedByCurrentUser(t) &&
                    (additionalFilter == null ? true : additionalFilter(t));

            var items = _dbsetGetter(_context).Where(predicate).ToList();
            var answer = new BaseModelCollection<T>() { Items = items };

            return answer;
        }

        public virtual IActionResult GetById(long id)
        {
            var item = GetSingleItem(id);

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

            item.Created = DateTime.Now;
            item.Creator = CurrentUserName();

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

            var existingItem = GetSingleItem(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem = UpdateExistingItem(existingItem, newItem);

            _dbsetGetter(_context).Update(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        public virtual IActionResult Delete(long id)
        {
            var existingItem = GetSingleItem(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _dbsetGetter(_context).Remove(existingItem);
            _context.SaveChanges();
            return new NoContentResult();
        }

        protected abstract T UpdateExistingItem(T existingItem, T newItem);

        protected T GetSingleItem(long id)
        {
            return _dbsetGetter(_context).FirstOrDefault(t => t.Id == id && IsOwnedByCurrentUser(t));
        }

        protected bool IsOwnedByCurrentUser(T item)
        {
            return !string.IsNullOrEmpty(item.Creator) &&
                item.Creator.Equals(CurrentUserName());
        }

        protected string CurrentUserName()
        {
            return this.User.Identity.Name;
        }
    }
}