using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMLMenuWriter
{
    class XMLMenuWriter
    {
        static void Main(string[] args)
        {
            //if (args.Length != 2)
            //{
            //    Console.WriteLine("Invalid call! Please include menu xml file and active path.");

            //}
            string testData = @"<menu>
	                            <item>
		                            <displayName>Home</displayName>
		                            <path value=""/Default.aspx""/>
	                            </item>
	                            <item>
		                            <displayName>Trips</displayName>
		                            <path value=""/Requests/Quotes/CreateQuote.aspx""/>
		                            <subMenu>
			                            <item>
				                            <displayName>Create Quote</displayName>
				                            <path value=""/Requests/Quotes/CreateQuote.aspx""/>
			                            </item>
			                            <item>
				                            <displayName>Open Quotes</displayName>
				                            <path value=""/Requests/OpenQuotes.aspx""/>
			                            </item>
			                            <item superOverride=""true"">
				                            <displayName>Scheduled Trips</displayName>
				                            <path value=""/Requests/Trips/ScheduledTrips.aspx""/>
			                            </item>
		                            </subMenu>
	                            </item>
	                            <item>
		                            <displayName>Company</displayName>
		                            <path value=""/mvc/company/view"" />
		                            <subMenu>
			                            <item>
				                            <displayName>Customers</displayName>
				                            <path value=""/customers/customers.aspx""/>
			                            </item>
			                            <item>
				                            <displayName>Pilots</displayName>
				                            <path value=""/pilots/pilots.aspx""/>
			                            </item>
			                            <item>
				                            <displayName>Aircraft</displayName>
				                            <path value=""/aircraft/Aircraft.aspx""/>
			                            </item>
		                            </subMenu>
	                            </item>
                            </menu>";
            //var xmlReader = XmlReader.Create(args[0]);

            var serializer = new XmlSerializer(typeof(Menu));
            using (var reader = new StringReader(testData))
            {
                Menu result = (Menu)serializer.Deserialize(reader);
            }

        }
    }

    [XmlRoot("menu")]
    public class Menu
    {
        [XmlElement("item")]
        public List<Item> Items { get; set; }
    }

    public class SubMenu
    {
        [XmlElement("item")]
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        [XmlElement("displayName")]
        public string Name { get; set; }

        [XmlElement("path")]
        public string Path { get; set; }

        [XmlElement("submenu")]
        public SubMenu SubMenu { get; set; }
    }

}
