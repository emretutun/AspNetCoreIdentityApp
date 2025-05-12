using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspNetCoreIdentityApp.Web.Requirements
{
    public class ViolenceRequirment : IAuthorizationRequirement
    {
        public int ThresholdAge { get; set; }
    }
    public class ViolenceRequirmentHandler : AuthorizationHandler<ViolenceRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViolenceRequirment requirement)
        {

            if (!context.User.HasClaim(x => x.Type == "birthdate"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            Claim birthDateClaim = context.User.FindFirst("birthdate")!;
            var today = DateTime.Now;
            var birthDate = Convert.ToDateTime(birthDateClaim.Value);
            var age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age)) age--;

            if (age >= requirement.ThresholdAge)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;


        }


    }
}

