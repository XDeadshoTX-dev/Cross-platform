namespace Lab9NETMAUIMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
