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
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            var mosList = new Dictionary<XElement, XElement>();

            var doc = XDocument
                .Load(@"C:\Users\AWR03011\Desktop\Onboard Training\Repositories\csTraining1\101620_MOPLogs.mtl.xml")
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
                        ,
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
                ;

            

            doc.ForEach(x => x.roId.Value.ToList().Distinct());

            foreach (var lala in doc)
            {
                cbroId.Items.Add(lala);
            }
            /**
            foreach (var each in doc)
            {
                MessageBox.Show(each.ToString());

            }
            */

        }
    }
}
