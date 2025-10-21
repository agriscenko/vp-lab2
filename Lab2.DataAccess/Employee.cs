using System.ComponentModel.DataAnnotations;

namespace Lab2.DataAccess;

public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public required string LastName { get; set; }

    [Required]
    [StringLength(50)]
    public required string Position { get; set; }

    [Required]
    public required decimal Salary { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime HireDate { get; set; }

    public bool IsActive { get; set; } = true;

    public required Department Department { get; set; }
}
