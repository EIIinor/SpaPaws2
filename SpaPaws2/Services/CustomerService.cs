using SpaPaws2.Contexts;
using SpaPaws2.Models;
using SpaPaws2.Models.Enteties;

namespace SpaPaws2.Services;

internal class CustomerService
{
    private static DataContext _context = new DataContext();


    //public static async Task SaveAsync(Customer customer)
    //{

    //}



    public static async Task SaveAsync(Guid customerGuid, AnimalEntity animal, BookingEntity booking)
    {

        {
            // Retrieve the customer entity by its Guid
            CustomerEntity customer = _context.Customers.FirstOrDefault(c => c.Id == customerGuid);

            if (customer == null)
            {
                // Throw an exception if the customer was not found
                throw new ArgumentException("Customer not found", nameof(customerGuid));
            }

            // Set the customer ID on the animal entity
            animal.CustomerId = customer.Id;

            // Add the animal and booking entities to the context and save changes
            _context.Animals.Add(animal);
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }
    }


}
