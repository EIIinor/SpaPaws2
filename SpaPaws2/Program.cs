using SpaPaws2.Services;

var menu = new MenuService();



while (true)
{
    Console.Clear();
    Console.WriteLine("1. Skapa en ny kund & bokning");
    Console.WriteLine("2. Visa alla bokningar");
    Console.WriteLine("3. Visa en specefik bokning");
    Console.WriteLine("4. Updatera en bokning");


    Console.WriteLine("Välj ett av följande alternativ: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.CreateNewCustomerAsync();
            break;

        case "2":
            Console.Clear();
            await menu.ListAllBookingsAsync();
            break;

        case "3":
            Console.Clear();
            await menu.ListSpecificBookingAsync();
            break;

        case "4":
            Console.Clear();
            await menu.UpdateBookingAsync();
            break;



    }
    Console.WriteLine("\nTryck på valfri kanpp för att komma vidare..");

    Console.ReadKey();
}