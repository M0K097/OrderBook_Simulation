public static class ConsolePrinter
{
    public static void print_orderbook(OrderBook book)
    {
        var ASKS = book.ASKS;
        var BIDS = book.BIDS;
        Console.WriteLine($"ASKS({ASKS.Count()}):");
        var copy_asks = new List<LimitOrder>(ASKS);
        copy_asks.Reverse();
        show_order_list(copy_asks, 8);
        var last_trade_price = 0m;
        if(book.trade_log.Count > 0)
        {
            last_trade_price = book.trade_log.Last().execution_price;
        }
        Console.WriteLine($"-----------------------------------------------------> PRICE:{last_trade_price}");
        Console.WriteLine($"BIDS({BIDS.Count()}):");
        show_order_list(BIDS, 8);
    }

    public static void print_tradelogs(List<Tradelog> logs)
    {
        Console.WriteLine($"======================= TRADE_LOGS ========================");
        const int ROWS = 5;
        int l = logs.Count - 1;
        for (var i = l; i > l - ROWS; i--)
        {
            if (i >= 0)
                logs[i].print();
        }

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
