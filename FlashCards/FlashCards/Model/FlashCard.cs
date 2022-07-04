namespace FlashCards.Model
{
    public class FlashCard
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }
    }
}
