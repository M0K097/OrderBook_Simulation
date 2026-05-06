
public enum Side
{
    buy,
    sell
}

public enum OrderType
{
    market,
    limit
}

public enum Status
{
    open,
    partially_filled,
    filled,
    cancelled
}

public abstract class Order 
{
    public static int order_id_counter = 0;

    public int order_id {get;}
    public OrderType type {get; set;} 
    public Side order_side {get;}
    public double quantity {get;}
    public DateTime time {get;}
    public Status status {get; private set;}
    public double filled {get; private set;}
    public double remaining {get; private set;}

    public double fill_order(double amount)
    {
        var left_over = amount - remaining;
        if(left_over >= 0)
        {
            filled += remaining;
            status = Status.filled;
            remaining = 0;
        }
        else
        {
            remaining -= amount;
            filled += amount;
            left_over = 0;
            status = Status.partially_filled;
        }
        return left_over;
    }

    public string show_order_info()
    {
        var info = $"SIDE: {order_side}\n"+
            $"ID: {order_id}\n"+
            $"TYPE: {type}\n"+
            $"QUANTITY: {quantity}\n"+
            $"FILLED: {filled}\n"+
            $"REMAINING: {remaining}\n"+
            $"STATUS: {status}";

        return info;
    }

    public void cancel_order()
    {
        this.status = Status.cancelled;
    }

    public Order(Side side, double quantity)
    {
       this.order_id = Order.order_id_counter++;
       this.order_side = side;
       this.quantity = quantity;
       this.time = DateTime.Now;
       this.status = Status.open;
       this.filled = 0;
       this.remaining = this.quantity;
    }
}

// a limit order needs a price it can be executed for.
public class LimitOrder : Order
{
    public decimal price {get; set;} 
    public int relative_market_time {get; set;}
    public LimitOrder( Side side, double quantity,decimal price, int ticker_time) : base(side,quantity)
    {
        this.type = OrderType.limit;
        this.price = price;
        this.relative_market_time = ticker_time;
    }
}

// a market order is executed to the best available price right now.
public class MarketOrder : Order
{
    public MarketOrder(Side side, double quantity,decimal price) : base(side,quantity)
    {
        this.type = OrderType.market;
    }
}

