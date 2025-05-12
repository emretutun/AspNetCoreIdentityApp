using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {

        public override IdentityError DuplicateUserName(string UserName)
        {
            //return base.DuplicateUserName(UserName);
            return new() {Code = "DuplicateUserName" ,Description = $"{UserName} kullanıcı adı başka bir kullanıcı tarafından alınmıştır." };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            //return base.DuplicateEmail(email);
            return new() { Code = "DuplicateEmail", Description = $"{email}  başka bir kullanıcı tarafından alınmıştır." };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            //return base.PasswordTooShort(length);
            return new() { Code = "DuplicateTooShort", Description = $"Şifre En az 6 Karakterli olmalıdır!" };
            
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            //return base.PasswordRequiresNonAlphanumeric();
            return new() { Code = "DuplicateAlphanumeric", Description = "Şifre en az 1 tane alfanümerik karakter içermelidir!" };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            //return base.PasswordRequiresUpper();
            return new() { Code = "DuplicateUpper", Description = "Şifre en az 1 büyük harf içermelidir!" };
        }

    }
}
