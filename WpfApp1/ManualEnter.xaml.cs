using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BLL;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 
    public partial class ManualEnter : Window
    {
        private InputData _inputData;
        public ManualEnter(out InputData inputData)
        {
            InitializeComponent();
            _inputData = new InputData();
            inputData = _inputData;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void Degree_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextDegreeError.Text = "";
        }
        private void Number_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextNumberError.Text = "";
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (Convert.ToInt32(TextBoxDegree.Text) > 1 && Convert.ToInt32(TextBoxDegree.Text) < 10)
                {
                    _inputData.DEGREE = Convert.ToInt32(TextBoxDegree.Text);
                }
                else
                {
                    TextBoxDegree.Text = "";
                    TextDegreeError.Text = "Value should be grater then 1 and less then 10";
                }
                if (Convert.ToInt32(TextBoxNumberElements.Text) > 0)
                {
                    _inputData.NOPTS = Convert.ToInt32(TextBoxNumberElements.Text);
                    _inputData.X = new float[_inputData.NOPTS];
                    _inputData.Y = new float[_inputData.NOPTS];
                    string[] splitX = TextBlockXValues.Text.Split('\n');
                    string[] splitY = TextBlockYValues.Text.Split('\n');
                    if (splitX.Length == _inputData.NOPTS)
                    {
                        for (int i = 0; i < _inputData.NOPTS; i++)
                        {
                            if (splitX[i].Contains(','))
                            {
                                splitX[i].Replace(',', '.');
                            }
                            _inputData.X[i] = (float)Convert.ToDouble(splitX[i]);
                        }
                        if (splitY.Length == _inputData.NOPTS)
                        {
                            for (int i = 0; i < _inputData.NOPTS; i++)
                            {
                                if (splitY[i].Contains(','))
                                {
                                    splitY[i].Replace(',', '.');
                                }
                                _inputData.Y[i] = (float)Convert.ToDouble(splitY[i]);
                            }

                            //MainWindow.firstCalc(_inputData.DEGREE, _inputData.NOPTS, _inputData.X, _inputData.Y);
                            DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            TextYError.Text = "Number of inputed values not correspond to the value of elements";
                        }
                    }
                    else
                    {
                        TextXError.Text = "Number of inputed values not correspond to the value of elements";
                    }
                }
                else
                {
                    TextBoxNumberElements.Text = "";
                    TextNumberError.Text = "Value should be grater then 0";
                }
            }
            catch
            {
                MessageBox.Show("You entered some data in wrong format, all inputed values should be digits");
                //TextBoxNumberElements.Text = "";
                // TextBoxDegree.Text = "";
            }
        }

        private void TextBlockXValues_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextXError.Text = "";
        }

        private void TextBlockYValues_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextYError.Text = "";
        }
    }
}
