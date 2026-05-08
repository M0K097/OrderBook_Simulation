public enum Side
{
    buy,
    sell
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

    public int order_id { get; }
    public Side order_side { get; }
    public double quantity { get; }
    public DateTime time { get; }
    public Status status { get; private set; }
    public double filled { get; private set; }
    public double remaining { get; private set; }

    public void cancel_order() => status = Status.cancelled;

    public void fill(double qty)
    {
        remaining -= qty;
        filled += qty;

        if (remaining == 0)
            status = Status.filled;
        else if (filled > 0)
            status = Status.partially_filled;
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

public class LimitOrder : Order
{
    public decimal price { get; private set; }
    public LimitOrder(Side side, double quantity, decimal price) : base(side, quantity)
    {
        this.price = price;
    }
}

public class MarketOrder : Order
{
    public MarketOrder(Side side, double quantity) : base(side, quantity){}
}

