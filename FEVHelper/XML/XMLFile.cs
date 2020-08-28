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
    public class XMLFile
    {
        public static event EventHandler QuantitiesRead;
        protected static void OnQuantitiesRead()
        {
            QuantitiesRead?.Invoke(typeof(XMLFile), EventArgs.Empty);
        }

        public static List<Quantity> Read(List<string> list, string quantitiesPath)
        {
            List<IEnumerable<XElement>> descendatsList = new List<IEnumerable<XElement>>();
            Dictionary<string, string> idAndDescription = new Dictionary<string, string>();
            List<Quantity> listQuantity = new List<Quantity>();
            Quantity quantity = new Quantity();

            // Loading document
            var document = XDocument.Load(quantitiesPath);
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
                    ;
                }

            }
            // Publish an event
            OnQuantitiesRead();
            // Return list of quantity
            return listQuantity;

        }
    }
}
