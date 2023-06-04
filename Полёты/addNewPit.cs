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
    public partial class addNewPit : Form
    {
        DataBase dataBase = new DataBase();
        public addNewPit()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            var num = nomTextBox.Text;
            var date = dataTextBox.Text;
            var id = idTextBox.Text;
            var place = placeTextBox.Text;

            dataBase.CreateNewPassInTrip(num, id, date, place);
        }
        private void nomTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nomTextBox.Text.Length; i++)
                if (!char.IsDigit(nomTextBox.Text[i]))
                    nomTextBox.Text = nomTextBox.Text.Remove(i, 1);
        }
        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < idTextBox.Text.Length; i++)
                if (!char.IsDigit(idTextBox.Text[i]))
                    idTextBox.Text = idTextBox.Text.Remove(i, 1);
        }
    }
}
