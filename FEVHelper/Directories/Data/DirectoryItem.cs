using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEVHelper.Directories.Data
{
    /// <summary>
    /// Information aboute directory item such a drive, a file a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; } 
        /// <summary>
        /// Absolute path to this item
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name {  get { return  this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}
