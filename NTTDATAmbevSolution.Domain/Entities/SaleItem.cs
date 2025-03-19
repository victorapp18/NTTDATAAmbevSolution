namespace NTTDATAmbevSolution.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice => (UnitPrice * Quantity) - Discount;

        public void ApplyDiscount()
        {
            if (Quantity >= 4 && Quantity < 10)
            {
                Discount = (UnitPrice * Quantity) * 0.10m; // 10% de desconto
            }
            else if (Quantity >= 10 && Quantity <= 20)
            {
                Discount = (UnitPrice * Quantity) * 0.20m; // 20% de desconto
            }
            else
            {
                Discount = 0; // Nenhum desconto para menos de 4 itens
            }
        }
    }
}
