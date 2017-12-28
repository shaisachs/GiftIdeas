namespace giftideas.Models
{
    public class Holiday : BaseModel
    {
        public string Name { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }
}
