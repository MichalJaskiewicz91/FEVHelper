using FEVHelper.Models;
using FEVHelper.XML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace FEVHelper.CSV
{
    public class CSVFile
    {
        public static bool AutoOpenFile { get; set; }
        public CSVFile()
        {
            
        }
        public void OnQuantitiesRead(object sender, EventArgs e)
        {
            MessageBox.Show("Quantities read! this message is invoked by CSV FIle class");
        }
        public static void Save(List<Quantity> readQuantities, string chosenCSVFilePath)
        {
            // Create a content that will be saved inside the .csv file
            String joined = String.Join(Environment.NewLine, readQuantities.Select(p => $"{p.ID};{p.Description};"));

            // Check if file is already created
            if (!File.Exists(chosenCSVFilePath))
            {
                try
                {
                    using (FileStream fileStream = File.Create(chosenCSVFilePath))
                    {
                        MessageBox.Show("File properly created");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            // Try to write a content
            try
            {
                File.WriteAllText(chosenCSVFilePath, joined);
                MessageBox.Show("Quantities have been stored");
                if (AutoOpenFile == true)
                {
                    Process.Start(chosenCSVFilePath);
                }


            }
            catch (Exception)
            {
                // Handling exception
                MessageBox.Show("The chosen file curently is unavaiable");
            }

        }

    }
}
