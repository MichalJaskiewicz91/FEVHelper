using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FEVHelper.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Static string value, used for comparing paths
        /// </summary>
        private static string xcfm = ".xcfm";
        /// <summary>
        /// Static string value, used for comparing paths
        /// </summary>
        private static string csv = ".csv";
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
        public static string SubstringTheXCFMString(string checkedString)
        {
            string substractedtring = checkedString.Substring(checkedString.Length - 5, 5);
            return substractedtring;
        }
        public static string SubstringTheCSVString(string checkedString)
        {
            string substractedtring = checkedString.Substring(checkedString.Length - 4, 4);
            return substractedtring;
        }

        /// <summary>
        /// Check if quantity path exists
        /// </summary>
        public static bool QuanttiesPathExists(string quantitiesPath)
        {
            // Check if quantities path exists
            if (quantitiesPath == null || quantitiesPath == "")
            {
                MessageBox.Show("Quantities Path does not exist!");
                return false;
            }
            // Check if proper path is given
            else if (quantitiesPath.Length < 6 || FileHelper.SubstringTheXCFMString(quantitiesPath) != xcfm)
            {
                MessageBox.Show("Given path is incorrect, please load .xcfm file!");
                return false;
            }
            else
                return true;
        }

        public static bool CSVPathExists(string csvPath)
        {
            // Check if csv file path is already chosen
            if (csvPath == null || csvPath == "")
            {
                MessageBox.Show("CSV save path is not chosen");
                return false;
            }
            // Check if proper path is given
            else if (csvPath.Length < 5 || FileHelper.SubstringTheCSVString(csvPath) != csv)
            {
                MessageBox.Show("Given path is incorrect, please load .csv file!");
                return false;
            }
            else
                return true;
        }

    }
}
