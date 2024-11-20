using Auth0.OidcClient;
using IdentityModel.OidcClient;
using System.Diagnostics;

namespace Lab9NETMAUIMobile
{
    public partial class MainPage : ContentPage
    {
        private OidcClient _oidcClient;

        public MainPage()
        {
            InitializeComponent();
            //InitializeAuth0();
        }

        private async void InitializeAuth0()
        {
            string _identityServerUrl = "https://dev-cuaqrq8o7875e7fg.us.auth0.com";
            string _clientId = "Rm3NZZsXKBKw4pJcJCBv3DNlAUwhwD6k";
            string _redirectUri = "myapp://callback";

            var options = new OidcClientOptions
            {
                Authority = _identityServerUrl,
                ClientId = _clientId,
                RedirectUri = _redirectUri,
                Scope = "openid profile email",
            };

            var client = new OidcClient(options);

            try
            {
                var authorizeUrl = new Uri($"{_identityServerUrl}/authorize?response_type=code&client_id={_clientId}&redirect_uri={Uri.EscapeDataString(_redirectUri)}&scope=openid%20profile%20email");

                var result = await WebAuthenticator.AuthenticateAsync(authorizeUrl, new Uri(_redirectUri));

                if (result?.AccessToken != null)
                {
                    Console.WriteLine($"Successfully logged in. Token: {result.AccessToken}");
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Authentication was canceled by the user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
            }
        }
        public async void OnCentralTableClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CentralTablePage());
        }

        public async void OnVehicleCategoryInformationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VehicleCategoryInformationPage());
        }

        public async void OnModelInformationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ModelInformationPage());
        }

        public async void OnGraphicClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GraphicPage());
        }

        public async void OnAboutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
