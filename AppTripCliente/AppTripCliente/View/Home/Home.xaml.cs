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
			BindingContext = new HomeVM(Navigation, this);
		}

		public void ChangePage(bool flag , bool checkInternet)
		{
			if (checkInternet)
			{
                if (flag)
                {
                    QuotesView.Content = new WithQuotes();
                }
                else
                {
                    QuotesView.Content = new WithoutQuotes();
                }
			}
			else
			{
				QuotesView.Content = new WithoutInternet();
			}
			
		}
    }
}