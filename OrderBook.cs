// the orderbook is the central point of the market, 
// it keeps track of placed orders and their transactions.

public class OrderBook
{
    private List<LimitOrder> BIDS { get; set; }
    private List<LimitOrder> ASKS { get; set; }
    public int market_time { get; private set; } = 0; //<-- internal time to sort orders FIFO
    public int market_TICKS { get; private set; } = 0;//<-- amount of trades that get executed
    public List<decimal> execution_prices = new List<decimal>(); 

    // create a limit order then try to match it, if it can't be filled, add it to the book.
    public void place_limit_order(Side order_side, double quantity, decimal price)
    {
        var new_order = new LimitOrder(order_side, quantity, price);
        if (new_order.order_side == Side.buy)
        {
            market_TICKS += match_limit_order(new_order, ASKS);
            if (new_order.status != Status.filled)
                BIDS.Add(new_order);
        }
        else if (new_order.order_side == Side.sell)
        {
            market_TICKS += match_limit_order(new_order, BIDS);
            if (new_order.status != Status.filled)
                ASKS.Add(new_order);
        }
        sort_orders();
        market_time++;
    }

    // removes all filled orders from the book
    private void clean_book()
    {
        ASKS.RemoveAll(a => a.status == Status.filled);
        BIDS.RemoveAll(b => b.status == Status.filled);
    }

    // returns the amount of trades made
    private int match_limit_order(LimitOrder order, List<LimitOrder> orders_to_match)
    {
        int ticks = 0;
        foreach (var match in orders_to_match)
        {
            if (order.status == Status.filled)
                break;
            if (match.status == Status.filled)
                continue;
            if ((order.order_side == Side.sell && match.price >= order.price) ||
                (order.order_side == Side.buy && match.price <= order.price))
            {
                var t = trade(order, match);
                execution_prices.Add(t);
                ticks++;
            }
        }
        return ticks;
    }

    private decimal trade(Order order1, LimitOrder order2)
    {
        order1.fill_order(order2.quantity);
        order2.fill_order(order1.quantity);
        var executen_price = order2.price;
        return executen_price; 
    }

    public void print_orderbook()
    {
        clean_book();
        Console.WriteLine($"ASKS({ASKS.Count()}):");
        var copy_asks = new List<LimitOrder>(ASKS);
        copy_asks.Reverse();
        show_order_list(copy_asks,8);
        var price = 0.0m;
        if (execution_prices.Count() > 0)
            price = execution_prices.Last();
        Console.WriteLine($"-----------------------------------------------------> Tick:{market_TICKS} ------------>>> PRICE = {price} ");
        Console.WriteLine($"BIDS({BIDS.Count()}):");
        show_order_list(BIDS,8);
    }

    private void show_order_list(List<LimitOrder> list, int max_window)
    {
        var counter = 0;
        foreach (var order in list)
        {
            Console.WriteLine($"ID:{order.order_id} - QUANTA:{order.quantity}  - FILL:[{order.filled}/{order.quantity}] - Price:{order.price}");
            counter++;
            if(counter > max_window)
                break;
        }
    }

    public List<decimal> give_execution_prices(int max)
    {
        return execution_prices;
    }

    public decimal give_last_price()
    {
        var price = 0.0m;
        if(execution_prices.Count() > 0)
            price = execution_prices.Last();
        return price;
    }

    private void sort_orders()
    {
        // sorted to highest bid at top then fifo
        BIDS = BIDS.OrderByDescending(bid => bid.price)
            .ThenBy(bid => bid.order_id).ToList();
        // sorted to lowest ask at top then fifo
        ASKS = ASKS.OrderBy(ask => ask.price)
            .ThenBy(ask => ask.order_id).ToList();
    }

    public OrderBook()
    {
        BIDS = new List<LimitOrder>();
        ASKS = new List<LimitOrder>();
    }
}
