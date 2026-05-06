
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

    public int order_id {get;}
    public Side order_side {get;}
    public double quantity {get;}
    public decimal price {get;}
    public DateTime time {get;}
    public Status status {get;private set;}
    public double filled {get;private set;}
    public double remaining {get;private set;}

    public bool fill_order(double amount)
    {
        filled += amount;
        remaining = quantity - filled;
        if (remaining > 0)
        {
            status = Status.partially_filled;
            return false;
        }
        status = Status.filled;
        return true;
    }

    public Order(Side side, double quantity, decimal price)
    {
       this.order_id = Order.order_id_counter++;
       this.order_side = side;
       this.quantity = quantity;
       this.price = price;
       this.time = DateTime.Now;
       this.status = Status.open;
       this.filled = 0;
       this.remaining = this.quantity;
    }
}

