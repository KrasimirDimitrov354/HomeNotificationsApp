namespace HomeNotifications.Common;

public static class Messages
{
    public const string UnexpectedError = "Unexpected error occured, contact admin.";
    public const string UnauthorizedAccess = "You do not have access to the requested page.";

    public static class User
    {
        public const string InvalidCredentialsError = "Invalid credentials provided.";
        public const string UsernameAlreadyExistsError = "Username already exists.";
        public const string PasswordMismatchError = "Passwords don't match.";

        public const string UserCreated = "User successfully created.";
    }

    public static class Role
    {
        public const string InvalidIdError = "Role selection failed, please try again.";
    }
}
