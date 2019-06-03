using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BL;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace DLL
{
    class WorkWithDB
    {
        string connectionString;
        private InputData _inputData;
        public void Select(String selectSQL, int iX, int iy) // функция подключения к базе данных и обработка запросов
        {
            
        //   String selectSQL = "Select * FROM All_Data";
        DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            connectionString = "Data Source=WIN-G54R6C1QK3D;Initial Catalog=CalcResult;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом

            _inputData.NOPTS = dataTable.Rows.Count;
            _inputData.X = new float[_inputData.NOPTS];
            _inputData.Y = new float[_inputData.NOPTS];

            for (int i = 0; i < dataTable.Rows.Count; i++)
            { // перебираем данные
                _inputData.X[i] = (float)Convert.ToDouble(dataTable.Rows[i][iX]);
                _inputData.Y[i] = (float)Convert.ToDouble(dataTable.Rows[i][iy]);

            }

        }

    }
}
