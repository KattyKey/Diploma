using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dl
{
    public class WorkWithDB
    {
        string connectionString = "Data Source=WIN-G54R6C1QK3D;Initial Catalog=CalcResult;Integrated Security=True";

        public DataTable Select(String selectSQL, int iX, int iy) // функция подключения к базе данных и обработка запросов
        {
          
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении                                                                        
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом

            return dataTable;            
        }
        public void Insert(string TextOfCommand)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();                                           // открываем базу данных

            SqlCommand updateCommand = new SqlCommand(TextOfCommand, sqlConnection);

            updateCommand.ExecuteNonQuery();
        }
    }
}
