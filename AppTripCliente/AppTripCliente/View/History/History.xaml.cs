using AppTripCliente.View.Account;
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
	public partial class History : ContentPage
	{
		public History ()
		{
			InitializeComponent ();
			BindingContext = new HistoryVM(Navigation, this);
		}

		public void ChangeView(bool flag)
		{
			if (flag)
			{
				jijiji.Content = new WithHistory();
			}
			else
			{
				jijiji.Content = new WithoutHistory();
			}
		}
	}
}