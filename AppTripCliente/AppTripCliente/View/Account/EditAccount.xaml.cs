using AppTripCliente.Model;
using AppTripCliente.ViewModel.AccountViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.Account
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditAccount : ContentPage
	{
		public EditAccount (User model)
		{
			InitializeComponent ();
			BindingContext = new EditAccountVM(Navigation, model);
		}
	}
}