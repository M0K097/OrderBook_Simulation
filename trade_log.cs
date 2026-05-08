public class TradeLog
{
    private static int log_id_counter = 0;
    public int log_id { get; set; }
    public int taker_id { get; set; }
    public int maker_id { get; set; }
    public decimal quantity {get;set;}
    decimal execution_price { get; set; }

    public void print()
    {
        Console.WriteLine($"Log Nr.:{log_id}\n" +
                $"taker-ID:{taker_id}\n" +
                $"maker-ID:{maker_id}\n" +
                $"qnt:{quantity}\n" +
                $"= execution-price: {execution_price}");
    }

    public TradeLog(Order new_order, LimitOrder resting_order)
    {
        this.log_id = log_id_counter++;
        this.taker_id = new_order.order_id;
        this.maker_id = resting_order.order_id;
        execution_price = resting_order.price;
    }
}
