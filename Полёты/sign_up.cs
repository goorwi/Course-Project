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

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string quaryString = $"insert into register(login_user, password_user) values('{log}', '{pass}')";

                SqlCommand command = new SqlCommand(quaryString, dataBase.GetConnection());

                dataBase.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно зарегистрирован!");
                    StartMenu s = new StartMenu();
                    this.Hide();
                    s.ShowDialog();
                }
                else MessageBox.Show("Аккаунт не создан!");

                dataBase.CloseConnection();
            }
        }

        private bool checkUser()
        {
            string log = login.Text;
            string pass = password.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string quaryString = $"select id_user, login_user, password_user from register where login_user = '{log}' and password_user = '{pass}'";

            SqlCommand command = new SqlCommand(quaryString, dataBase.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0) { MessageBox.Show("Пользователь уже существует!"); return false; }
            else return true;
        }
    }
}
