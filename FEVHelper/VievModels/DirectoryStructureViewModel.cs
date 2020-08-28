using FEVHelper.Commands;
using FEVHelper.CSV;
using FEVHelper.Directories;
using FEVHelper.Directories.Data;
using FEVHelper.Helpers;
using FEVHelper.Models;
using FEVHelper.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace FEVHelper.VievModels
{
    /// <summary>
    /// the view model for applications main Directory view
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {
        #region Private properties
        /// <summary>
        /// Static string value, used for comparing paths
        /// </summary>
        private static string xcfm = ".xcfm";
        /// <summary>
        /// Static string value, used for comparing paths
        /// </summary>
        private static string csv = ".csv";
        #endregion

        #region Public properties

        /// <summary>
        ///  A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        public List<Quantity> ReadQuantities { get; set; }

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
        /// Actions when Read XML button is clicked
        /// </summary>
        public ICommand ReadXMLFileCommand => new CommandHandler(CanExecute, OnXMLRead);
        /// <summary>
        /// Actions when Save button is clicked
        /// </summary>
        public ICommand SaveCSVFileCommand => new CommandHandler(CanExecute, OnCSVSave);
        public ICommand AutomaticallySavingCommand => new CommandHandler(CanExecute, OnAutomaticallyCSVSave);
        public ICommand AutomaticallyOpenCSVFileCommand => new CommandHandler(CanExecute, OnAutomaticallyOpenCSVFile);

        /// <summary>
        /// Chosen CSV file path
        /// </summary>
        public string ChosenCSVFilePath { get; set; }
        /// <summary>
        /// quantities path
        /// </summary>
        public string QuantitiesPath { get; set; }
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

            // Binding CommandHandler logic to view.
            // This sytax is a example of polimorphism
            this.SelectedItemChangedCommand = new CommandHandler(CanExecute, OnSelectionChange);

            this.ChooseCSVFileCommand = new CommandHandler(CanExecute, OnCSVChosen);

            this.ReadQuantities = new List<Quantity>();

            CSVFile cSVFile = new CSVFile();




        }


        #endregion

        #region Methods
        // Always return true thanks to body expression
        public bool CanExecute(object parameter) => true;

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
            QuantitiesPath = b.FullPath;
        }
        /// <summary>
        /// Actions when CSV choose button is clicked
        /// </summary>
        /// <param name="obj"></param>
        private void OnCSVChosen(object obj)
        {
            // Assign string property to result of SelectCSVFile method
            ChosenCSVFilePath = FileHelper.SelectCSVFile();
        }
        /// <summary>
        /// Read XML values
        /// </summary>
        /// <param name="obj"></param>
        private void OnXMLRead(object obj)
        {
            // Check if quantities path exists
            if (FileHelper.QuanttiesPathExists(QuantitiesPath) == false)
                return;

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
            // Get quantity measurements isChecked property and add to the list if it's true
            var quantityReference = (bool)values[0];
            if (quantityMeasurements == true)
                activeMembers.Add("QuantityReference");
            // Get quantity string isChecked property and add to the list if it's true
            var quantityFormula = (bool)values[1];
            if (quantityString == true)
                activeMembers.Add("QuantityFormula");
            // Get quantity constant isChecked property and add to the list if it's true
            var quantityTable = (bool)values[2];
            if (quantityConstant == true)
                activeMembers.Add("QuantityTable");
            // Get quantity constant isChecked property and add to the list if it's true
            var quantityLUT = (bool)values[2];
            if (quantityConstant == true)
                activeMembers.Add("QuantityLookupTable");

            // Invoke Read method and pass active members and quantities path
            this.ReadQuantities = XMLFile.Read(activeMembers,QuantitiesPath);

        }
        /// <summary>
        /// Save read quantities to CSV file
        /// </summary>
        /// <param name="obj"></param>
        private void OnCSVSave(object obj)
        {
            // Check whether csv path exists
            if (FileHelper.CSVPathExists(ChosenCSVFilePath) == false)
                return;

            // Invoke the save method
            CSVFile.Save(ReadQuantities, this.ChosenCSVFilePath);
        }

        private void OnAutomaticallyCSVSave(object obj)
        {
            CheckBox checkbox = new CheckBox();
            checkbox = (CheckBox)obj;

            if (checkbox.IsChecked == true)
            {
                if (FileHelper.QuanttiesPathExists(QuantitiesPath) == true && FileHelper.CSVPathExists(ChosenCSVFilePath) == true)
                {
                    XMLFile.QuantitiesRead += OnQuantitiesRead;
                }
                else
                {
                    checkbox.IsChecked = false;
                }
            }
            else
            {
                XMLFile.QuantitiesRead -= OnQuantitiesRead;
            }

        }
        public void OnQuantitiesRead(object sender, EventArgs e)
        {
            CSVFile.Save(ReadQuantities, this.ChosenCSVFilePath);
        }
        private void OnAutomaticallyOpenCSVFile(object obj)
        {
            CheckBox checkbox = new CheckBox();
            checkbox = (CheckBox)obj;

            if (checkbox.IsChecked == true)
                CSVFile.AutoOpenFile = true;
            else
                CSVFile.AutoOpenFile = false;

        }

        #endregion

    }
}

