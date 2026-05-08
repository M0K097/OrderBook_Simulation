public class Trade
{
    private static int trade_id = 0;
    public int id { get; set; }
    public Order new_order { get; set; }
    public Order resting_order { get; set; }
    decimal execution_price { get; set; }

    public void print()
    {
        Console.WriteLine($"Trade ID:{id}\n" +
                $"new_order_id:{new_order.order_id}\n" +
                $"resting_order_id:{resting_order.order_id}\n" +
                $"= execution-price: {execution_price}");
    }

    public Trade(Order new_order, LimitOrder resting_order)
    {
        this.id = trade_id++;
        this.new_order = new_order;
        this.resting_order = resting_order;
        execution_price = resting_order.price;
    }
}
