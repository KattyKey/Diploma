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
        string connectionString;
        SqlDataAdapter adapter;
        DataTable phonesTable;

        private InputData _inputData;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String selectSQL = "Select * FROM All_Data";
            ConnectDB _con = new ConnectDB();
            _con.Create(selectSQL, 0, 1, _inputData);
           
        }
        private void Button_Before_Click(object sender, RoutedEventArgs e)
        {
            String selectSQL = "Select * FROM Inputed_Values";
            ConnectDB _con = new ConnectDB();
            _con.Create(selectSQL, 2, 3, _inputData);
           // Select(selectSQL,2,3);
        }
        //public void Select(String selectSQL,int iX,int iy) // функция подключения к базе данных и обработка запросов
        //{
        // //   String selectSQL = "Select * FROM All_Data";
        //    DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
        //                                                                    // подключаемся к базе данных
        //    connectionString = "Data Source=WIN-G54R6C1QK3D;Initial Catalog=CalcResult;Integrated Security=True";
        //    SqlConnection sqlConnection = new SqlConnection(connectionString);
        //    sqlConnection.Open();                                           // открываем базу данных
        //    SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
        //    sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
        //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
        //    sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом

        //    _inputData.NOPTS = dataTable.Rows.Count;
        //    _inputData.X = new float[_inputData.NOPTS];
        //    _inputData.Y = new float[_inputData.NOPTS];

        //    for (int i = 0; i < dataTable.Rows.Count; i++) { // перебираем данные
        //        _inputData.X[i] = (float)Convert.ToDouble(dataTable.Rows[i][iX]);
        //        _inputData.Y[i] = (float)Convert.ToDouble(dataTable.Rows[i][iy]);
               
        //    }
           
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
