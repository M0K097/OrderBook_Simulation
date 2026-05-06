
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
    public int order_id {get;}
    public Side order_side {get;}
    public double quantity {get;}
    public decimal price {get;}
    public DateTime time {get;}
    public Status status {get;set;}
    public double filled {get;set;}
    public double remaining {get;set;}

    public Order(int id, Side side, double quantity, decimal price)
    {
       this.order_id = id;
       this.order_side = side;
       this.quantity = quantity;
       this.price = price;
       this.time = DateTime.Now;
       this.status = Status.open;
       this.filled = 0;
       this.remaining = this.quantity;
    }
}

