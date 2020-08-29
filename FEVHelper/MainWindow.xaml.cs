
using FEVHelper.Models;
using FEVHelper.VievModels;
using System.Windows;


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
            // Declatation of FEV database
            FEVEntities db = new FEVEntities();
            var log = db.Logs;

            foreach (var item in log)
            {
                
            }

            // Binding UI to viewmodel
            this.DataContext = new DirectoryStructureViewModel();
        }
        #endregion
        /// <summary>
        /// Method attached to SelectedItemChangedCommand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
