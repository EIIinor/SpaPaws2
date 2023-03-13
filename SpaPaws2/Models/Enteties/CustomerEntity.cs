using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPaws2.Models.Enteties;


[Index(nameof(Email), IsUnique = true)]

internal class CustomerEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;


    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;


    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; } = null!;

    [Column(TypeName = "char(13)")]
    public string PhoneNumber { get; set; } = null!;


    public int AnimalId { get; set; }
    public AnimalEntity Animal { get; set; } = null!;

    public int BookingId { get; set; }
    public BookingEntity Booking { get; set; } = null!;
}
