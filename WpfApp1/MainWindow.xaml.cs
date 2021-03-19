using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<(string, XElement)> xdoc;
        public Dictionary<string, IList<XElement>> doc;

        public MainWindow()
        {
            InitializeComponent();
                        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                       
            // find <roID, IList<MosMessage>>
            xdoc = XDocument
                .Load(@"C:\Users\AWR03011\source\repos\WpfApp1\101620_MOPLogs.mtl.xml")
                .Root
                .Descendants("LogEntry")
                .Descendants("RawMOS")
                .Select(x => (
                    roId: XDocument
                        .Parse(x.Value)
                        .Root
                        .Elements()
                        .ElementAt(2)
                        .Element("roID")
                        .Value,
                    rawMos: XDocument.Parse(x.Value).Root
                ))
                /**
                .GroupBy(x => x.roId)
                .ToDictionary(
                    x => x.Key,
                    x => x.Select(s => s.rawMos)
                )
                */
                .ToList()
                
                ;

            xdoc.GroupBy(x => x.Item1)
                .Select(x => x.First())
                .ToList()
                .ForEach(x => cbroId.Items.Add(x.Item1))
            ;

            
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var choice = cbroId.SelectedItem.ToString();

            var tosave = new XDocument(new XElement("RawMOS"));

            xdoc.Where(x => x.Item1 == choice)
                .Select(x => x.Item2)
                .ToList()
                .ForEach(x => tosave.Root.Add(x));  

            tosave.Save("Sample.xml");
        }

    }
}
