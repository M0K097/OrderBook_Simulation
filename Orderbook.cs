// the orderbook is the central point of the market, 
// it keeps track of palced orders and their transactions.

public class OrderBook
{
    // only limit orders go to the book
    private List<LimitOrder> BIDS { get; set; }
    private List<LimitOrder> ASKS { get; set; }


    public bool place_limit_order(Side order_side, double quantity, decimal price)
    {
        var new_order = new LimitOrder(order_side, quantity, price);
        if (new_order.order_side == Side.buy)
            BIDS.Add(new_order);
        else if (new_order.order_side == Side.sell)
            ASKS.Add(new_order);
        else
            return false;

        return true;
    }

    public void print_orderbook()
    {
        var info = String.Empty;
        foreach (var ask in ASKS)
        {
            Console.WriteLine(ask.show_order_info());
        }
        foreach (var bid in BIDS)
        {
            Console.WriteLine(bid.show_order_info());
        }
    }
    public OrderBook()
    {
        BIDS = new List<LimitOrder>();
        ASKS = new List<LimitOrder>();
    }
}
