using ErrorOr;

namespace BuberDinner.Domain.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidEmail => Error.Validation(code: "User.InvalidEmail", description: "Invalid Credentials");
            public static Error InvalidPassword => Error.Validation(code: "User.WrongPassword", description: "Password is Wrong");
        }
    }
}
