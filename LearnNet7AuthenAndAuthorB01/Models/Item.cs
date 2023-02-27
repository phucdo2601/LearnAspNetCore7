namespace LearnNet7AuthenAndAuthorB01.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string imgUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
