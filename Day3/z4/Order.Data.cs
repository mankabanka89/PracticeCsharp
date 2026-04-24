namespace StoreSystem
{
    partial class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        public Order(int orderID, string customerName, decimal totalAmount)
        {
            OrderID = orderID;
            CustomerName = customerName;
            TotalAmount = totalAmount;
        }
    }
}