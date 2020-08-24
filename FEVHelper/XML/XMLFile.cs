using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FEVHelper.XML
{
    public class XMLFile
    {
        public Dictionary<string, string> Read(List<string> list)
        {
            List<IEnumerable<XElement>> descendatsList = new List<IEnumerable<XElement>>();
            Dictionary<string, string> idAndDescription = new Dictionary<string, string>();

            // Loading document
            var document = XDocument.Load(@"E:\Quantities_ADSM_2.xcfm");
            //try
            //{

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

            for (int i = 0; i < list.Count; i++)
            {
                descendatsList[i] = document.Descendants(list[i]);
            }

            // Define descedants QuantityMeasurement
            var quantityMeasurements = document.Descendants("QuantityMeasurement");

            // Define descedants QuantityString
            var quantityString = document.Descendants("QuantityString");

            // Define descedants QuantityConstant
            var quantityConstant = document.Descendants("QuantityConstant");

            // Shorter form searching Quantity Measurements
            var shortFormSearchingData = quantityMeasurements.Where(c => ((string)c.Attribute("Parameter") == "true"))
                                          .Select(c => new
                                          {
                                              ID = c.Attribute("ID"),
                                              description = c.Attribute("Description")
                                          });

            // Shorter form searching Quantity Constant
            var shortFormSearchingDataQuantityConstant = quantityConstant.Where(c => ((string)c.Attribute("Parameter") == "true"))
                                          .Select(c => new
                                          {
                                              ID = c.Attribute("ID"),
                                              description = c.Attribute("Description")
                                          });

            //Longer from searching QuantityString
            var longFormSearchedData = from item in quantityString
                                       where (string)item.Attribute("Parameter") == "true"
                                       select new
                                       {
                                           ID = item.Attribute("ID"),
                                           description = item.Attribute("Description")
                                       };

            // Retrieving data from Quantity Measurements and saving to the List
            foreach (var item in shortFormSearchingData)
            {
                idAndDescription.Add((string)item.ID, (string)item.description);
            }

            // Retrieving data from Quantity Constant and saving to the List
            foreach (var item in shortFormSearchingDataQuantityConstant)
            {
                idAndDescription.Add((string)item.ID, (string)item.description);
            }

            //Longer from searching QuantityString
            foreach (var item in longFormSearchedData)
            {
                idAndDescription.Add((string)item.ID, (string)item.description);
            }



            return idAndDescription;

        }
    }
}
