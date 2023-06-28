﻿using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace AppTripCliente.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageContainer : Xamarin.Forms.TabbedPage
    {
        public TabbedPageContainer ()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSmoothScrollEnabled(true);
        }
    }
}