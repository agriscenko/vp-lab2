using System.ComponentModel.DataAnnotations;

namespace Lab2.DataAccess;

public class LeaveRequest
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public required string Type { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime EndDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime RequestDate { get; set; }

    [StringLength(200)]
    public string? Comments { get; set; }

    public int? ApprovedById { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ApprovalDate { get; set; }

    [StringLength(200)]
    public string? ApprovalComments { get; set; }

    public required Employee Employee { get; set; }
}
