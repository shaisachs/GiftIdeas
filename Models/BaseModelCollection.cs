using System.Collections.Generic;

namespace giftideas.Models
{
    public class BaseModelCollection<T> where T : BaseModel
    {
        public IEnumerable<T> Items { get; set; }
    }
}