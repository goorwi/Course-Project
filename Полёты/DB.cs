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
    public enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public enum CreateForm
    {
        Pass,
        Comp
    }

    public partial class DB : Form
    {
        private readonly checkUser _user;
        DataBase dataBase = new DataBase();
        int selecterRow;
        public DB(checkUser user)
        {
            _user = user;
            InitializeComponent();
        }

        private void isAdmin()
        {
            if (_user.Status != "Admin")
            {
                createButton_pass.Enabled = false;
                createComp.Enabled = false;
                createPitButton.Enabled = false;
                createTripButton.Enabled = false;
                deleteButton_pass.Enabled = false;
                deletePitButton.Enabled = false;
                deleteTripButton.Enabled = false;
                delCompButton.Enabled = false;
                changeButton_pass.Enabled = false;
                changeCompButton.Enabled = false;
                changePitButton.Enabled = false;
                changeTripButton.Enabled = false;
                saveButton_pass.Enabled = false;
                saveCompButton.Enabled = false;
                savePitButton.Enabled = false;
                saveTripButton.Enabled = false;
            }
        }

        private void DB_Load(object sender, EventArgs e)
        {
            CreateColumns_pass();
            CreateColumns_comp();
            CreateColumns_trip();
            CreateColumns_pit();
            RefreshDataGrid_pass(passengersDGW);
            RefreshDataGrid_comp(companyDGV);
            RefreshDataGrid_trip(tripDGV);
            RefreshDataGrid_pit(pitDGV);
            statusUserLabel.Text += $" {_user.Status}";
            isAdmin();
            
        }

        //Passenger Tab
        #region
        private void CreateColumns_pass()
        {
            passengersDGW.Columns.Add("ID_psg", "ID");
            passengersDGW.Columns.Add("name", "Имя");
            passengersDGW.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow_pass(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1).Trim(), RowState.ModifiedNew);
        }
        private void RefreshDataGrid_pass(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {
                dataBase.OpenConnection();
                SqlDataReader reader = dataBase.ReadPassengers("");

                while (reader.Read())
                {
                    ReadSingleRow_pass(dgv, reader);
                }
                reader.Close();
                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            dgv.Columns[2].Visible = false;
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
            addNewPassenger a = new addNewPassenger(CreateForm.Pass);
            a.Show();
        }
        private void refreshButton_pass_Click(object sender, EventArgs e)
        {
            RefreshDataGrid_pass(passengersDGW);
            clearFields_pass();
        }
        private void Search_pass(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {
                dataBase.OpenConnection();

                SqlDataReader read = dataBase.ReadPassengers(searchListBox.Text);
                while (read.Read())
                {
                    ReadSingleRow_pass(dgv, read);
                }

                read.Close();
                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void searchListBox_TextChanged(object sender, EventArgs e)
        {
            Search_pass(passengersDGW);
        }
        private void deleteRow_pass()
        {
            int index = passengersDGW.CurrentCell.RowIndex;

            passengersDGW.Rows[index].Visible = false;

            if (passengersDGW.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                passengersDGW.Rows[index].Cells[2].Value = RowState.Deleted;
            }
        }
        private void deleteButton_pass_Click(object sender, EventArgs e)
        {
            deleteRow_pass();
            clearFields_pass();
        }
        private void update_pass()
        {
            for (int index = 0; index < passengersDGW.Rows.Count; index++)
            {
                var rowState = (RowState)passengersDGW.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(passengersDGW.Rows[index].Cells[0].Value);

                    try
                    {
                        dataBase.UpdatePass(id.ToString(), "", rowState);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (rowState == RowState.Modified)
                {
                    try
                    {
                        var id = passengersDGW.Rows[index].Cells[0].Value.ToString();
                        var name = passengersDGW.Rows[index].Cells[1].Value.ToString();

                        dataBase.UpdatePass(id, name, rowState);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void saveButton_pass_Click(object sender, EventArgs e)
        {
            update_pass();
        }
        private void change_pass()
        {
            var selectedRowIndex = passengersDGW.CurrentCell.RowIndex;

            var name = nameTextBox_pass.Text.Trim();

            if (passengersDGW.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                passengersDGW.Rows[selectedRowIndex].SetValues(idTextBox_pass.Text, name);
                passengersDGW.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
            }
        }
        private void changeButton_pass_Click(object sender, EventArgs e)
        {
            change_pass();
            clearFields_pass();
        }
        private void clearFields_pass()
        {
            idTextBox_pass.Text = "";
            nameTextBox_pass.Text = "";
        }
        private void clearAllButton_pass_Click(object sender, EventArgs e)
        {
            clearFields_pass();
        }
        private void nameTextBox_pass_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nameTextBox_pass.Text.Length; i++)
                if (char.IsDigit(nameTextBox_pass.Text[i]))
                    nameTextBox_pass.Text = nameTextBox_pass.Text.Remove(i,1);

        }
        #endregion

        //Company Tab
        #region
        private void CreateColumns_comp()
        {
            companyDGV.Columns.Add("ID_comp", "ID");
            companyDGV.Columns.Add("name", "Имя");
            companyDGV.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow_comp(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1).Trim(), RowState.ModifiedNew);
        }
        private void RefreshDataGrid_comp(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {
                dataBase.OpenConnection();

                SqlDataReader reader = dataBase.ReadCompanies("");

                while (reader.Read())
                {
                    ReadSingleRow_comp(dgv, reader);
                }
                reader.Close();

                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            dgv.Columns[2].Visible = false;
        }
        private void companyDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selecterRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = companyDGV.Rows[selecterRow];

                idCompTextBox.Text = row.Cells[0].Value.ToString();
                nameCompTextBox.Text = row.Cells[1].Value.ToString();
            }
        }
        private void createComp_Click(object sender, EventArgs e)
        {
            addNewPassenger a = new addNewPassenger(CreateForm.Comp);
            a.Show();
        }
        private void refreshCompButton_Click(object sender, EventArgs e)
        {
            RefreshDataGrid_comp(companyDGV);
            clearFields_comp();
        }
        private void Search_comp(DataGridView dgv)
        {
            dgv.Rows.Clear();
            try
            {
                dataBase.OpenConnection();

                SqlDataReader read = dataBase.ReadCompanies(searchListBox_comp.Text);
                while (read.Read())
                {
                    ReadSingleRow_comp(dgv, read);
                }
                read.Close();

                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void searchListBox_comp_TextChanged(object sender, EventArgs e)
        {
            Search_comp(companyDGV);
        }
        private void deleteRow_comp()
        {
            int index = companyDGV.CurrentCell.RowIndex;

            companyDGV.Rows[index].Visible = false;

            if (companyDGV.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                companyDGV.Rows[index].Cells[2].Value = RowState.Deleted;
            }
        }
        private void delCompButton_Click(object sender, EventArgs e)
        {
            deleteRow_comp();
            clearFields_comp();
        }
        private void update_comp()
        {
            for (int index = 0; index < companyDGV.Rows.Count; index++)
            {
                var rowState = (RowState)companyDGV.Rows[index].Cells[2].Value;

                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    try
                    {
                        var id = Convert.ToInt32(companyDGV.Rows[index].Cells[0].Value);

                        dataBase.UpdateComp(id.ToString(), "", rowState);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
                if (rowState == RowState.Modified)
                {
                    try
                    {
                        var id = companyDGV.Rows[index].Cells[0].Value.ToString();
                        var name = companyDGV.Rows[index].Cells[1].Value.ToString();

                        dataBase.UpdateComp(id, name, rowState);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    

                }
            }
        }
        private void saveCompButton_Click(object sender, EventArgs e)
        {
            update_comp();
        }
        private void change_comp()
        {
            var selectedRowIndex = companyDGV.CurrentCell.RowIndex;

            var name = nameCompTextBox.Text.Trim();

            if (companyDGV.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                companyDGV.Rows[selectedRowIndex].SetValues(idCompTextBox.Text, name);
                companyDGV.Rows[selectedRowIndex].Cells[2].Value = RowState.Modified;
            }
        }
        private void changeCompButton_Click(object sender, EventArgs e)
        {
            change_comp();
            clearFields_comp();
        }
        private void clearFields_comp()
        {
            idCompTextBox.Text = "";
            nameCompTextBox.Text = "";
        }
        private void clearCompButton_Click(object sender, EventArgs e)
        {
            clearFields_comp();
        }
        private void nameCompTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nameTextBox_pass.Text.Length; i++)
                if (char.IsDigit(nameTextBox_pass.Text[i]))
                    nameTextBox_pass.Text = nameTextBox_pass.Text.Remove(i, 1);
        }
        #endregion

        //Trip Tab
        #region
        private void CreateColumns_trip()
        {
            tripDGV.Columns.Add("trip_no", "Номер рейса");
            tripDGV.Columns.Add("ID_comp", "ID компании");
            tripDGV.Columns.Add("plane", "Самолёт");
            tripDGV.Columns.Add("town_from", "Город отправления");
            tripDGV.Columns.Add("town_to", "Город прибытия");
            tripDGV.Columns.Add("time_out", "Время отправления");
            tripDGV.Columns.Add("time_in", "Время прибытия");
            tripDGV.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow_trip(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetInt32(1), record.GetString(2).Trim(), record.GetString(3).Trim(), record.GetString(4).Trim(), record.GetDateTime(5), record.GetDateTime(6), RowState.ModifiedNew);
        }
        private void RefreshDataGrid_trip(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {
                dataBase.OpenConnection();

                SqlDataReader reader = dataBase.ReadTrips("");

                while (reader.Read())
                {
                    ReadSingleRow_trip(dgv, reader);
                }
                reader.Close();
                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            dgv.Columns[7].Visible = false;
        }
        private void tripDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selecterRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = tripDGV.Rows[selecterRow];

                tripNoTextBox.Text = row.Cells[0].Value.ToString();
                nameCompanyTextBox.Text = row.Cells[1].Value.ToString();
                namePlaneTextBox.Text = row.Cells[2].Value.ToString();
                townFromTextBox.Text = row.Cells[3].Value.ToString();
                townToTextBox.Text = row.Cells[4].Value.ToString();
                timeOutTextBox.Text = row.Cells[5].Value.ToString();
                timeInTextBox.Text = row.Cells[6].Value.ToString();
            }
        }
        private void createTrip_Click(object sender, EventArgs e)
        {
            addNewTrip a = new addNewTrip();
            a.Show();
        }
        private void refreshTripButton_Click(object sender, EventArgs e)
        {
            RefreshDataGrid_trip(tripDGV);
            clearFields_trip();
        }
        private void Search_trip(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {   
                dataBase.OpenConnection();

                SqlDataReader read = dataBase.ReadTrips(searchTripTextBox.Text);
                while (read.Read())
                {
                    ReadSingleRow_trip(dgv, read);
                } 
                read.Close();
                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

          
        }
        private void searchTripTextBox_TextChanged(object sender, EventArgs e)
        {
            Search_trip(tripDGV);
        }
        private void deleteRow_trip()
        {
            int index = tripDGV.CurrentCell.RowIndex;

            tripDGV.Rows[index].Visible = false;

            if (tripDGV.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                tripDGV.Rows[index].Cells[7].Value = RowState.Deleted;
            }
        }
        private void deleteTripButton_Click(object sender, EventArgs e)
        {
            deleteRow_trip();
            clearFields_trip();
        }
        private void update_trip()
        {
            for (int index = 0; index < tripDGV.Rows.Count; index++)
            {
                var rowState = (RowState)tripDGV.Rows[index].Cells[7].Value;

                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    try
                    {
                        var id = Convert.ToInt32(tripDGV.Rows[index].Cells[0].Value);

                        dataBase.UpdateTrip(id.ToString(), "", "", "", "", "", "", rowState);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
                if (rowState == RowState.Modified)
                {
                    try
                    {
                        var num = tripDGV.Rows[index].Cells[0].Value.ToString();
                        var id = tripDGV.Rows[index].Cells[1].Value.ToString();
                        var plane = tripDGV.Rows[index].Cells[2].Value.ToString();
                        var from = tripDGV.Rows[index].Cells[3].Value.ToString();
                        var to = tripDGV.Rows[index].Cells[4].Value.ToString();
                        var timeO = tripDGV.Rows[index].Cells[5].Value.ToString();
                        var timeI = tripDGV.Rows[index].Cells[6].Value.ToString();

                        dataBase.UpdateTrip(id, num, plane, from, to, timeO, timeI, rowState);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    
                }
            }
        }
        private void saveTripButton_Click(object sender, EventArgs e)
        {
            update_trip();
        }
        private void change_trip()
        {
            var selectedRowIndex = tripDGV.CurrentCell.RowIndex;

            var id = idCompTextBox.Text.Trim();
            var plane = namePlaneTextBox.Text.Trim();
            var from = townFromTextBox.Text.Trim();
            var to = townToTextBox.Text.Trim();
            var timeO = timeOutTextBox.Text;
            var timeI = timeInTextBox.Text;

            if (tripDGV.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                DateTime timeOut = new DateTime(), timeIn = new DateTime();
                if (DateTime.TryParse(timeO, out timeOut) && DateTime.TryParse(timeI, out timeIn))
                {
                    tripDGV.Rows[selectedRowIndex].SetValues(idTextBox_pass.Text, id, plane, from, to, timeOut, timeIn);
                    tripDGV.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;
                }
                else MessageBox.Show("Введите корректные значения дат!");
                
            }
        }
        private void changeTripButton_Click(object sender, EventArgs e)
        {
            change_trip();
            clearFields_trip();
        }
        private void clearFields_trip()
        {
            tripNoTextBox.Text = "";
            idCompTextBox.Text = "";
            namePlaneTextBox.Text = "";
            townFromTextBox.Text = "";
            townToTextBox.Text = "";
            timeOutTextBox.Text = "";
            timeInTextBox.Text = "";
        }
        private void clearTripButton_Click(object sender, EventArgs e)
        {
            clearFields_trip();
        }
        private void nameCompanyTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nameTextBox_pass.Text.Length; i++)
                if (char.IsLetter(nameTextBox_pass.Text[i]))
                    nameTextBox_pass.Text = nameTextBox_pass.Text.Remove(i, 1);
        }
        private void townFromTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nameTextBox_pass.Text.Length; i++)
                if (char.IsDigit(nameTextBox_pass.Text[i]))
                    nameTextBox_pass.Text = nameTextBox_pass.Text.Remove(i, 1);
        }
        private void DB_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void townToTextBox_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < nameTextBox_pass.Text.Length; i++)
                if (char.IsDigit(nameTextBox_pass.Text[i]))
                    nameTextBox_pass.Text = nameTextBox_pass.Text.Remove(i, 1);
        }
        #endregion

        //Passengers in Trip
        #region
        private void CreateColumns_pit()
        {
            pitDGV.Columns.Add("trip_no", "Номер рейса");
            pitDGV.Columns.Add("date", "Дата рейса");
            pitDGV.Columns.Add("ID_psg", "ID пассажира");
            pitDGV.Columns.Add("place", "Место в салоне");
            pitDGV.Columns.Add("IsNew", String.Empty);
        }
        private void ReadSingleRow_pit(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetDateTime(1), record.GetInt32(2), record.GetString(3).Trim(), RowState.ModifiedNew);
        }
        private void RefreshDataGrid_pit(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {
                dataBase.OpenConnection();

                SqlDataReader reader = dataBase.ReadPiT("");

                while (reader.Read())
                {
                    ReadSingleRow_pit(dgv, reader);
                }
                reader.Close();
                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            dgv.Columns[4].Visible = false;
        }
        private void pitDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selecterRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = pitDGV.Rows[selecterRow];

                tripNoPitTextBox.Text = row.Cells[0].Value.ToString();
                dataPitTextBox.Text = row.Cells[1].Value.ToString();
                idPassPitTextBox.Text = row.Cells[2].Value.ToString();
                placePitTextBox.Text = row.Cells[3].Value.ToString();
            }
        }
        private void createPitButton_Click(object sender, EventArgs e)
        {
            addNewPit a = new addNewPit();
            a.Show();
        }
        private void refreshPitButton_Click(object sender, EventArgs e)
        {
            RefreshDataGrid_pit(pitDGV);
            clearFields_pit();
        }
        private void Search_pit(DataGridView dgv)
        {
            dgv.Rows.Clear();

            try
            {
                dataBase.OpenConnection();

                SqlDataReader read = dataBase.ReadPiT(searchPitTextBox.Text);
                while (read.Read())
                {
                    ReadSingleRow_pit(dgv, read);
                }

                read.Close();
                dataBase.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void searchPitTextBox_TextChanged(object sender, EventArgs e)
        {
            Search_pit(pitDGV);
        }
        private void deleteRow_pit()
        {
            int index = pitDGV.CurrentCell.RowIndex;

            pitDGV.Rows[index].Visible = false;

            if (pitDGV.Rows[index].Cells[0].Value.ToString() != string.Empty)
            {
                pitDGV.Rows[index].Cells[4].Value = RowState.Deleted;
            }
        }
        private void deletePitButton_Click(object sender, EventArgs e)
        {
            deleteRow_pit();
            clearFields_pit();
        }
        private void update_pit()
        {
            for (int index = 0; index < pitDGV.Rows.Count; index++)
            {
                var rowState = (RowState)pitDGV.Rows[index].Cells[4].Value;

                if (rowState == RowState.Existed) continue;
                if (rowState == RowState.Deleted)
                {
                    try
                    {
                        var num = pitDGV.Rows[index].Cells[0].Value.ToString();
                        var date = pitDGV.Rows[index].Cells[1].Value.ToString();
                        var id = pitDGV.Rows[index].Cells[3].Value.ToString();

                        dataBase.UpdatePiT(id, num, date, rowState, "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
                if (rowState == RowState.Modified)
                {
                    try
                    {
                        var num = pitDGV.Rows[index].Cells[0].Value.ToString();
                        var date = pitDGV.Rows[index].Cells[1].Value.ToString();
                        var id = pitDGV.Rows[index].Cells[3].Value.ToString();
                        var place = pitDGV.Rows[index].Cells[4].Value.ToString();

                        dataBase.UpdatePiT(id, num, date, rowState, place);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Ошибка преобразования")) ;
                        else MessageBox.Show(ex.Message);
                    }

                }
            }
        }
        private void savePitButton_Click(object sender, EventArgs e)
        {
            update_pit();
        }
        private void change_pit()
        {
            var selectedRowIndex = pitDGV.CurrentCell.RowIndex;

            var num = tripNoPitTextBox.Text.Trim();
            var newDate = dataPitTextBox.Text;
            var id = idPassPitTextBox.Text.Trim();
            var newPlace = placePitTextBox.Text.Trim();
            DateTime data = new DateTime();

            if (pitDGV.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (System.Text.RegularExpressions.Regex.Match(newPlace, @"([0-9][a-z])") != null && DateTime.TryParse(newDate, out data))
                {
                    pitDGV.Rows[selectedRowIndex].SetValues(num, newDate, id, newPlace);
                    pitDGV.Rows[selectedRowIndex].Cells[4].Value = RowState.Modified;
                }
                else MessageBox.Show("Неккоректная запись места!");
            }
        }
        private void changePitButton_Click(object sender, EventArgs e)
        {
            change_pit();
            clearFields_pit();
        }
        private void clearFields_pit()
        {
            tripNoPitTextBox.Text = "";
            dataPitTextBox.Text = "";
            idPassPitTextBox.Text = "";
            placePitTextBox.Text = "";
        }
        private void clearPitButton_Click(object sender, EventArgs e)
        {
            clearFields_pit();
        }

        #endregion
    }
}
