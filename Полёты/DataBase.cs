using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;

namespace Полёты
{
    public class DataBase
    {
        SqlConnection sc = new SqlConnection(@"Data Source=ВЛАДИСЛАВ-ПК\SQLEXPRESS;Initial Catalog=aero;Integrated Security=True");
        public void OpenConnection()
        {
            if (sc.State == ConnectionState.Closed)
            {
                sc.Open();
            }
        }
        public void CloseConnection()
        {
            if (sc.State == ConnectionState.Open)
            {
                sc.Close();
            }
        }
        public SqlConnection GetConnection() => sc;

        public string QueryPassengerOrCompany(string table, string name) => (name == null) ? null : $"insert into {table} (name) values('{name}')";
        public void CreateNewPassenger(string table, string name, CreateForm cf)
        {
            OpenConnection();

            try
            {
                string Query = "";
                switch (cf)
                {
                    case CreateForm.Pass: Query = QueryPassengerOrCompany(table, name); break;
                    case CreateForm.Comp: Query = QueryPassengerOrCompany(table, name); break;
                }
                if (Query == null) throw new ArgumentNullException();

                if (ExecuteSqlCommandCreate(Query) == 1)
                    MessageBox.Show("Запись успешно добавлена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            CloseConnection();
        }
        public int ExecuteSqlCommandCreate(string Query)
        {
            var command = new SqlCommand(Query, GetConnection());

            return command.ExecuteNonQuery();
        }

        public string CheckIdFromTable(string table, string column, string id) => (table == null || column == null || id == null) ? null : $"select {column} from {table} where {column} = '{id}'";
        public string QueryPassInTrip(string num, string id, string date, string place) => 
            (num == null || id == null || date == null || place == null) ? null : $"insert into Pass_in_trip (trip_no, date, ID_psg, place) values('{num}', '{date}', '{id}', '{place}')";
        public bool checkNum(string tableName, string columun, string id)
        {
            string sQuery = CheckIdFromTable(tableName, columun, id);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand com = new SqlCommand(sQuery, GetConnection());

            adapter.SelectCommand = com;
            adapter.Fill(table);

            if (table.Rows.Count > 0) return true;
            else return false;
        }
        public bool checkId(string tableName, string columun, string id)
        {
            string sQuery = CheckIdFromTable(tableName, columun, id);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            SqlCommand com = new SqlCommand(sQuery, GetConnection());

            adapter.SelectCommand = com;
            adapter.Fill(table);

            if (table.Rows.Count > 0) return true;
            else return false;
        }
        public bool checkDate(string date, DateTime dt) => (date != null) ? DateTime.TryParse(date, out dt) : false;
        public bool checkPlace(string place) => (place != null) ? (System.Text.RegularExpressions.Regex.Match(place, @"([0-9]?[a-z])") != null) : false;
        public void CreateNewPassInTrip(string num, string id, string date, string place)
        {
            OpenConnection();

            bool n = checkNum("Trip", "trip_no", num), i = checkId("Passenger", "ID_psg", id), d = checkDate(date, new DateTime()), p = checkPlace(place);
            try
            {
                if (n && i && d && p) 
                {
                    string Query = QueryPassInTrip(num, id, date, place);

                    if (ExecuteSqlCommandCreate(Query) == 1)
                        MessageBox.Show("Запись успешно добавлена!");
                }
                else throw new ArgumentException();
            }
            catch
            {
                if (!n)
                {
                    MessageBox.Show("Номер рейса не действителен!", "Ошибка!");
                }
                else if (!i)
                {
                    MessageBox.Show("ID пассажира не действителен!", "Ошибка!");
                }
                else if (!d)
                {
                    MessageBox.Show("Введите корректное значение даты!", "Ошибка!");
                }
                else if (!p)
                {
                    MessageBox.Show("Введите корректное значение места!", "Ошибка!");
                }
            }
            CloseConnection();
        }

        public string QueryTrip(string num, string comp, string plane, string tFrom, string tTo, string tOut, string tIn) =>
            (num == null || comp == null || plane == null || tTo == null || tFrom == null || tOut == null || tIn == null) ? null : $"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{num}','{comp}','{plane}','{tFrom}','{tTo}','{tOut}','{tIn}')";
        public void CreateNewTrip(string tripNo, string idComp, string plane, string townFrom, string townTo, string timeOut, string timeIn)
        {
            OpenConnection();

            bool tIn = checkDate(timeIn, new DateTime()), tOut = checkDate(timeOut, new DateTime()), i = checkId("Company", "ID_comp", idComp), t = !checkNum("Trip", "trip_no", tripNo);
            try
            {
                if (!(tIn && tOut && i && t)) throw new ArgumentException();

                string Query = QueryTrip(tripNo, idComp, plane, townTo, townFrom, timeOut, timeIn); ;

                if (ExecuteSqlCommandCreate(Query) == 1)
                    MessageBox.Show("Запись успешно добавлена!", "Успешно!");
            }
            catch
            {
                if (!tIn) MessageBox.Show("Введите корректное значение в пункте \"Время прибытия\"!", "Ошибка!");
                else if (!tOut) MessageBox.Show("Введите корректное значение в пункте \"Время отправления\"!", "Ошибка!");
                else if (!t) MessageBox.Show("Таблица не должна содержать одинаковые значения в пункте \"Номер рейса\"!", "Ошибка!");
                else if (!i) MessageBox.Show("Данной компании не существует!\nВведите корректное значение!", "Ошибка!");
            }

            CloseConnection();
        }

        public string QueryUser(string login, string password) => (login == null || password == null) ? null : $"select id_user, login_user, password_user, is_admin from register where login_user = '{login}' and password_user = '{password}'";
        public DataTable GetUsers(string login, string password)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string Query = QueryUser(login, password);

            var command = new SqlCommand(Query, GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public string QueryNewUser(string login, string password) => (login == null || password == null) ? null : $"insert into Register(login_user, password_user, is_admin) values('{login}', '{password}', 0)";
        public void CreateNewUser(string login, string password)
        {
            OpenConnection();

            string Query = QueryNewUser(login, password);

            ExecuteSqlCommandCreate(Query);

            CloseConnection();
        }

        public string QueryReadPass(string s) => (s == "" || s == null) ? "select * from Passenger" : $"select * from Passenger where concat (ID_psg, name) like '%" + s + "%'";
        public SqlDataReader ReadPassengers(string s)
        {
            SqlCommand command = new SqlCommand(QueryReadPass(s), GetConnection());

            return command.ExecuteReader();
        }
        public string QueryDeletePass(string id) => (id == null) ? null : $"delete from Passenger where ID_psg = '{id}'";
        public string QueryUpdatePass(string name, string id) => (name == null || id == null) ? null : $"update Passenger set name = '{name}' where ID_psg = '{id}'";
        public void UpdatePass(string id, string name, RowState rs)
        {
            OpenConnection();
            switch (rs)
            {
                case RowState.Deleted:
                    {
                        OpenConnection();

                        var query = QueryDeletePass(id);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
                case RowState.Modified:
                    {
                        OpenConnection();

                        var query = QueryUpdatePass(name, id);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
            }
            CloseConnection();
        }

        public string QueryReadCompany(string s) => (s == "" || s == null) ? "select * from Company" : $"select * from Company where concat (ID_comp, name) like '%" + s + "%'";
        public SqlDataReader ReadCompanies(string s)
        {
            SqlCommand command = new SqlCommand(QueryReadCompany(s), GetConnection());

            return command.ExecuteReader();
        }
        public string QueryDeleteComp(string id) => (id == null) ? null : $"delete from Company where ID_comp = '{id}'";
        public string QueryUpdateComp(string name, string id) => (name == null || id == null) ? null : $"update Company set name = '{name}' where ID_comp = '{id}'";
        public void UpdateComp(string id, string name, RowState rs)
        {
            OpenConnection();
            switch (rs)
            {
                case RowState.Deleted:
                    {
                        OpenConnection();

                        var query = QueryDeleteComp(id);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
                case RowState.Modified:
                    {
                        OpenConnection();

                        var query = QueryUpdateComp(name, id);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
            }
            CloseConnection();
        }

        public string QueryReadTrip(string s) => (s == "" || s == null) ? $"select * from Trip" : $"select * from Trip where concat (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) like '%" + s + "%'";
        public SqlDataReader ReadTrips(string s)
        {
            SqlCommand command = new SqlCommand(QueryReadTrip(s), GetConnection());

            return command.ExecuteReader();
        }
        public string QueryDeleteTrip(string num) => (num == null) ? null : $"delete from Trip where trip_no = '{num}'";
        public string QueryUpdateTrip(string id, string num, string plane, string from, string to, string tOut, string tIn) =>
            (id == null || num == null || plane == null || from == null || to == null || tOut == null || tIn == null) ? null : $"update Trip set ID_comp = '{id}', plane = '{plane}', town_from = '{from}', town_to = '{to}', time_out = '{tOut}', time_in = '{tIn}' where trip_no = '{num}'";
        public void UpdateTrip(string id, string num, string plane, string from, string to, string tOut, string tIn, RowState rs)
        {
            OpenConnection();
            switch (rs)
            {
                case RowState.Deleted:
                    {
                        OpenConnection();

                        var query = QueryDeleteTrip(num);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
                case RowState.Modified:
                    {
                        OpenConnection();

                        bool timeOut = checkDate(tOut, new DateTime()), timeIn = checkDate(tIn, new DateTime());
                        try
                        {
                            if (!(timeOut && timeIn)) throw new Exception();

                            var query = QueryUpdateTrip(id, num, plane, from, to, tOut, tIn);
                            var command = new SqlCommand(query, GetConnection());
                            command.ExecuteNonQuery();
                        }
                        catch
                        {
                            if (timeIn || timeOut) MessageBox.Show("Неккоректное значение даты!");
                        }

                        CloseConnection();
                        break;
                    }
            }
            CloseConnection();
        }

        public string QueryReadPiT(string s) => (s == "" || s == null) ? $"select * from Pass_in_trip" : $"select * from Pass_in_trip where concat (trip_no, date, ID_psg, place) like '%" + s + "%'";
        public SqlDataReader ReadPiT(string s)
        {
            SqlCommand command = new SqlCommand(QueryReadPiT(s), GetConnection());

            return command.ExecuteReader();
        }
        public string QueryDeletePiT(string id, string num, string date) => (id == null || num == null || date == null) ? null : $"delete from Pass_in_trip where trip_no = '{num}' and date = '{date}' and ID_psg = '{id}'";
        public string QueryUpdatePiT(string id, string num, string date, string newPlace) =>
            (id == null || num == null || date == null || newPlace == null) ? null : $"update Pass_in_trip set place = '{newPlace}' where trip_no = '{num}' and date = '{date}' and ID_psg = '{id}'";
        public void UpdatePiT(string id, string num, string date, RowState rs, string newPlace)
        {
            OpenConnection();
            switch (rs)
            {
                case RowState.Deleted:
                    {
                        OpenConnection();

                        var query = QueryDeletePiT(id, num, date);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
                case RowState.Modified:
                    {
                        OpenConnection();

                        var query = QueryUpdatePiT(id, num, date, newPlace);
                        var command = new SqlCommand(query, GetConnection());
                        command.ExecuteNonQuery();

                        CloseConnection();
                        break;
                    }
            }
            CloseConnection();
        }
    }
}