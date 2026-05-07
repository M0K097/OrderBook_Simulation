public class Market
{
    public OrderBook book {get;}
    public int market_makers {get;set;} = 0;
    public int cycle_counter {get; private set;} = 0;
    private bool open = false;
    public int frequency_in_ms = 1000;

    public void start()
    {
        open = true;
        while(open)
        {
            Console.Clear();
            book.place_limit_order(Side.buy,100,(decimal)10);
            book.print_orderbook();
            Thread.Sleep(frequency_in_ms);
        }
    }
    public void stop() => open = false;

    Random dice = new Random();


    public Market(OrderBook book)
    {
        this.book = book;
    }
}
