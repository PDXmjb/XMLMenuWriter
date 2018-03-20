using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XMLMenuWriter
{
    class XMLMenuWriter
    {
        /// <summary>
        /// Stores the active path, used to determine which menu items should be marked "ACTIVE".
        /// </summary>
        private string ActivePath { get; set; }

        /// <summary>
        /// Stores the path of the XML file to be rendered.
        /// </summary>
        private string XMLPath { get; set; }

        /// <summary>
        /// Create a new XMLMenuWriter.
        /// </summary>
        /// <param name="xmlPath">Path of the XML file to be rendered</param>
        /// <param name="activePath">
        /// The active path, that determines which menu items should be marked active.
        /// </param>
        public XMLMenuWriter(string xmlPath, string activePath)
        {
            XMLPath = xmlPath;
            ActivePath = activePath;
        }

        /// <summary>
        /// Parses a given menu file and prints a structured menu to the console, including
        /// labeling paths that match the one supplied in the arguments as "active".
        /// </summary>
        /// <param name="args">
        /// <required>args[0] contain the path of a valid .xml or xml-structured text file.</required> 
        /// <required>args[1] contain the active menu option.</required>
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Invalid function call! Please include menu xml file and active path.");
            }
            else
            {
                var menuWriter = new XMLMenuWriter(args[0], args[1]);
                menuWriter.WriteMenu();
            }            
        }

        /// <summary>
        /// Deserializes the supplied XML file into a tiered structure and renders the individual
        /// items to the console with proper indendation.
        /// </summary>
        public void WriteMenu()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Menu));
                
                using (var reader = XmlReader.Create(XMLPath))
                {
                    var menu = (Menu)serializer.Deserialize(reader);
                    RenderMenu(menu, 0);
                }
            }
            // Normally I'd have multiple catch statements, if we needed to handle things differently
            // on a per-exception basis. In this case, I'll just warn the user what happened.
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                // Requiring a keypress.
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        #region Helper methods
        /// <summary>
        /// Recursively renders the menu's tiered structure to the console.
        /// </summary>
        /// <param name="menu">Menu to be rendered</param>
        /// <param name="indent">
        /// Depth of the current menu or submenu, changing the amount of indentation
        /// received.
        /// </param>
        private void RenderMenu(Menu menu, int indent)
        {
            foreach (var item in menu.Items)
            {
                Console.WriteLine(MenuLineItem(item, indent));

                // If a submenu exists, we need to render its items also, with greater indentation.
                if (item.SubMenu != null)
                {
                    RenderMenu(item.SubMenu, indent + 1);
                }
            }
        }

        /// <summary>
        /// Builds a single line of the menu with appropriate indentation.
        /// </summary>
        /// <param name="item">Menu line item being rendered</param>
        /// <param name="indent">Tabbed depth determined by depth of submenu, if applicable.</param>
        /// <returns></returns>
        private string MenuLineItem(Item item, int indent)
        {
            var stringBuilder = new StringBuilder();
            var indentString = stringBuilder.Append('\t', indent);

            return String.Format("{0}{1}, {2} {3}", indentString,
                                                    item.Name, 
                                                    item.Path.Value, 
                                                    IsActive(item) ? "ACTIVE" : String.Empty);
        }

        /// <summary>
        /// Recursively determines if the current menu item or any of its children
        /// are active, necessitating the top-level item being marked active as well.
        /// </summary>
        /// <param name="item">Current menu item being evaluated.</param>
        /// <returns>True if the menu item should appear as active, false otherwise.</returns>
        private bool IsActive(Item item)
        {
            // If the current item's path matches, we need to mark it active.
            if (item.Path.Value == ActivePath)
            {
                return true;
            }
            var isSubMenuActive = false;
            if (item.SubMenu != null)
            {
                foreach (var subItem in item.SubMenu.Items)
                {
                    // If ANY of the submenu items are active, we want this menu marked active as well.
                    if (IsActive(subItem))
                    {
                        isSubMenuActive = true;
                    }
                }
            }
            return isSubMenuActive;
        }
        #endregion
    }

    #region Deserialization Classes
    [XmlRoot("menu")]
    public class Menu
    {
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
