using static SpaPaws2.Models.Enteties.BookingEntity;

namespace SpaPaws2.Models;

internal class Customer
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;


    public string AnimalName { get; set; } = null!;
    public string AnimalBreed { get; set; } = null!;


    public DateTime Datetime { get; set; } = DateTime.Now;
    public string Booking { get; set; } = null!;
    public BookingStatus BookingStatus { get; set; } = BookingStatus.NotStarted;

}
