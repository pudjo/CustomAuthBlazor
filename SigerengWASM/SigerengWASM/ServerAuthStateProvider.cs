using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using SigerengWASM.CLient.Model;

namespace SigerengWASM
{
    public class ServerAuthStateProvider : ServerAuthenticationStateProvider,IDisposable// ServerAuthenticationStateProvider
    {
        PersistentComponentState _componentState;
        // to pass the parameter to client status user sudah login atau belum
        private PersistingComponentStateSubscription _subCription;
        private Task<AuthenticationState> athenticationStateTask;
        public ServerAuthStateProvider(PersistentComponentState componenState)
        {
            _componentState= componenState;
            AuthenticationStateChanged += ServerAuthStateProvider_AuthenticationStateChange;
            _subCription=_componentState.RegisterOnPersisting(PersistComponentSateAsync, RenderMode.InteractiveWebAssembly);
        }

        private void ServerAuthStateProvider_AuthenticationStateChange(Task<AuthenticationState> task)
        {
            athenticationStateTask = task;
        }
        private async Task PersistComponentSateAsync()
        {
            var authState = await athenticationStateTask;
            if (authState.User.Identity.IsAuthenticated== true)
            {
                var loggedUser = LogedInUserModel.FromPrincipal(authState.User);
                _componentState.PersistAsJson(nameof(LogedInUserModel), loggedUser);
               
            }
        }

        public void Dispose()
        {
            AuthenticationStateChanged -= ServerAuthStateProvider_AuthenticationStateChange;
            _subCription.Dispose();
        }
    }
}
