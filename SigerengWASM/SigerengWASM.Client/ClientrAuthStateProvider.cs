using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SigerengWASM.Client.Model;
using SigerengWASM.CLient.Model;
using System.Security.Claims;

namespace SigerengWASM.Client
{
    public class ClientrAuthStateProvider : AuthenticationStateProvider
    {
      //  private readonly PersistentComponentState _componenState;
        private Task<AuthenticationState> _athenticationStateTask= Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
        public ClientrAuthStateProvider(PersistentComponentState componenState)
        {
            ClaimsPrincipal principal;
            if (componenState.TryTakeFromJson<LogedInUserModel>(nameof(LogedInUserModel),out var loggedUser) &&
                loggedUser != null  && loggedUser.Id != 0) {

                var claim = loggedUser.ToClaim();
                var identity = new ClaimsIdentity(claim,Constant.AuthName);
                principal = new ClaimsPrincipal(identity);

            }
            else
            {
                
                var identity = new ClaimsIdentity();
                principal = new ClaimsPrincipal(identity);

                
            }

            var authSate = new AuthenticationState(principal);
            _athenticationStateTask = Task.FromResult(authSate);
    }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() => _athenticationStateTask;

    }
}
