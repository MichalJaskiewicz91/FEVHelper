using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEVHelper.Directories.Data
{
    public enum DirectoryItemType
    {
        /// <summary>
        /// A logical drive
        /// </summary>
        Drive,
        /// <summary>
        /// A physical file
        /// </summary>
        File,
        /// <summary>
        /// A folder
        /// </summary>
        Folder
    }
}
