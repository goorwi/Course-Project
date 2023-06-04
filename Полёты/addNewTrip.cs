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
    public partial class addNewTrip : Form
    {
        DataBase dataBase = new DataBase();
        public addNewTrip()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            var tripNo = tripNoTextBox.Text;
            var idComp = idCompTextBox.Text;
            var plane = namePlaneTextBox.Text;
            var townFrom = townFromTextBox.Text;
            var townTo = townToTextBox.Text;
            var timeOut = timeOutTextBox.Text;
            var timeIn = timeInTextBox.Text;

            dataBase.CreateNewTrip(tripNo, idComp, plane, townFrom, townTo, timeOut, timeIn);
        }
        private void townFromTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < townFromTextBox.Text.Length; i++)
                if (char.IsDigit(townFromTextBox.Text[i]))
                    townFromTextBox.Text = townFromTextBox.Text.Remove(i, 1);
        }
        private void townToTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < townToTextBox.Text.Length; i++)
                if (char.IsDigit(townToTextBox.Text[i]))
                    townToTextBox.Text = townToTextBox.Text.Remove(i, 1);
        }
        private void idCompTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < idCompTextBox.Text.Length; i++)
                if (!char.IsDigit(idCompTextBox.Text[i]))
                    idCompTextBox.Text = idCompTextBox.Text.Remove(i, 1);
        }
        private void tripNoTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tripNoTextBox.Text.Length; i++)
                if (!char.IsDigit(tripNoTextBox.Text[i]))
                    tripNoTextBox.Text = tripNoTextBox.Text.Remove(i, 1);
        }
    }
}
