using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Transactions;
using System.IO;
using NUnit.Framework;
using Moq;

namespace Полёты
{
    [TestFixture]
    class IntegrationTestDataBase
    {
        DataBase db;
        SqlConnection sc = new SqlConnection(@"Data Source=ВЛАДИСЛАВ-ПК\SQLEXPRESS;Initial Catalog=aero;Integrated Security=True");
        TransactionScope scope;

        [SetUp]
        public void SetUp()
        {
            scope = new TransactionScope();

            db = new DataBase();
            db.OpenConnection();
        }
        [TearDown]
        public void TearDown()
        {
            db.CloseConnection();
            scope.Dispose();
        }


        [TestCase("1", "admin","admin", true)]
        [TestCase("2", "user", "user", false)]
        public void TestGetUsers(int id, string login, string password, bool isAdmin)
        {
            DataTable expected = new DataTable();
            for (int i = 0; i < 4; i++) expected.Columns.Add();
            DataRow r = expected.NewRow();
            r[0] = id; r[1] = login; r[2] = password;r[3] = isAdmin;
            expected.Rows.Add(r);

            DataTable actual = db.GetUsers(login, password);
            Assert.AreEqual(expected.Rows[0].ItemArray[0], actual.Rows[0].ItemArray[0].ToString().Trim());
            Assert.AreEqual(expected.Rows[0].ItemArray[1], actual.Rows[0].ItemArray[1].ToString().Trim());
            Assert.AreEqual(expected.Rows[0].ItemArray[2], actual.Rows[0].ItemArray[2].ToString().Trim());
            Assert.AreEqual(expected.Rows[0].ItemArray[3], actual.Rows[0].ItemArray[3].ToString().Trim());
        }
        [Test]
        public void TestOpenConnection()
        {
            Assert.AreEqual(ConnectionState.Open, db.GetConnection().State);
        }
        [Test]
        public void TestCloseConnection()
        {
            db.GetConnection().Close();
            Assert.AreEqual(ConnectionState.Closed, db.GetConnection().State);
        }
        [TestCase("")]
        public void TestReadPassengers(string s)
        {
            Assert.IsNotNull(db.ReadPassengers(s));
        }

        [TestCase("vlad", "vlad")]
        public void TestCreateNewUser(string login, string password)
        {
            db.CreateNewUser(login, password);

            var table = new DataTable();
            var command = new SqlCommand($"select login_user, password_user from Register where login_user = '{login}' and password_user = '{password}'", db.GetConnection());
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            Assert.AreEqual(1, table.Rows.Count);
            Assert.AreEqual(login, table.Rows[0].ItemArray[0]);
            Assert.AreEqual(password, table.Rows[0].ItemArray[1]);
        }
        [TestCase("Passenger", "Russkih V.D.", CreateForm.Pass)]
        [TestCase("Company", "Langrod", CreateForm.Comp)]
        public void TestCreateNewPassenger(string table, string name, CreateForm _cf)
        {
            db.CreateNewPassenger(table, name, _cf);
            
            var dataTable = new DataTable();
            var command = new SqlCommand($"select name from {table} where name = '{name}'", db.GetConnection());
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            Assert.AreEqual(1, dataTable.Rows.Count);
            Assert.AreEqual(name, dataTable.Rows[0].ItemArray[0].ToString().Trim());

        }
        [TestCase("Kidom L.D.")]
        public void TestExecuteCommandQuery(string name)
        {
            string query = $"insert into Passenger(name) values ('{name}')";
            Assert.AreEqual(1, db.ExecuteSqlCommandCreate(query));

            var dataTable = new DataTable();
            var command = new SqlCommand($"select name from Passenger where name = '{name}'", db.GetConnection());
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            Assert.AreEqual(name, dataTable.Rows[0].ItemArray[0].ToString().Trim());
        }
        [TestCase("Company", "ID_comp", "Landrog")]
        [TestCase("Passenger", "ID_psg", "Kidow J.O.")]
        public void TestCheckId(string tableName, string column, string name)
        {
            Assert.IsFalse(db.checkId(tableName, column, "0"));

            //Наполнение тестовыми данными
            var command = new SqlCommand($"insert into {tableName}(name) values('{name}')", db.GetConnection());
            command.ExecuteNonQuery();

            command.CommandText = $"select {column} from {tableName} where name = '{name}'";
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            var tableExpected = new DataTable();
            adapter.Fill(tableExpected);

            Assert.AreEqual(1, tableExpected.Rows.Count);
            Assert.IsTrue(db.checkNum(tableName, column, tableExpected.Rows[0].ItemArray[0].ToString()));
        }
        [TestCase("Trip", "trip_no", "5958")]
        public void TestCheckNum(string tableName, string column, string num)
        {
            Assert.IsFalse(db.checkNum(tableName, column, "0"));

            //Наполнение тестовыми данными

            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            command.CommandText = $"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{num}',{table.Rows[0].ItemArray[0]},'Harold','London','Washington','10.10.2010','11.10.2010')";
            command.ExecuteNonQuery();

            Assert.IsTrue(db.checkNum(tableName, column, num));
        }
        [TestCase("4568", "10.10.2010", "9f")]
        public void TestCreateNewPassInTrip(string num, string date, string place)
        {
            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            var id = table.Rows[0].ItemArray[0].ToString();
            command.CommandText = $"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{num}',{id},'Harold','London','Washington','10.10.2010','11.10.2010')";
            command.ExecuteNonQuery();
            command.CommandText = "insert into Passenger(name) values ('Kidow L.A.')";
            command.ExecuteNonQuery();
            adapter.SelectCommand = new SqlCommand("select ID_psg from Passenger where name = 'Kidow L.A.'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);
            id = table.Rows[0].ItemArray[0].ToString();

            db.CreateNewPassInTrip(num, id, date, place);

            table.Clear();
            adapter.SelectCommand = new SqlCommand($"select * from Pass_in_trip where trip_no = '{num}' and ID_psg = '{id}' and date = '{date}'", db.GetConnection());
            adapter.Fill(table);

            Assert.AreEqual(1, table.Rows.Count);
            Assert.AreEqual(id, table.Rows[0].ItemArray[0].ToString().Trim());
            Assert.AreEqual(num, table.Rows[0].ItemArray[1].ToString().Trim());
            Assert.AreEqual(DateTime.Parse(date), DateTime.Parse(table.Rows[0].ItemArray[2].ToString().Trim()));
            Assert.AreEqual(place, table.Rows[0].ItemArray[3].ToString().Trim());
        }
        [TestCase("5465", null, "Boeing", "London", "Washington", "10.10.2010", "11.10.2010")]
        public void TestCreateNewTrip(string tripNo, string idComp, string plane, string townFrom, string townTo, string timeOut, string timeIn)
        {
            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            idComp = table.Rows[0].ItemArray[0].ToString();

            db.CreateNewTrip(tripNo, idComp, plane, townFrom, townTo, timeOut, timeIn);

            table.Clear();
            adapter.SelectCommand = new SqlCommand($"select * from Trip where trip_no = '{tripNo}'", db.GetConnection());
            adapter.Fill(table);

            Assert.AreEqual(1, table.Rows.Count);
            Assert.AreEqual(idComp, table.Rows[0].ItemArray[0].ToString().Trim());
            Assert.AreEqual(tripNo, table.Rows[0].ItemArray[1].ToString().Trim());
            Assert.AreEqual(plane, table.Rows[0].ItemArray[2].ToString().Trim());
            Assert.AreEqual(townTo, table.Rows[0].ItemArray[3].ToString().Trim());
            Assert.AreEqual(townFrom, table.Rows[0].ItemArray[4].ToString().Trim());
            Assert.AreEqual(DateTime.Parse(timeOut), DateTime.Parse(table.Rows[0].ItemArray[5].ToString().Trim()));
            Assert.AreEqual(DateTime.Parse(timeIn), DateTime.Parse(table.Rows[0].ItemArray[6].ToString().Trim()));
        }
        [TestCase("Kidow J.L.", RowState.Deleted)]
        public void TestDeletePassenger(string name, RowState _rw)
        {
            var command = new SqlCommand($"insert into Passenger(name) values ('{name}')", db.GetConnection());
            command.ExecuteNonQuery();

            var table = new DataTable();
            var adapter = new SqlDataAdapter($"select ID_psg from Passenger where name = '{name}'", db.GetConnection());
            adapter.Fill(table);
            var ID_psg = table.Rows[0].ItemArray[0].ToString().Trim();

            db.UpdatePass(ID_psg, name, _rw);

            adapter.SelectCommand = new SqlCommand($"select * from Passenger where ID_psg = '{ID_psg}'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);

            Assert.AreEqual(0, table.Rows.Count);
        }
        [TestCase("Kidow J.L.", "Kidow S.A.", RowState.Modified)]
        public void TestUpdatePassenger(string name, string newName, RowState _rw)
        {
            var command = new SqlCommand($"insert into Passenger(name) values ('{name}')", db.GetConnection());
            command.ExecuteNonQuery();

            var table = new DataTable();
            var adapter = new SqlDataAdapter($"select ID_psg from Passenger where name = '{name}'", db.GetConnection());
            adapter.Fill(table);
            var ID_psg = table.Rows[0].ItemArray[0].ToString().Trim();

            db.UpdatePass(ID_psg, newName, _rw);

            adapter.SelectCommand = new SqlCommand($"select name from Passenger where ID_psg = '{ID_psg}'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);

            Assert.AreEqual(newName, table.Rows[0].ItemArray[0].ToString().Trim());
        }
        [TestCase("Langrod", RowState.Deleted)]
        public void TestDeleteCompany(string name, RowState _rw)
        {
            var command = new SqlCommand($"insert into Company(name) values ('{name}')", db.GetConnection());
            command.ExecuteNonQuery();

            var table = new DataTable();
            var adapter = new SqlDataAdapter($"select ID_comp from Company where name = '{name}'", db.GetConnection());
            adapter.Fill(table);
            var ID_comp = table.Rows[0].ItemArray[0].ToString().Trim();

            db.UpdateComp(ID_comp, name, _rw);

            adapter.SelectCommand = new SqlCommand($"select * from Company where ID_comp = '{ID_comp}'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);

            Assert.AreEqual(0, table.Rows.Count);
        }
        [TestCase("Langrod", "Ladgrove", RowState.Modified)]
        public void TestUpdateCompany(string name, string newName, RowState _rw)
        {
            var command = new SqlCommand($"insert into Company(name) values ('{name}')", db.GetConnection());
            command.ExecuteNonQuery();

            var table = new DataTable();
            var adapter = new SqlDataAdapter($"select ID_comp from Company where name = '{name}'", db.GetConnection());
            adapter.Fill(table);
            var ID_comp = table.Rows[0].ItemArray[0].ToString().Trim();

            db.UpdateComp(ID_comp, newName, _rw);

            adapter.SelectCommand = new SqlCommand($"select name from Company where ID_comp = '{ID_comp}'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);

            Assert.AreEqual(newName, table.Rows[0].ItemArray[0].ToString().Trim());
        }
        [TestCase("5465", null, "Boeing", "London", "Washington", "10.10.2010", "11.10.2010", RowState.Deleted)]
        public void TestDeleteTrip(string tripNo, string idComp, string plane, string townFrom, string townTo, string timeOut, string timeIn, RowState _rw)
        {
            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            idComp = table.Rows[0].ItemArray[0].ToString();

            command = new SqlCommand($"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{tripNo}','{idComp}','{plane}','{townFrom}','{townTo}','{timeOut}','{timeIn}')", db.GetConnection());
            command.ExecuteNonQuery();

            db.UpdateTrip(idComp, tripNo, plane, townFrom, townTo, timeOut, timeIn, _rw);

            table = new DataTable();
            adapter = new SqlDataAdapter($"select * from Trip where trip_no = '{tripNo}'", db.GetConnection());
            adapter.Fill(table);

            Assert.AreEqual(0, table.Rows.Count);
        }
        [TestCase("5465", null, "Boeing", "London", "Washington", "10.10.2010", "11.10.2010", RowState.Modified, "Moscow", "09.10.2010", "10.10.2010")]
        public void TestUpdateTrip(string tripNo, string idComp, string plane, string townFrom, string townTo, string timeOut, string timeIn, RowState _rw, string newTownFrom, string newTimeOut, string newTimeIn)
        {
            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            idComp = table.Rows[0].ItemArray[0].ToString();

            command = new SqlCommand($"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{tripNo}','{idComp}','{plane}','{townFrom}','{townTo}','{timeOut}','{timeIn}')", db.GetConnection());
            command.ExecuteNonQuery();

            db.UpdateTrip(idComp, tripNo, plane, newTownFrom, townTo, newTimeOut, newTimeIn, _rw);

            table = new DataTable();
            adapter = new SqlDataAdapter($"select * from Trip where trip_no = '{tripNo}'", db.GetConnection());
            adapter.Fill(table);

            Assert.AreEqual(1, table.Rows.Count);
            Assert.AreEqual(tripNo, table.Rows[0].ItemArray[0].ToString().Trim());
            Assert.AreEqual(idComp, table.Rows[0].ItemArray[1].ToString().Trim());
            Assert.AreEqual(plane, table.Rows[0].ItemArray[2].ToString().Trim());
            Assert.AreEqual(newTownFrom, table.Rows[0].ItemArray[3].ToString().Trim());
            Assert.AreEqual(townTo, table.Rows[0].ItemArray[4].ToString().Trim());
            Assert.AreEqual(DateTime.Parse(newTimeOut), DateTime.Parse(table.Rows[0].ItemArray[5].ToString().Trim()));
            Assert.AreEqual(DateTime.Parse(newTimeIn), DateTime.Parse(table.Rows[0].ItemArray[6].ToString().Trim()));
        }
        [TestCase("4568", "10.10.2010", "9f", RowState.Deleted)]
        public void TestDeletePassInTrip(string num, string date, string place, RowState _rw)
        {
            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            var id = table.Rows[0].ItemArray[0].ToString();

            command.CommandText = $"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{num}',{id},'Harold','London','Washington','10.10.2010','11.10.2010')";
            command.ExecuteNonQuery();
            command.CommandText = "insert into Passenger(name) values ('Kidow L.A.')";
            command.ExecuteNonQuery();
            adapter.SelectCommand = new SqlCommand("select ID_psg from Passenger where name = 'Kidow L.A.'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);
            id = table.Rows[0].ItemArray[0].ToString();

            command.CommandText = $"insert into Pass_in_trip(trip_no, date, ID_psg, place) values ('{num}','{date}','{id}','{place}')";
            command.ExecuteNonQuery();

            db.UpdatePiT(id, num, date, _rw, "");

            table.Clear();
            adapter.SelectCommand = new SqlCommand($"select * from Pass_in_trip where trip_no = '{num}' and ID_psg = '{id}' and date = '{date}'", db.GetConnection());
            adapter.Fill(table);

            Assert.AreEqual(0, table.Rows.Count);
        }
        [TestCase("4568", "10.10.2010", "9f", RowState.Modified, "10e")]
        public void TestUpdatePass_in_trip(string num, string date, string place, RowState _rw, string newPlace)
        {
            var command = new SqlCommand($"insert into Company(name) values ('Ladrog')", db.GetConnection());
            command.ExecuteNonQuery();
            var adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand("select ID_comp from Company where name = 'Ladrog'", db.GetConnection());
            var table = new DataTable();
            adapter.Fill(table);
            var id = table.Rows[0].ItemArray[0].ToString();

            command.CommandText = $"insert into Trip (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{num}',{id},'Harold','London','Washington','10.10.2010','11.10.2010')";
            command.ExecuteNonQuery();
            command.CommandText = "insert into Passenger(name) values ('Kidow L.A.')";
            command.ExecuteNonQuery();
            adapter.SelectCommand = new SqlCommand("select ID_psg from Passenger where name = 'Kidow L.A.'", db.GetConnection());
            table = new DataTable();
            adapter.Fill(table);
            id = table.Rows[0].ItemArray[0].ToString();

            command.CommandText = $"insert into Pass_in_trip(trip_no, date, ID_psg, place) values ('{num}','{date}','{id}','{place}')";
            command.ExecuteNonQuery();

            db.UpdatePiT(id, num, date, _rw, newPlace);

            table.Clear();
            adapter.SelectCommand = new SqlCommand($"select * from Pass_in_trip where trip_no = '{num}' and ID_psg = '{id}' and date = '{date}'", db.GetConnection());
            adapter.Fill(table);

            Assert.AreEqual(1, table.Rows.Count);
            Assert.AreEqual(id, table.Rows[0].ItemArray[0].ToString().Trim());
            Assert.AreEqual(num, table.Rows[0].ItemArray[1].ToString().Trim());
            Assert.AreEqual(DateTime.Parse(date), DateTime.Parse(table.Rows[0].ItemArray[2].ToString().Trim()));
            Assert.AreEqual(newPlace, table.Rows[0].ItemArray[3].ToString().Trim());
        }

    }
}
