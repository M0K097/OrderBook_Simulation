public class Market
{
    public OrderBook book { get; }
    public int limit_orders_per_cycle { get; set; }
    public double optimizm_parameter { get; set; }
    public int max_quantity { get; set; }
    public int max_price { get; set; }
    public int max_cycles { get; set; }
    public int frequency_in_ms = 1000;
    public int risk_parameter {get;set;}
    Random dice = new Random();

    public void start()
    {
        for (int cycle = 0; cycle < max_cycles; cycle++)
        {
            for (int i = 0; i < limit_orders_per_cycle; i++)
            {
                var roll = dice.Next(100);
                var amount = dice.Next(max_quantity);
                var price = dice.Next(max_price);
                if (roll < optimizm_parameter)
                    book.place_limit_order(Side.buy, amount, (decimal)price);
                else
                    book.place_limit_order(Side.sell, amount, (decimal)price);
            }
            Console.Clear();
            Console.WriteLine($"Market_Cycle[{cycle}]");
            console_printer.print_orderbook(book); 
            console_printer.print_trade(book);
            Thread.Sleep(frequency_in_ms);
        }
    }

    public Market(OrderBook book, int limit_orders_per_cycle, int max_cycles, int lambda, int max_quantity, int max_price)
    {
        this.book = book;
        this.limit_orders_per_cycle = limit_orders_per_cycle;
        this.max_cycles = max_cycles;
        this.optimizm_parameter = lambda;
        this.max_price = max_price;
        this.max_quantity = max_quantity;
    }
}
