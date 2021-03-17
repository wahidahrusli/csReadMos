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
        public MainWindow()
        {
            InitializeComponent();

            // find <roID, IList<MosMessage>>
            var doc = XDocument
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
                        .Element("roID"),
                    rawMos: x
                ))
                .ToList();
            /**
            .GroupBy(x => x.roId)
            .ToDictionary(
                x => x.Key,
                x => x.
                )
            */

            // add in combobox
            doc.Select(x => x.roId.Value)
                .Distinct()
                .ToList()
                .ForEach(x => cbroId.Items.Add(x));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
