namespace Domain.Constants;

public static class EntityConstraints
{
    public static class User
    {
        public const int FirstNameMaxLength = 100;
        public const int LastNameMaxLength = 100;

        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 64;
    }
}