using AppTripCliente.ViewModel.HistoryViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.History
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WithoutHistory : ContentView
	{

		public WithoutHistory ()
		{
			InitializeComponent ();
		}
	}
}