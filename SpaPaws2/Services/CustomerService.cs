using Microsoft.EntityFrameworkCore;
using SpaPaws2.Contexts;
using SpaPaws2.Models;
using SpaPaws2.Models.Enteties;
using static SpaPaws2.Models.Enteties.BookingEntity;

namespace SpaPaws2.Services;

internal class CustomerService
{
    private static DataContext _context = new DataContext();



    public static async Task SaveAsync(Customer customer)
    {
        var customerEntity = new CustomerEntity
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
        };
        var animalEntity = await _context.Animals.FirstOrDefaultAsync(x => x.AnimalName == customer.AnimalName && x.AnimalBreed == customer.AnimalBreed);
        if (animalEntity != null)
            customerEntity.AnimalId = animalEntity.Id;
        else
            customerEntity.Animal = new AnimalEntity
            {
                AnimalName = customer.AnimalName,
                AnimalBreed = customer.AnimalBreed,
            };

        // Check if a booking entity with the specified Datetime, BookingType, and Status exists in the database
        var bookingEntity = await _context.Bookings.FirstOrDefaultAsync(x => x.Datetime == customer.Datetime && x.BookingType == customer.BookingType && x.Status == customer.Status);
        if (bookingEntity != null)
            customerEntity.BookingId = bookingEntity.Id;
        else
        {
            // Create a new booking entity with the new status and add it to the database
            var newBookingEntity = new BookingEntity
            {
                Datetime = customer.Datetime,
                BookingType = customer.BookingType,
                Status = customer.Status
            };
            _context.Add(newBookingEntity);
            customerEntity.Booking = newBookingEntity;
        }

        _context.Add(customerEntity);
        await _context.SaveChangesAsync();
    }




    public static async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var customers = new List<Customer>();

        foreach (var customer in await _context.Customers.Include(x => x.Animal).Include(x => x.Booking).ToListAsync())
            customers.Add(new Customer
            {
                Id= customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
        
                AnimalId= customer.AnimalId,
                AnimalName = customer.Animal.AnimalName,
                AnimalBreed = customer.Animal.AnimalBreed,

                BookingId= customer.BookingId,
                Datetime = customer.Booking.Datetime,
                BookingType = customer.Booking.BookingType,
                Status = customer.Booking.Status
            });

        return customers;
    }


    public static async Task<Customer> GetAsync(string email)
    {
        var customer = await _context.Customers.Include(x => x.Animal).Include(x => x.Booking).FirstOrDefaultAsync(x => x.Email == email);
        if (customer != null)
            return new Customer
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,

                AnimalId = customer.AnimalId,
                AnimalName = customer.Animal.AnimalName,
                AnimalBreed = customer.Animal.AnimalBreed,

                Datetime = customer.Booking.Datetime,
                BookingType = customer.Booking.BookingType,
                Status = customer.Booking.Status
            };
        else
            return null!;
    }





    public static async Task UpdateAsync(Customer customer)
    {
        var customerEntity = await _context.Customers
            .Include(x => x.Booking)
            .Include(x => x.Animal)
            .FirstOrDefaultAsync(x => x.Id == customer.Id);

        if (customerEntity != null)
        {
            if (!string.IsNullOrEmpty(customer.FirstName))
                customerEntity.FirstName = customer.FirstName;
            

            if (!string.IsNullOrEmpty(customer.LastName))
                customerEntity.LastName = customer.LastName;
            

            if (!string.IsNullOrEmpty(customer.Email))        
                customerEntity.Email = customer.Email;
            

            if (!string.IsNullOrEmpty(customer.PhoneNumber))         
                customerEntity.PhoneNumber = customer.PhoneNumber;
            



            if (!string.IsNullOrEmpty(customer.AnimalName) ||
                !string.IsNullOrEmpty(customer.AnimalBreed))
            {
                var animalEntity = await _context.Animals.FirstOrDefaultAsync(x => x.AnimalName == customer.AnimalName && x.AnimalBreed == customer.AnimalBreed );

                if (animalEntity != null)
                    customerEntity.AnimalId = animalEntity.Id;
                else
                    customerEntity.Animal = new AnimalEntity
                    {
                        AnimalName = customer.AnimalName,
                        AnimalBreed = customer.AnimalBreed,
                    };
            }



            if (customer.Datetime != DateTime.MinValue ||
                !string.IsNullOrEmpty(customer.BookingType) ||
                customer.Status != default(BookingStatus))
            {
                var bookingEntity = await _context.Bookings.FirstOrDefaultAsync(x =>
                    x.Datetime == customer.Datetime &&
                    x.BookingType == customer.BookingType &&
                    x.Status == customer.Status);

                if (bookingEntity != null)
                {
                    customerEntity.BookingId = bookingEntity.Id;
                }
                else if (!string.IsNullOrEmpty(customer.BookingType))
                {
                    customerEntity.Booking = new BookingEntity
                    {
                        Datetime = customer.Datetime,
                        BookingType = customer.BookingType,
                        Status = customer.Status
                    };
                }
            }
            _context.Update(customerEntity);
            await _context.SaveChangesAsync();
        }
    }





    //if (!string.IsNullOrEmpty(customer.AnimalName) ||
    //    !string.IsNullOrEmpty(customer.AnimalBreed))
    //{
    //    if (customerEntity.Animal == null)
    //    {
    //        customerEntity.Animal = new AnimalEntity();
    //    }

    //    customerEntity.Animal.AnimalName = customer.AnimalName;
    //    customerEntity.Animal.AnimalBreed = customer.AnimalBreed;
    //}

    //if (customer.BookingId > 0)
    //{
    //    if (customerEntity.Booking == null)
    //    {
    //        customerEntity.Booking = new BookingEntity();
    //    }

    //    customerEntity.Booking.Id = customer.BookingId;
    //    customerEntity.Booking.Datetime = customer.Datetime;
    //    customerEntity.Booking.BookingType = customer.BookingType;
    //    customerEntity.Booking.Status = customer.Status; // update the booking status property
    //}



    //if (customer.Datetime != DateTime.MinValue ||
    //    !string.IsNullOrEmpty(customer.BookingType) ||
    //    customer.Status != default(BookingStatus))
    //{
    //    var bookingEntity = await _context.Bookings.FirstOrDefaultAsync(x => x.Datetime == customer.Datetime && x.BookingType == customer.BookingType && x.Status == customer.Status );
    //    if (bookingEntity != null)
    //        customerEntity.BookingId = bookingEntity.Id;
    //    else
    //        customerEntity.Booking = new BookingEntity
    //        {
    //            Datetime = customer.Datetime,
    //            BookingType = customer.BookingType,
    //            Status = customer.Status
    //        };

    //}




}
