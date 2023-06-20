using AppTripCliente.Data.Home;
using AppTripCliente.ViewModel.HomeViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.Home
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{
		private readonly TripData data = null;
		public Home()
		{
			InitializeComponent();
			data = new TripData();
			BindingContext = new HomeVM(Navigation);
			changeContentView();
		}

		public async void changeContentView()
		{
			var QuotesNumber = await data.CountQuotes();
			if (QuotesNumber > 0)
			{
				QuotesView.Content = new WithQuotes();
			}
			else
			{
				QuotesView.Content = new WithoutQuotes();
			}
		}
    }
}