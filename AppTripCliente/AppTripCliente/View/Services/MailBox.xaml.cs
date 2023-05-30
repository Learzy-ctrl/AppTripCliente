using AppTripCliente.ViewModel.ServicesVIewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View.Services
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MailBox : ContentPage
	{
		public MailBox ()
		{
			InitializeComponent ();
			BindingContext = new MailBoxVM(Navigation);
		}
	}
}