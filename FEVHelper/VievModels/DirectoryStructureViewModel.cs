using FEVHelper.Commands;
using FEVHelper.Directories;
using FEVHelper.Directories.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FEVHelper.VievModels
{
    /// <summary>
    /// the view model for applications main Directory view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Public properties
        /// <summary>
        ///  A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        /// <summary>
        /// Command that allows changing default path
        /// </summary>
        public ICommand ChangeDefaultPathCommand { get; set; }

        /// <summary>
        /// Command that handles when selected item is changed
        /// </summary>
        public ICommand SelectedItemChangedCommand { get; set; }
        /// <summary>
        /// Default bench path
        /// </summary>
        public string BenchPath { get; set; }
        /// <summary>
        /// Dummy path
        /// </summary>
        public string LolPath { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Default contructor
        /// </summary>
        public DirectoryStructureViewModel()
        {

            // Get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            //Create the view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(children.Select(drive => new DirectoryItemViewModel(drive.FullPath, DirectoryItemType.Drive)));

            //Get default bench path from DummyDataVieModel
            this.BenchPath = DummyDataViewModel.BenchPath;

            // Binding CommandHandler logic to view.
            // This sytax is a example of polimorphism
            this.ChangeDefaultPathCommand = new CommandHandler(CanExecute, OnPathChanged);

            // Binding CommandHandler logic to view.
            // This sytax is a example of polimorphism
            this.SelectedItemChangedCommand = new CommandHandler(CanExecute, OnSelectionChange);
        }

        #endregion

        #region Methods
        // Always return true
        public bool CanExecute(object parameter) => true;
        /// <summary>
        /// Actions when button is clicked
        /// </summary>
        /// <param name="parameter"></param>
        private void OnPathChanged(object parameter)
        {
            LolPath = BenchPath;
        }
        /// <summary>
        /// Actions when selection is changed
        /// </summary>
        /// <param name="parameter"></param>
        private void OnSelectionChange(object parameter)
        {
            RoutedPropertyChangedEventArgs<object> e = (RoutedPropertyChangedEventArgs<object>)parameter;
            var newValue = e.NewValue;
            DirectoryItemViewModel b = (DirectoryItemViewModel)newValue;
            LolPath = b.FullPath;
        }

        #endregion

    }
}
