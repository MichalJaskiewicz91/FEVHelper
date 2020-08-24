using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEVHelper.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Open file dialog and reruen filepath
        /// </summary>
        /// <returns></returns>
        public static string SelectCSVFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Please select a CSV file with quantities"
            };
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileName;
            else return String.Empty;


        }
    }
}
