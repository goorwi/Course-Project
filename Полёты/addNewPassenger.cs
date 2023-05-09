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
    public partial class addNewPassenger : Form
    {
        DataBase dataBase = new DataBase();
        public addNewPassenger()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            dataBase.OpenConnection();

            var name = nameTextBox.Text;

            var addQuery = $"insert into Passenger (name) values('{name}')";
            var command = new SqlCommand(addQuery, dataBase.GetConnection());

            command.ExecuteNonQuery();

            MessageBox.Show("Запись успешно добавлена!");

            dataBase.CloseConnection();
        }
    }
}
