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

            DataTable table = dataBase.GetUsers(log, pass);
            try
            {
                if (table.Rows.Count == 1)
                {
                    var user = new checkUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));

                    MessageBox.Show("Успешная авторизация!");
                    DB dB = new DB(user);
                    this.Hide();
                    dB.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void newUser_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign_up su = new sign_up();
            this.Hide();
            su.ShowDialog();
            
        }
        private void StartMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
