using FEVHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FEVHelper.XML
{
    public class XMLFile :IDisposable
    {

        public static List<Quantity> Read(List<string> list)
        {
            List<IEnumerable<XElement>> descendatsList = new List<IEnumerable<XElement>>();
            Dictionary<string, string> idAndDescription = new Dictionary<string, string>();
            List<Quantity> listQuantity = new List<Quantity>();
            Quantity quantity = new Quantity();

            string lol = list[0];
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
                descendatsList.Add(document.Descendants(list[i]));
            }
      
            for (int i = 0; i < descendatsList.Count; i++)
            {
                var filtredData = descendatsList[i].Where(c => ((string)c.Attribute("Parameter") == "true"))
                                              .Select(c => new
                                              {
                                                  ID = c.Attribute("ID"),
                                                  description = c.Attribute("Description")
                                              });
                foreach (var item in filtredData)
                {
                    idAndDescription.Add((string)item.ID, (string)item.description);
                }

                foreach (var item in filtredData)
                {
                    Quantity quan = new Quantity { ID = (string)item.ID, Description = (string)item.description };
                    listQuantity.Add(quan);
;               }

            }


            // Retrieving data from Quantity Measurements and saving to the List

            return listQuantity;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
