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
        public Dictionary<string, IGrouping<string, XElement>> doc;

        public MainWindow()
        {
            InitializeComponent();
                        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            doc = XDocument
                .Load(@"C:\Users\AWR03011\source\repos\WpfApp1\101620_MOPLogs.mtl.xml")
                .Root.Descendants("LogEntry").Descendants("RawMOS")
                .Select(x => XDocument.Parse(x.Value).Root)
                .GroupBy(x => x.Elements().ElementAt(2).Element("roID").Value)
                .ToDictionary(
                    x => x.Key,
                    x => x
                );
            
            // Adding roID inside cboroId
            doc.Keys.ToList().ForEach(x => cbroId.Items.Add(x));
                        
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var choice = cbroId.SelectedItem.ToString();

            var tosave = new XDocument(new XElement("RawMOS"));

            doc.Values.ToList().Where(x => x.Key == choice)
                .ToList().ForEach(x => tosave.Root.Add(x));

            tosave.Save(@"C:\Users\AWR03011\source\repos\WpfApp1\MosList.xml");
        }

    }
}
