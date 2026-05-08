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
        sort_orders();
        var new_order = new LimitOrder(order_side, quantity, price);
        Console.WriteLine($"placing ORDER: ID:{new_order.order_id} SIDE:{new_order.order_side} QUANTITY:{new_order.quantity} PRICE:{new_order.price}");
        if (new_order.order_side == Side.buy)
            match_limit_order_buy(new_order);
        else
            match_limit_order_sell(new_order);
    }

    public void match_limit_order_sell(LimitOrder order)
    {
        while (order.status != Status.filled && BIDS.Count() > 0)
        {
            var best_bid = BIDS[0];

            if (order.price < best_bid.price)
                break;
            trade(order, best_bid);
            if (best_bid.status == Status.filled)
                BIDS.Remove(best_bid);
        }
        if (order.status != Status.filled)
            ASKS.Add(order);
    }
    public void match_limit_order_buy(LimitOrder order)
    {
        while (order.status != Status.filled && ASKS.Count() > 0)
        {
            var best_sell = ASKS[0];

            if (order.price < best_sell.price)
                break;
            trade(order, best_sell);
            if (best_sell.status == Status.filled)
                ASKS.Remove(best_sell);
        }
        if (order.status != Status.filled)
            BIDS.Add(order);
    }

    private void trade(Order o1, LimitOrder o2)
    {
        var qty = Math.Min(o1.remaining, o2.remaining);
        o1.fill(qty);
        o2.fill(qty);
        var trade = new Trade(o1, o2);
        trade.print();
        trade_log.Add(trade);
    }

    private void sort_orders()
    {
        BIDS = BIDS.OrderByDescending(bid => bid.price)
            .ThenByDescending(bid => bid.order_id).ToList();
        ASKS = ASKS.OrderBy(ask => ask.price)
            .ThenByDescending(ask => ask.order_id).ToList();
    }

}
