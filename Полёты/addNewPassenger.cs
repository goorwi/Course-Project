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
        CreateForm _cf;
        DataBase dataBase = new DataBase();
        public addNewPassenger(CreateForm cf)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _cf = cf;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            var table = "";
            var name = nameTextBox.Text;
            switch (_cf)
            {
                case CreateForm.Pass: table = "Passenger"; break;
                case CreateForm.Comp: table = "Company"; break;
            }
            dataBase.CreateNewPassenger(table, name, _cf);
        }
        private void addNewPassenger_Load(object sender, EventArgs e)
        {
            if (_cf == CreateForm.Comp)
                label1.Text = "Создание записи:\nКомпания";
        }
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nameTextBox.Text.Length; i++)
                if (char.IsDigit(nameTextBox.Text[i]))
                    nameTextBox.Text = nameTextBox.Text.Remove(i, 1);
        }
    }
}
