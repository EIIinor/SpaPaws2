using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPaws2.Models.Enteties;

internal class AnimalEntity
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string AnimalName { get; set; } = null!;

    [Column(TypeName = "nvarchar(20)")]
    public string AnimalBreed { get; set; } = null!;

    public ICollection<CustomerEntity> Customers = new HashSet<CustomerEntity>();
}
