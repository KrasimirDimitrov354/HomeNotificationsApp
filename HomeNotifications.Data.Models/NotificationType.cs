namespace HomeNotifications.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TypeConstants = Common.EntityFieldValidation.NotificationType;

public class NotificationType
{
    public NotificationType()
    {
        Notifications = new HashSet<Notification>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(TypeConstants.TypeNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = TypeConstants.TypeTypeName)]
    public string Color { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; }

    //System columns
    public Guid Created_By_Id { get; set; }
    public DateTime Created_Date { get; set; }

    public Guid? Modified_By_Id { get; set; }
    public DateTime? Modified_Date { get; set; }
}
