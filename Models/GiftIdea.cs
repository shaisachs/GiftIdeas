using System;

namespace giftideas.Models
{
    public class GiftIdea
    {
        public long Id { get; set; }
        public long HolidayId { get; set; }

        public virtual Holiday Holiday { get; set; }

        public long RecipientId { get; set; }

        public virtual Recipient Recipient { get; set; }

        public string GiftDescription { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
