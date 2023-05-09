using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Полёты
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }

    public partial class DB : Form
    {
        DataBase dataBase = new DataBase();
        int selecterRow;
        public DB()
        {
            InitializeComponent();
        }

        private void DB_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(passengersDGW);
        }

        private void CreateColumns()
        {
            passengersDGW.Columns.Add("ID_psg", "ID");
            passengersDGW.Columns.Add("name", "Имя");
            passengersDGW.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();

            string quaryString = $"select * from Passenger";

            SqlCommand command = new SqlCommand(quaryString, dataBase.GetConnection());

            dataBase.OpenConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();

        }

        private void passengersDGW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selecterRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = passengersDGW.Rows[selecterRow];

                idTextBox_pass.Text = row.Cells[0].Value.ToString();
                nameTextBox_pass.Text = row.Cells[1].Value.ToString();
            }
        }

        private void createButton_pass_Click(object sender, EventArgs e)
        {
            addNewPassenger a = new addNewPassenger();
            a.Show();
        }

        private void refreshButton_pass_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(passengersDGW);
        }
    }
}
