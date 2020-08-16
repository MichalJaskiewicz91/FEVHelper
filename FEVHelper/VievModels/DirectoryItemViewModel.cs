using FEVHelper.Directories;
using FEVHelper.Directories.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FEVHelper.VievModels
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }
        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name
        {
            get
            {
                return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath);
            }
        }
        /// <summary>
        /// A list of all children inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children
        {
            get;
            set;
        }
        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                //If the UI tells us to expand...
                if (value == true)
                    // Find all children
                    Expand();
                // If the UI tells us to close
                else
                    this.ClearChildren();
            }
        }
        #endregion

        #region Public commands
        /// <summary>
        /// The command to this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Contructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            //Setup the children as nedeed
            this.ClearChildren();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Remnoves all children from the list, addind a dummy item to show the expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            //Clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
            {

            }
        }
        #endregion

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;

            // Find all children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                            children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type)));
        }
    }
}
