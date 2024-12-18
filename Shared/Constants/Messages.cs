namespace Shared.Constants;

public static class Messages
{
    public static class Validation
    {
        public const string NotEmpty = "Bu alan boş bırakılamaz.";
        public static string MaxLength(int length) => $"En fazla {length} karakter giriniz.";
        public static string MinLength(int length) => $"En az {length} karakter giriniz.";
        public static string RangeLength(int from, int to) => $"En az {from} en fazla {to} karakter giriniz.";

        public static string PasswordLengthInvalid(int min, int max) => $"Şifreniz en az {min}, en fazla {max} karakter uzunluğunda olmalıdır.";
        public const string PasswordComplexity = "Şifreniz en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.";

        public const string EmailAddressInvalid = "Lütfen geçerli bir e-posta adresi giriniz.";
    }
}
