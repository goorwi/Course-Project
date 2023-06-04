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
    public partial class sign_up : Form
    {
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
        }

        private void sign_up_Load(object sender, EventArgs e)
        {
            login.TextAlign = HorizontalAlignment.Right;
            password.TextAlign = HorizontalAlignment.Right;
            create_button.Location = new Point(this.ClientSize.Width / 2 - create_button.Width / 2, create_button.Location.Y);
            password.PasswordChar = '*';
            login.MaxLength = 50;
            password.MaxLength = 50;
            
        }

        private void create_button_Click(object sender, EventArgs e)
        {
            if (checkUser())
            {
                string log = login.Text;
                string pass = password.Text;

                try
                {
                    dataBase.CreateNewUser(log, pass);

                    MessageBox.Show("Аккаунт успешно зарегистрирован!");
                    StartMenu s = new StartMenu();
                    this.Hide();
                    s.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Аккаунт не создан!\n" + ex.Message);
                }
            }
        }

        private bool checkUser()
        {
            string log = login.Text;
            string pass = password.Text;

            DataTable table = dataBase.GetUsers(log, pass);

            if (table.Rows.Count > 0) { MessageBox.Show("Пользователь уже существует!"); return false; }
            else return true;
        }

        private void sign_up_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
