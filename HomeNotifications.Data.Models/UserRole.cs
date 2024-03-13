namespace HomeNotifications.Data.Models;

using System.ComponentModel.DataAnnotations;

using RoleConstants = Common.EntityFieldValidation.Role;

public class UserRole
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(RoleConstants.RoleMaxLength)]
    public string Name { get; set; } = null!;

    //System columns
    public Guid Created_By_Id { get; set; }
    public DateTime Created_Date { get; set; }

    public Guid? Modified_By_Id { get; set; }
    public DateTime? Modified_Date { get; set; }
}
