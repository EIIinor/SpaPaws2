using SpaPaws2.Models.Enteties;
using static SpaPaws2.Models.Enteties.BookingEntity;

namespace SpaPaws2.Models;

internal class Customer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;


    public int AnimalId { get; set; }
    public string AnimalName { get; set; } = null!;
    public string AnimalBreed { get; set; } = null!;


    public int BookingId { get; set; }
    public DateTime Datetime { get; set; } = DateTime.Now;
    public string BookingType { get; set; } = null!;
    public BookingStatus Status { get; set; }

}
