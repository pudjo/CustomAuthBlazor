using System.Security.Claims;

namespace Sigereng.Model
{
    public record LogedInUserModel(int Id, string Name, string Email)
    {
        public Claim[] ToClaim() =>
        [
            new Claim (ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim (ClaimTypes.Name, Name),
            new Claim (ClaimTypes.Email, Email),

            



        ];



    };
    
}
