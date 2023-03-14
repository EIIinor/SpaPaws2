using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPaws2.Models.Enteties;

internal class BookingEntity
{
    [Key]
    public int Id { get; set; }

    public DateTime Datetime { get; set; } = DateTime.Now;

    [Column(TypeName = "nvarchar(300)")]
    public string BookingType { get; set; } = null!;

    public enum BookingStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
    public BookingStatus Status { get; set; } = BookingStatus.NotStarted;

    public ICollection<CustomerEntity> Customers = new HashSet<CustomerEntity>();

}


