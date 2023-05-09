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
    public partial class StartMenu : Form
    {
        DataBase dataBase = new DataBase();
        
        public StartMenu()
        {
            InitializeComponent();

        }

        private void StartMenu_Load(object sender, EventArgs e)
        {
            login.TextAlign = HorizontalAlignment.Right;
            password.TextAlign = HorizontalAlignment.Right;
            getConnect_button.Location = new Point(this.ClientSize.Width / 2 - getConnect_button.Width / 2, getConnect_button.Location.Y);
            password.PasswordChar = '*';
            login.MaxLength = 50;
            password.MaxLength = 50;
            newUser_link.Location = new Point(this.ClientSize.Width / 2 - newUser_link.Width / 2, newUser_link.Location.Y);
        }

        private void getConnect_button_Click(object sender, EventArgs e)
        {
            string log = login.Text;
            string pass = password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string quaryString = $"select id_user, login_user, password_user from register where login_user = '{log}' and password_user = '{pass}'";

            SqlCommand command = new SqlCommand(quaryString, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1) 
            {
                MessageBox.Show("Успешная авторизация!");
                DB dB = new DB();
                this.Hide();
                dB.ShowDialog();
            }
            else { MessageBox.Show("Произошла ошибка!"); }

            dataBase.GetConnection();
        }

        private void newUser_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign_up su = new sign_up();
            this.Hide();
            su.ShowDialog();
        }
    }
}
