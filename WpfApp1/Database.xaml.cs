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
using BLL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Database.xaml
    /// </summary>
    public partial class Database : Window
    {
       // string connectionString;
       // SqlDataAdapter adapter;
        //DataTable phonesTable;

        private InputData _inputData;
        ConnectDB _con;
        public Database(out InputData inputData)
        {
            InitializeComponent();
           // connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _inputData = new InputData();
            inputData = _inputData;
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
        private void Button_All_Click(object sender, RoutedEventArgs e)
        {
            String selectSQL = "Select * FROM All_Data";
             _con = new ConnectDB();
            _con.Create(selectSQL, 0, 1, _inputData);
           
        }
        private void Button_Before_Click(object sender, RoutedEventArgs e)
        {
            String selectSQL = "Select * FROM Inputed_Values";
            ConnectDB _con = new ConnectDB();
            _con.Create(selectSQL, 2, 3, _inputData);
           // Select(selectSQL,2,3);
        }
       
        private void Handle_All_Data_Inputted(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(DegreeDB.Text) > 1 && Convert.ToInt32(DegreeDB.Text) < 10)
                {
                    _inputData.DEGREE = Convert.ToInt32(DegreeDB.Text);
                }
                else
                {
                    DegreeDB.Text = "";
                    ErrorDegreeDB.Text = "Value should be grater then 1 and less then 10";
                }

                DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("You entered some data in wrong format, all inputed values should be digits");
                //TextBoxNumberElements.Text = "";
                // TextBoxDegree.Text = "";
            }
        }
    }
}
