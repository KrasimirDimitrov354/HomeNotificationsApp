namespace HomeNotifications.Web.Models.Notification;

using System.ComponentModel.DataAnnotations;

using HomeNotifications.Web.Models.Type;

using NotificationConstants = Common.EntityFieldValidation.Notification;

public class NotificationFormModel
{
    public NotificationFormModel()
    {
        Types = new HashSet<TypeDropdownSelect>();
    }

    [Required]
    [StringLength(NotificationConstants.ContentMaxLength, 
        MinimumLength = NotificationConstants.ContentMinLength,
        ErrorMessage = NotificationConstants.NotificationLengthErrorMessage)]
    public string Content { get; set; } = null!;

    [Display(Name = NotificationConstants.NotificationTypeDisplayName)]
    public int TypeId {  get; set; }

    public ICollection<TypeDropdownSelect> Types { get; set; }
}
