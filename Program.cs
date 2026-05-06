OrderBook test_book = new OrderBook();
 Random dice = new Random();

 int counter = 0;
while(counter < 100)
{
    var side = dice.Next(2);
    var amount = dice.Next(100);
    var price = dice.Next(1000);

    if (side == 1)
        test_book.place_limit_order(Side.buy, amount,(decimal)price);
    else
        test_book.place_limit_order(Side.sell, amount,(decimal)price);

    Console.Clear();
    test_book.clean_book();
    test_book.print_orderbook();
    Thread.Sleep(2000);
    counter++;
   

}
    
    






