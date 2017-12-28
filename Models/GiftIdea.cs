using System;

namespace giftideas.Models
{
    public class GiftIdea : BaseModel
    {
        public long HolidayId { get; set; }

        public long RecipientId { get; set; }

        public string GiftDescription { get; set; }

    }
}
