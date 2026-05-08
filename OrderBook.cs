// the orderbook is the central point of the market, 
// it keeps track of placed orders and their transactions.

public class OrderBook
{
    public int ticks { get; } = 0;
    public List<LimitOrder> BIDS { get; private set; } = new List<LimitOrder>();
    public List<LimitOrder> ASKS { get; private set; } = new List<LimitOrder>();
    public List<Trade> trade_log = new List<Trade>();

    public void place_limit_order(Side order_side, double quantity, decimal price)
    {
        var new_order = new LimitOrder(order_side, quantity, price);
        Console.WriteLine($"placing ORDER: ID:{new_order.order_id} SIDE:{new_order.order_side} QUANTITY:{new_order.quantity} PRICE:{new_order.price}");
        if (new_order.order_side == Side.buy)
            match_limit_order_buy(new_order);
        else
            match_limit_order_sell(new_order);
        sort_orders();
    }

    public void match_limit_order_buy(LimitOrder order)
    {
        sort_orders();
        if (ASKS.Count() > 0)
        {
            var best_sell = ASKS.First();
            while (order.status != Status.filled && order.price >= best_sell.price)
            {
                trade(order, best_sell);
                if (best_sell.status == Status.filled)
                {
                    ASKS.Remove(best_sell);
                    if (ASKS.Count() > 0)
                        best_sell = ASKS.First();
                    else
                        break;
                }
            }
        }
        if (order.status != Status.filled)
            BIDS.Add(order);
    }


    public void match_limit_order_sell(LimitOrder order)
    {
        sort_orders();
        if (BIDS.Count() > 0)
        {
            var best_bid = BIDS.First();
            while (order.status != Status.filled && order.price <= best_bid.price)
            {
                trade(order, best_bid);
                if (best_bid.status == Status.filled)
                {
                    BIDS.Remove(best_bid);
                    if (BIDS.Count() > 0)
                        best_bid = BIDS.First();
                    else
                        break;
                }
            }
        }
        if (order.status != Status.filled)
            ASKS.Add(order);
    }

    private void trade(Order o1, LimitOrder o2)
    {
        var tmp = o1.quantity;
        o1.fill(o2.quantity);
        o2.fill(tmp);
        var trade = new Trade(o1, o2);
        trade.print();
        trade_log.Add(trade);
    }

    private void sort_orders()
    {
        BIDS = BIDS.OrderByDescending(bid => bid.price)
            .ThenBy(bid => bid.order_id).ToList();
        ASKS = ASKS.OrderBy(ask => ask.price)
            .ThenBy(ask => ask.order_id).ToList();
    }

}
