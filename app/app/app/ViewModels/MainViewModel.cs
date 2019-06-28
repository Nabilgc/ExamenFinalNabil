using System;
using System.Collections.Generic;
using System.Text;

namespace app.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels

        public NabilViewModel Galebs
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Galebs = new NabilViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion 
    }
}

