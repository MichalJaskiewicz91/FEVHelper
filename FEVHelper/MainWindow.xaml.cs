using FEVHelper.VievModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace FEVHelper
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        //Default constructor
        public MainWindow()
        {
            InitializeComponent();

            // Binding UI to viewmodel
            this.DataContext = new DirectoryStructureViewModel();
        }
        #endregion

        private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
