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
using System.Windows.Shapes;
using BL;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
        private InputData _inputData;
        public Database(out InputData inputData)
        {
            InitializeComponent();
            _inputData = new InputData();
            inputData = _inputData;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
