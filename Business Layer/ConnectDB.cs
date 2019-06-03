using System;
using System.Collections.Generic;
using System.Text;
using dl;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class ConnectDB
    {
        public void Create(String selectSQL, int iX, int iy, InputData _inputData) {

            DataTable _dataTable = new DataTable("dataBase");
            WorkWithDB work = new WorkWithDB();

            _dataTable = work.Select(selectSQL, 0, 1);
            _inputData.NOPTS = _dataTable.Rows.Count;
            _inputData.X = new float[_inputData.NOPTS];
            _inputData.Y = new float[_inputData.NOPTS];
            
            for (int i = 0; i < _dataTable.Rows.Count; i++)
            { // перебираем данные
                _inputData.X[i] = (float)Convert.ToDouble(_dataTable.Rows[i][iX]);
                _inputData.Y[i] = (float)Convert.ToDouble(_dataTable.Rows[i][iy]);
            }
        }
    }
}
