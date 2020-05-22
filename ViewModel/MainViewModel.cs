using System;
using System.Collections.Generic;
using System.Text;
using MiniTC.ViewModel.BaseClass;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;

namespace MiniTC.ViewModel
{
    using Model;
    class MainViewModel : ViewModelBase
    {
        Model model = new Model();

        private string _currentLeftDirectory = null;
        private string _currentRightDirectory = null;

        // Obecny wybrany folder na lewym panelu
        public string CurrentLeftDirectory
        {
            get { return _currentLeftDirectory; }
            set
            {
                _currentLeftDirectory = value;
                onPropertyChanged(nameof(CurrentLeftFiles));
                onPropertyChanged(nameof(CurrentLeftDirectory));
            }
        }

        // Obecne pliki na lewym panelu
        public ObservableCollection<string> CurrentLeftFiles
        {
            get
            {
                return new ObservableCollection<string>
                    (model.GetFiles(CurrentLeftDirectory));
            }
        }

        // Obecny wybrany folder na prawym panelu
        public string CurrentRightDirectory
        {
            get { return _currentRightDirectory; }
            set
            {
                _currentRightDirectory = value;
                onPropertyChanged(nameof(CurrentRightFiles));
                onPropertyChanged(nameof(CurrentRightDirectory));
            }
        }

        // Obecne pliki na prawym panelu
        public ObservableCollection<string> CurrentRightFiles
        {
            get
            {
                return new ObservableCollection<string>
                    (model.GetFiles(CurrentRightDirectory));
            }
        }

        // Dostępne dyski
        public ObservableCollection<string> CurrentDrives
        {
            get
            {
                return new ObservableCollection<string>(model.GetDrives());
            }
        }

        // Wybrany plik
        public string SelectedFile { get; set; }

        // Kliknięcie na lewym panelu
        private ICommand _leftClick = null;
        public ICommand LeftClick
        {
            get
            {
                if (_leftClick == null)
                {
                    _leftClick = new RelayCommand(
                        x => { CurrentLeftDirectory = model.ChangePath
                            (CurrentLeftDirectory, SelectedFile); },
                        x => true);
                }
                return _leftClick;
            }
        }

        // Kliknięcie na prawym panelu
        private ICommand _rightClick = null;
        public ICommand RightClick
        {
            get
            {
                if (_rightClick == null)
                {
                    _rightClick = new RelayCommand(
                    (arg) => { CurrentRightDirectory = model.ChangePath
                        (CurrentRightDirectory, SelectedFile); },
                    (arg) => true
                    );
                }
                return _rightClick;
            }
                
        }

        // Przycisk kopiujący plik
        private ICommand _copy = null;
        public ICommand Copy
        {
            get
            {
                if (_copy == null)
                {
                    _copy = new RelayCommand(x =>
                    {
                        if (CurrentRightDirectory != null)
                        {
                            string source = CurrentLeftDirectory + @"\" + SelectedFile;
                            string destination = _currentRightDirectory + @"\" + SelectedFile;

                            model.CopyFile(source, destination);
                        }

                        onPropertyChanged(nameof(CurrentRightFiles));
                    },
                    x => true);
                }
                return _copy;
            }
        }
    }
}
