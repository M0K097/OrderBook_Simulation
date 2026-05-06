// the orderbook is the central point of the market, 
// it keeps track of palced orders and their transactions.

public class OrderBook
{
    // only limit orders go to the book
    private List<LimitOrder> BIDS { get; set; }
    private List<LimitOrder> ASKS { get; set; }

    // relative_order_book_time
    public int market_ticks {get; private set;} = 0;


    public bool place_limit_order(Side order_side, double quantity, decimal price)
    {
        var new_order = new LimitOrder(order_side, quantity, price,market_ticks);
        if (new_order.order_side == Side.buy)
            BIDS.Add(new_order);
        else if (new_order.order_side == Side.sell)
            ASKS.Add(new_order);
        else
            return false;

        market_ticks++;
        return true;
    }

    public void print_orderbook()
    {
        Console.WriteLine($"ASKS({ASKS.Count()}):");

        // turn list upside down just for the benefit of visualization
        var copy_asks = new List<LimitOrder>(ASKS);
        copy_asks.Reverse();
        show_order_list(copy_asks);
        
        Console.WriteLine("------------------------------------------------------->");
        Console.WriteLine($"BIDS({BIDS.Count()}):");
        show_order_list(BIDS);
    }

    public void show_order_list(List<LimitOrder> list)
    {
        foreach (var order in list)
        {
           Console.WriteLine($"ID:{order.order_id} - QUANTA:{order.quantity} - TICK:{order.relative_market_time} - STATUS:{order.status} - Price:{order.price}");
        }

    }

    public void sort_orders()
    {
        // sorted to highest bid at top then fifo
        BIDS.OrderByDescending(bid => bid.price)
            .ThenBy(bid => bid.time);

        // sorted to lowest ask at top then fifo
        ASKS.OrderBy(ask => ask.price)
            .ThenBy(bid => bid.time);
    }

    public OrderBook()
    {
        BIDS = new List<LimitOrder>();
        ASKS = new List<LimitOrder>();
    }
}
