using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using BLL;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InputData _inputData;
        Database dat;
        ManualEnter man;
        Approximation_Calculation _calc;
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             man = new ManualEnter( out _inputData);
            if(man.ShowDialog() == true)
            {
                firstCalc();
                CBError.Text = "";
            }
        }
        public void firstCalc()
        {
            _inputData.COEF = new float[_inputData.DEGREE + 1];
            _calc = new Approximation_Calculation();
            _calc.Calculation(_inputData.DEGREE, _inputData.NOPTS, _inputData.X, _inputData.Y, _inputData.COEF);
            
            Info.Text=string.Format("{0}", "ORDER    COEFFICIENT\n");
            if (_inputData.NOPTS > 0)
            {
                for (int i = 0; i < _inputData.DEGREE + 1; i++)
                {
                    Info.Text += string.Format("{0}      {1}, {2}", i, _inputData.COEF[i], '\n');
                }
                Info.Text += string.Format("{0}", "Y Values\n");
                for (int i = 0; i < _inputData.NOPTS; i++)
                {
                    Info.Text += string.Format("{0}     {1}, {2}", i+1, _inputData.Y[i], '\n');
                }

            }

        }

        private void CB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (_inputData.COEF.Length > 0)
                {
                    float X = (float)Convert.ToDouble(TextBoxCB.Text);
                    float Y = Approximation_Calculation.CalculateHRV(X, _inputData.COEF);
                    ResultHRV.Text = Convert.ToString(Y);
                    CBError.Text = "";
                }
            }
            catch
            {
                CBError.Text = "Calculate coeficients first";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             dat = new Database(out _inputData);
            if (dat.ShowDialog() == true)
            {
                firstCalc();
                CBError.Text = "";
            }
        }
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            ConnectDB _connect = new ConnectDB();
            _connect.AddToDB((float)Convert.ToDouble(TextBoxCB.Text), (float)Convert.ToDouble(ResultHRV.Text));
        }
    }
}
