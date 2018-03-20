using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XMLMenuWriter
{
    class XMLMenuWriter
    {
        private static string ActivePath { get; set; }

        /// <summary>
        /// Parses a given menu file and prints a structured menu to the console, including
        /// labeling paths that match the one supplied in the arguments as "active".
        /// </summary>
        /// <param name="args">
        /// @Required: args[0] contain the path of a valid .xml or xml-structured text file.
        /// @Required: args[1] contain the active menu option.</param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid function call! Please include menu xml file and active path.");
            }
            
            var serializer = new XmlSerializer(typeof(Menu));
            ActivePath = args[1];
            using (var reader = XmlReader.Create(args[0]))
            {
                var menu = (Menu)serializer.Deserialize(reader);
                RenderMenu(menu, 0);
            }
            Console.ReadKey();
        }

        static void RenderMenu(Menu menu, int indent)
        {
            foreach (var item in menu.Items)
            {
                Console.WriteLine(MenuLineItem(item, indent));
                if (item.SubMenu != null)
                {
                    RenderMenu(item.SubMenu, indent + 1);
                }
            }
        }

        static bool IsActive(Item item)
        {
            if (item.Path.Value == ActivePath)
            {
                return true;
            }
            var isSubMenuActive = false;
            if (item.SubMenu != null)
            {
                foreach (var subItem in item.SubMenu.Items)
                {
                    isSubMenuActive = IsActive(subItem);
                }
            }
            return isSubMenuActive;
        }

        static string MenuLineItem(Item item, int indent)
        {
            var stringBuilder = new StringBuilder();
            var indentString = stringBuilder.Append('\t', indent);

            return String.Format("{0}{1}, {2} {3}", indentString,
                                                    item.Name, 
                                                    item.Path.Value, 
                                                    IsActive(item) ? "ACTIVE" : String.Empty);
        }
    }

    #region Serialization Classes
    [XmlRoot("menu")]
    public class Menu
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("item")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [XmlElement("displayName")]
        public string Name { get; set; }

        [XmlElement("path")]
        public Path Path { get; set; }

        [XmlElement("subMenu")]
        public Menu SubMenu { get; set; }
    }

    public class Path
    {
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
    #endregion
}
