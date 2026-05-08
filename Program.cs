OrderBook test_book = new OrderBook();
Market test_market = new Market(test_book,1,50,1000,100);

const int ITERATIONS = 1000;
for(var i = 0; i < ITERATIONS; i++)
{
    test_market.execute_cycle();
    Console.Clear();
    ConsolePrinter.print_orderbook(test_book);
    ConsolePrinter.print_tradelogs(test_book.trade_log);
    Thread.Sleep(1000);

}









