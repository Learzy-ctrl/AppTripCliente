﻿using AppTripCliente.View.History;
using AppTripCliente.View.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppTripCliente.ViewModel.HistoryViewModel
{
    public class WithHistoryVM : BaseViewModel
    {
        #region Variables
        #endregion

        #region Constructor
        public WithHistoryVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion

        #region Objetcs
        #endregion

        #region Processes

        public void GoToTripDetail()
        {
            Navigation.PushModalAsync(new TripDetail());
        }

        #endregion

        #region Commands
        public ICommand GoToTripDetailCommand => new Command(GoToTripDetail);

        #endregion
    }
}