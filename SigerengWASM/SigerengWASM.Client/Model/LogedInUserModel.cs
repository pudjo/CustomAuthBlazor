using System.Security.Claims;

namespace SigerengWASM.CLient.Model
{
    public record LogedInUserModel(int Id, string Name, string Email)
    {
        public Claim[] ToClaim() =>
        [
            new Claim (ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim (ClaimTypes.Name, Name),
            new Claim (ClaimTypes.Email, Email),
        ];
        public static LogedInUserModel FromPrincipal(ClaimsPrincipal principal)
        {
            //var claims = principal.Claims;
            //var
            var userId = Convert.ToInt32(principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var name = principal.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var email = principal.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            return new LogedInUserModel(userId, name, email);

        }



    };
    
}
