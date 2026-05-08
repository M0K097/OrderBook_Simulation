public static class console_printer
{
    public static void print_orderbook(OrderBook book)
    {
        var ASKS = book.ASKS;
        var BIDS = book.BIDS;
        Console.WriteLine($"ASKS({ASKS.Count()}):");
        var copy_asks = new List<LimitOrder>(ASKS);
        copy_asks.Reverse();
        show_order_list(copy_asks, 8);
        Console.WriteLine($"-----------------------------------------------------> ");
        Console.WriteLine($"BIDS({BIDS.Count()}):");
        show_order_list(BIDS, 8);
    }

    private static void show_order_list(List<LimitOrder> list, int max_window)
    {
        var counter = 0;
        foreach (var order in list)
        {
            Console.WriteLine($"ID:{order.order_id} - QUANTA:{order.quantity}  - FILLED:[{order.filled}/{order.quantity}] - Price:{order.price}");
            counter++;
            if (counter > max_window)
                break;
        }
    }
}
