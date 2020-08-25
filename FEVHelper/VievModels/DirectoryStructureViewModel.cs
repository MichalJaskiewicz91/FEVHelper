using FEVHelper.Commands;
using FEVHelper.Directories;
using FEVHelper.Directories.Data;
using FEVHelper.Helpers;
using FEVHelper.Models;
using FEVHelper.XML;
using System.Collections.Generic;
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

        public List<Quantity> Stringi { get; set; }

        /// <summary>
        /// Command that allows changing default path
        /// </summary>
        public ICommand ChangeDefaultPathCommand { get; set; }

        /// <summary>
        /// Command that handles when selected item is changed
        /// </summary>
        public ICommand SelectedItemChangedCommand { get; set; }
        /// <summary>
        /// Action when choosen button is clicked
        /// </summary>
        public ICommand ChooseCSVFileCommand { get; set; }
        /// <summary>
        /// Actions when Read XML button is cllicked
        /// </summary>
        public ICommand ReadXMLFileCommand => new CommandHandler(CanExecute, OnXMLRead);
        public string ChosenCSVFlePath { get; set; }
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

            this.ChooseCSVFileCommand = new CommandHandler(CanExecute, OnCSVChosen);

            Quantity quantity = new Quantity { ID = "First", Description = "Descirption" };
            this.Stringi = new List<Quantity>();
            //this.Stringi.Add(quantity);

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
            // Casting object
            RoutedPropertyChangedEventArgs<object> e = (RoutedPropertyChangedEventArgs<object>)parameter;
            //Getting new value
            var newValue = e.NewValue;
            // Casting new value
            DirectoryItemViewModel b = (DirectoryItemViewModel)newValue;
            // Assigning string value to property
            LolPath = b.FullPath;
        }
        /// <summary>
        /// Actions when CSV choose button is clicked
        /// </summary>
        /// <param name="obj"></param>
        private void OnCSVChosen(object obj)
        {
            // Assign string property to result of SelectCSVFile method
            ChosenCSVFlePath = FileHelper.SelectCSVFile();
        }
        /// <summary>
        /// Read XML values
        /// </summary>
        /// <param name="obj"></param>
        private void OnXMLRead(object obj)
        {
            //List of active members
            List<string> activeMembers = new List<string>();
            // Cast checkboxes to array of objects
            var values = (object[])obj;

            // Get quantity measurements isChecked property and add to the list if it's true
            var quantityMeasurements = (bool)values[0];
            if (quantityMeasurements == true)
                activeMembers.Add("QuantityMeasurement");
            // Get quantity string isChecked property and add to the list if it's true
            var quantityString = (bool)values[1];
            if (quantityString == true)
                activeMembers.Add("QuantityString");
            // Get quantity constant isChecked property and add to the list if it's true
            var quantityConstant = (bool)values[2];
            if (quantityConstant == true)
                activeMembers.Add("QuantityConstant");

            // Invoke Read method
            this.Stringi = XMLFile.Read(activeMembers);

        }
        #endregion

    }
}

