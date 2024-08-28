namespace HomeNotifications.Common;

public static class EntityFieldValidation
{
    public static class User
    {
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 30;
        public const string UsernameLengthErrorMessage = "Username must be between 3 and 30 symbols long.";
        public const string UserNameExistsErrorMessage = "Username already exists!";

        public const int PasswordMinLength = 3;
        public const int PasswordMaxLength = 50;
        public const string PasswordHashTypeName = "char(110)";
        public const string PasswordLengthErrorMessage = "Password must be between 3 and 50 symbols long.";
        public const string PasswordExistsErrorMessage = "Password must be different from the previous one.";
        public const string PasswordNotMatchingErrorMessage = "The password and confirmation password do not match!";
        
        public const string ConfirmPasswordFieldDisplayName = "Confirm password";
        public const string RoleFieldDisplayName = "Role";
    }

    public static class Role
    {
        public const int RoleMinLength = 3;
        public const int RoleMaxLength = 20;
        public const string RoleLengthErrorMessage = "Role must be between 3 and 20 symbols long.";
        public const string RoleExistsErrorMessage = "Role already exists!";
    }

    public static class Notification
    {
        public const int ContentMinLength = 3;
        public const int ContentMaxLength = 100;
        public const string NotificationLengthErrorMessage = "Notification content must be between 3 and 100 symbols long.";
        public const string NotificationTypeDisplayName = "Type";
    }

    public static class NotificationType
    {
        public const int TypeNameMinLength = 3;
        public const int TypeNameMaxLength = 30;
        public const string TypeTypeName = "char(7)";
        public const string TypeNameLengthErrorMessage = "The name of the notification type must be between 3 and 30 symbols long.";
        public const string TypeNameAlreadyExists = "The notification type already exists!";
    }
}
