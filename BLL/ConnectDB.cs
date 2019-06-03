using System;
using System.Collections.Generic;
using System.Text;
using dl;
using System.Data;

namespace BLL
{
    public class ConnectDB
    {
        WorkWithDB _work = new WorkWithDB();

        public void Create(String selectSQL, int iX, int iy, InputData _inputData) {

            DataTable dataTable = new DataTable();
           
            dataTable = _work.Select(selectSQL, iX, iy);
            
            _inputData.NOPTS = dataTable.Rows.Count;
            _inputData.X = new float[_inputData.NOPTS];
            _inputData.Y = new float[_inputData.NOPTS];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            { // перебираем данные
                _inputData.X[i] = (float)Convert.ToDouble(dataTable.Rows[i][iX]);
                _inputData.Y[i] = (float)Convert.ToDouble(dataTable.Rows[i][iy]);

            }
        }
        public void AddToDB(float CB, float HRV)
        {
           string TextOfCommand = "Insert Into Inputed_Values ( Date, Time,CB_input,HRV_calc) VALUES('" + DateTime.Now.ToString("MM/dd/yyyy") + "', '"
                    + DateTime.Now.ToString("hh:mm tt") + "', '" + CB  + "', ' " + HRV + " ') ";
            _work.Insert(TextOfCommand);
        }
    }
}
