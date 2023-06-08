using AppTripCliente.View.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageContainer : Xamarin.Forms.TabbedPage
    {
        public TabbedPageContainer (string Id)
        {
            InitializeComponent();
            string userID = Id;
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSmoothScrollEnabled(true);
            var account = Children[3] as Account.Account;
        }



    }
}