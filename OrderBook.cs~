// the orderbook is the central point of the market, 
// it keeps track of placed orders and their transactions.

public class OrderBook
{
    private List<LimitOrder> BIDS { get; set; }
    private List<LimitOrder> ASKS { get; set; }
    public int market_time { get; private set; } = 0;
    public int market_TICKS { get; private set; } = 0;

    // returns true if placed
    public bool place_limit_order(Side order_side, double quantity, decimal price)
    {
        var new_order = new LimitOrder(order_side, quantity, price, market_time);
        if (new_order.order_side == Side.buy)
        {
            market_TICKS += match_limit_order(new_order, BIDS);
            BIDS.Add(new_order);
        }
        else if (new_order.order_side == Side.sell)
        {
            market_TICKS += match_limit_order(new_order, ASKS);
            ASKS.Add(new_order);
        }
        else
            return false;

        sort_orders();
        market_time++;
        return true;
    }

    private void clean_book()
    {
        ASKS.RemoveAll(a => a.status == Status.filled);
        BIDS.RemoveAll(b => b.status == Status.filled);
    }

    // returns the amount of trades made
    private int match_limit_order(LimitOrder order, List<LimitOrder> orders_to_match)
    {
        int trades = 0;
        foreach (var match in orders_to_match)
        {
            if(order.status == Status.filled)
                break;
            if(match.status == Status.filled)
                continue;
            if ((order.order_side == Side.sell && match.price <= order.price)||
                (order.order_side == Side.buy && match.price >= order.price))
            {
                trade(order, match);
                trades++;
            }
        }
        return trades;
    }

    private void trade(Order order1, LimitOrder order2)
    {
        order1.fill_order(order2.quantity);
        order2.fill_order(order1.quantity);
    }

    public void print_orderbook()
    {
        clean_book();
        Console.WriteLine($"ASKS({ASKS.Count()}):");
        var copy_asks = new List<LimitOrder>(ASKS);
        copy_asks.Reverse();
        show_order_list(copy_asks);
        Console.WriteLine($"Tick:{market_TICKS}> ------------------------------------------------------->");
        Console.WriteLine($"BIDS({BIDS.Count()}):");
        show_order_list(BIDS);
    }

    private void show_order_list(List<LimitOrder> list)
    {
        foreach (var order in list)
        {
            Console.WriteLine($"ID:{order.order_id} - QUANTA:{order.quantity} - TIME:{order.relative_market_time} - STATUS:{order.status}[{order.filled}/{order.quantity}] - Price:{order.price}");
        }
    }

    private void sort_orders()
    {
        // sorted to highest bid at top then fifo
        BIDS = BIDS.OrderByDescending(bid => bid.price)
            .ThenBy(bid => bid.time).ToList();
        // sorted to lowest ask at top then fifo
        ASKS = ASKS.OrderBy(ask => ask.price)
            .ThenBy(ask => ask.time).ToList();
    }

    public OrderBook()
    {
        BIDS = new List<LimitOrder>();
        ASKS = new List<LimitOrder>();
    }
}
