namespace Common.MassTransit.Models.Products
{
    public interface IProductMessage
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime DueDate { get; set; }
        public bool Enabled { get; set; }
        public List<string> Aliases { get; set; }
    }
}
