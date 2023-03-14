using SpaPaws2.Models;
using SpaPaws2.Models.Enteties;
using static SpaPaws2.Models.Enteties.BookingEntity;

namespace SpaPaws2.Services;

internal class MenuService
{

    public async Task CreateNewCustomerAsync()
    {
        var customer = new Customer();

        Console.Write("Registrera kund: \n");
        Console.WriteLine();

        Console.Write("Förnamn: ");
        customer.FirstName = Console.ReadLine() ?? "";

        Console.Write("Efternamn: ");
        customer.LastName = Console.ReadLine() ?? "";

        Console.Write("Telefonnummer: ");
        customer.PhoneNumber = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        customer.Email = Console.ReadLine() ?? "";
        Console.WriteLine();



        Console.Write("Registrera djur: \n");
        Console.WriteLine();

        Console.Write("Djurets Namn: ");
        customer.AnimalName = Console.ReadLine() ?? "";

        Console.Write("Djurets Ras: ");
        customer.AnimalBreed = Console.ReadLine() ?? "";
        Console.WriteLine();


        Console.Write("Bokning: \n");
        Console.WriteLine();

        Console.Write("Skriv in vilken av följande behandling önskas på djuret: \n");
        Console.Write("- Bad \n");
        Console.Write("- Liten trimmning \n");
        Console.Write("- Mellan trimming \n");
        Console.Write("- Stor trimmning \n");
        Console.Write("- Kloklippning \n");
        Console.Write(":");
        customer.BookingType = Console.ReadLine() ?? "";
        Console.WriteLine();

        //save customer to database
        var customerService = new CustomerService();
        await CustomerService.SaveAsync(customer);

    }




    public async Task ListAllBookingsAsync()
    {
        // get all bookings from database
        var customers = await CustomerService.GetAllAsync();

        if (customers.Any())
        {
            foreach (Customer customer in customers)
            {
                Console.WriteLine("Bokning uppgifter:");
                Console.WriteLine($"Bokning Id: {customer.BookingId}");
                Console.WriteLine($"Bokningstid: {customer.Datetime}");
                Console.WriteLine($"Behandling: {customer.BookingType}");
                Console.WriteLine($"Staus: {customer.Status}\n");

                Console.WriteLine($"Kund Id: {customer.Id} Namn: {customer.FirstName} ");
                Console.WriteLine($"Djur Id: {customer.AnimalId} Namn: {customer.AnimalName} \n");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Inga kontakter finns");
        }
    }

    public async Task ListSpecificBookingAsync()
    {
        //get a specific customer  from database

       Console.Write("Ange email på den kund du vill visa: ");
        var email = Console.ReadLine();


        if (!string.IsNullOrEmpty(email))
        {
            var customer = await CustomerService.GetAsync(email);

            if (customer != null)
            {
                Console.WriteLine("Kundens uppgifter:");
                Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Telefon: {customer.PhoneNumber}");
                Console.WriteLine($"Telefon: {customer.Email} \n");


                Console.WriteLine("Djurets uppgifter:");
                Console.WriteLine($"Namn: {customer.AnimalName}  Ras:{customer.AnimalBreed} \n");

                Console.WriteLine("Bokning uppgifter:");
                Console.WriteLine($"Bokningstid: {customer.Datetime}");
                Console.WriteLine($"Behandling: {customer.BookingType}");
                Console.WriteLine($"Status: {customer.Status}");
            }
            else
            {
                Console.WriteLine($"Ingen kund hittades");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"Ingen email angiven.");
            Console.WriteLine();
        }
    }




    public async Task UpdateBookingAsync()
    {
        // update a specific customer from database
        Console.Write("Ange email på den kund du vill uppdatera: ");
        var email = Console.ReadLine();

        if (!string.IsNullOrEmpty(email))
        {
            var customer = await CustomerService.GetAsync(email);
            if (customer != null)
            {
                Console.WriteLine("Fyll i de fälten du vill uppdatera. \n");

                Console.WriteLine("Kundens uppgifter: \n");

                Console.Write("Förnamn: ");
                customer.FirstName = Console.ReadLine() ?? null!;

                Console.Write("Efternamn: ");
                customer.LastName = Console.ReadLine() ?? null!;

                Console.Write("Email: ");
                customer.Email = Console.ReadLine() ?? null!;

                Console.Write("Telefonnummer: ");
                customer.PhoneNumber = Console.ReadLine() ?? null!;

                Console.WriteLine();

                Console.WriteLine("Djurets uppgifter: \n");

                Console.Write("Djurets Namn: ");
                customer.AnimalName = Console.ReadLine() ?? null!;

                Console.Write("Djurets Ras: ");
                customer.AnimalBreed = Console.ReadLine() ?? null!;

                Console.WriteLine();

                Console.WriteLine("Bokning: \n");

                Console.Write("Behandling: ");
                customer.BookingType = Console.ReadLine() ?? null!;

                Console.Write("Status: ");
                Console.Write("(0 - NotStarted, 1 - InProgress, 2 - Completed): ");
                string statusInput = Console.ReadLine();

                if (string.IsNullOrEmpty(statusInput))
                {
                    // Leave the status unchanged
                }
                else
                {
                    int status = Convert.ToInt32(statusInput);
                    if (Enum.IsDefined(typeof(BookingStatus), status))
                    {
                        customer.Status = (BookingStatus)status;
                    }
                    else
                    {
                        Console.WriteLine($"Felaktig inmatning. Bokningsstatusen måste vara ett tal mellan 0 och {Enum.GetValues(typeof(BookingStatus)).Length - 1}.");
                        Console.WriteLine();
                        return;
                    }
                }

                await CustomerService.UpdateAsync(customer);

            }
            else
            {
                Console.WriteLine($"Hittade ingen kund med det angivna mailen.");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"Ingen email angiven.");
            Console.WriteLine();
        }
    }



}
