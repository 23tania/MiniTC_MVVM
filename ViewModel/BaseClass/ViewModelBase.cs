using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MiniTC.ViewModel.BaseClass
{
    class ViewModelBase : INotifyPropertyChanged
    {
        // Zdarzenie informujące o zmiane własności w obiekcie ViewModelu
        public event PropertyChangedEventHandler PropertyChanged;

        // Metoda zgłaszjąca zmiany w własościach podanych jako argumenty
        protected void onPropertyChanged(params string[] namesOfProperties)
        {
            if (PropertyChanged != null)
            {
                foreach (var prop in namesOfProperties)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(prop));
                }
            }
        }


    }
}
