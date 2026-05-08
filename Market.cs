public class Market
{
    public OrderBook book { get; }
    public int limit_orders_per_cycle { get; set; }
    public double optimizm_parameter { get; set; }
    public int max_quantity { get; set; }
    public int max_price { get; set; }
    public int risk_parameter { get; set; }
    Random dice = new Random();

    public void execute_cycle()
    {
        for (int i = 0; i < limit_orders_per_cycle; i++)
        {
            var roll = dice.Next(100);
            var amount = dice.Next(1, max_quantity + 1);
            var price = dice.Next(1, max_price + 1);

            if (roll < optimizm_parameter)
                book.place_limit_order(Side.buy, amount, (decimal)price);
            else
                book.place_limit_order(Side.sell, amount, (decimal)price);
        }
    }

    public Market(OrderBook book, int limit_orders_per_cycle, int lambda, int max_quantity, int max_price)
    {
        this.book = book;
        this.limit_orders_per_cycle = limit_orders_per_cycle;
        this.optimizm_parameter = lambda;
        this.max_price = max_price;
        this.max_quantity = max_quantity;
    }
}
