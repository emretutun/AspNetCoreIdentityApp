using AspNetCoreIdentityApp.Core.Models;
using AspNetCoreIdentityApp.Web.CustomValidations;
using AspNetCoreIdentityApp.Web.Localizations;
using AspNetCoreIdentityApp.Repository.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Extenisons
{
    public static class StartupExtensions
    {

        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromHours(2);
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = ""; izin verilen kullanıcı adı karakterlerini gir ab..*123ÇAKL gibi

                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = false;


                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                options.Lockout.MaxFailedAccessAttempts = 3;

            }


        )
        .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
        .AddPasswordValidator<PasswordValidator>()
        .AddUserValidator<UserValidator>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<AppDbContext>();
        

        }

    }
}
