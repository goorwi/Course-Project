using System;
using NUnit.Framework;
using Moq;

namespace Полёты
{
    [TestFixture]
    public class UnitTestDataBase
    {
        DataBase db;
        [SetUp]
        public void InitDataBase()
        {
            db = new DataBase();
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestQueryRead(string s)
        {
            Assert.AreEqual("select * from Passenger", db.QueryReadPass(s));
            Assert.AreEqual("select * from Company", db.QueryReadCompany(s));
            Assert.AreEqual("select * from Trip", db.QueryReadTrip(s));
            Assert.AreEqual("select * from Pass_in_trip", db.QueryReadPiT(s));
        }

        [TestCase("V.")]
        public void TestQuerySearch(string s)
        {
            Assert.AreEqual("select * from Passenger where concat (ID_psg, name) like '%V.%'", db.QueryReadPass(s));
            Assert.AreEqual("select * from Company where concat (ID_comp, name) like '%V.%'", db.QueryReadCompany(s));
            Assert.AreEqual("select * from Trip where concat (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) like '%V.%'", db.QueryReadTrip(s));
            Assert.AreEqual("select * from Pass_in_trip where concat (trip_no, date, ID_psg, place) like '%V.%'", db.QueryReadPiT(s));
        }

        [TestCase("Passenger", "Kireev D.A.")]
        [TestCase("Company", "Kireev D.A.")]
        public void TestQueryAdd(string table, string name)
        {
            Assert.AreEqual($"insert into {table} (name) values('{name}')", db.QueryPassengerOrCompany(table, name));
        }

        [TestCase("Pass_in_trip", "1566", "16.10.2020", "15", "1c")]
        public void TestQueryAddPassInTrip(string table, string num, string date, string id, string place)
        {
            Assert.AreEqual($"insert into {table} (trip_no, date, ID_psg, place) values('{num}', '{date}', '{id}', '{place}')", db.QueryPassInTrip(num, id, date, place));
        }

        [TestCase("Trip", "1566", "1", "5", "London", "Washington", "16.10.2020", "17.10.2020")]
        public void TestQueryAddTrip(string table, string num, string comp, string plane, string townFrom, string townTo, string timeOut, string timeIn)
        {
            Assert.AreEqual($"insert into {table} (trip_no, ID_comp, plane, town_from, town_to, time_out, time_in) values('{num}','{comp}','{plane}','{townFrom}','{townTo}','{timeOut}','{timeIn}')",
                db.QueryTrip(num, comp, plane, townFrom, townTo, timeOut, timeIn));
        }

        [TestCase(null)]
        public void TestQueryAddNull(string str)
        {
            Assert.IsNull(db.QueryPassengerOrCompany(str, str));
            Assert.IsNull(db.QueryPassInTrip(str, str, str, str));
            Assert.IsNull(db.QueryTrip(str, str, str, str, str, str, str));
        }

        [TestCase("Passenger", "ID_psg", "1")]
        public void TestQueryCheckId(string table, string column, string id)
        {
            Assert.AreEqual($"select {column} from {table} where {column} = '{id}'", db.CheckIdFromTable(table, column, id));
        }

        [TestCase(null)]
        public void TestQueryCheckIdNull(string str)
        {
            Assert.IsNull(db.CheckIdFromTable(str, str, str));
        }

        [TestCase("10.10.2010")]
        public void TestCheckDate(string date)
        {
            Assert.IsTrue(db.checkDate(date, new DateTime()));
        }

        [TestCase(null)]
        public void TestCheckDateNull(string date)
        {
            Assert.IsFalse(db.checkDate(date, new DateTime()));
        }

        [TestCase("1c")]
        public void TestCheckPlace(string place)
        {
            Assert.IsTrue(db.checkPlace(place));
        }

        [TestCase(null)]
        public void TestCheckPlaceNull(string place)
        {
            Assert.IsFalse(db.checkPlace(place));
        }

        [TestCase("admin", "admin")]
        public void TestQueryUser(string login, string password)
        {
            Assert.AreEqual($"select id_user, login_user, password_user, is_admin from register where login_user = '{login}' and password_user = '{password}'", db.QueryUser(login, password));
        }

        [TestCase(null, null)]
        public void TestQueryUserNull(string login, string password)
        {
            Assert.IsNull(db.QueryUser(login, password));
        }

        [TestCase("admin", "admin")]
        public void TestAddNewUser(string login, string password)
        {
            Assert.AreEqual($"insert into Register(login_user, password_user, is_admin) values('{login}', '{password}', 0)", db.QueryNewUser(login, password));
        }

        [TestCase(null, null)]
        public void TestAddNewUserNull(string login, string password)
        {
            Assert.IsNull(db.QueryNewUser(login, password));
        }

        [TestCase("2")]
        public void TestQueryDeletePass(string id)
        {
            Assert.AreEqual($"delete from Passenger where ID_psg = '{id}'", db.QueryDeletePass(id));
        }

        [TestCase("Kireev D.A.", "2")]
        public void TestQueryUpdatePass(string name, string id)
        {
            Assert.AreEqual($"update Passenger set name = '{name}' where ID_psg = '{id}'", db.QueryUpdatePass(name, id));
        }

        [TestCase(null)]
        public void TestQueryDeletePassNull(string id)
        {
            Assert.IsNull(db.QueryDeletePass(id));
        }

        [TestCase(null, null)]
        public void TestQueryUpdatePassNull(string name, string id)
        {
            Assert.IsNull(db.QueryUpdatePass(name, id));
        }

        [TestCase("2")]
        public void TestQueryDeleteComp(string id)
        {
            Assert.AreEqual($"delete from Company where ID_comp = '{id}'", db.QueryDeleteComp(id));
        }

        [TestCase("Lagord", "2")]
        public void TestQueryUpdateComp(string name, string id)
        {
            Assert.AreEqual($"update Company set name = '{name}' where ID_comp = '{id}'", db.QueryUpdateComp(name, id));
        }

        [TestCase(null)]
        public void TestQueryDeleteCompNull(string id)
        {
            Assert.IsNull(db.QueryDeleteComp(id));
        }

        [TestCase(null, null)]
        public void TestQueryUpdateCompNull(string name, string id)
        {
            Assert.IsNull(db.QueryUpdateComp(name, id));
        }

        [TestCase("2")]
        public void TestQueryDeleteTrip(string id)
        {
            Assert.AreEqual($"delete from Trip where trip_no = '{id}'", db.QueryDeleteTrip(id));
        }

        [TestCase("2", "1566", "Lagord", "London", "Washington", "16.10.2020", "17.10.2020")]
        public void TestQueryUpdateTrip(string id, string num, string plane, string from, string to, string tout, string tin)
        {
            Assert.AreEqual($"update Trip set ID_comp = '{id}', plane = '{plane}', town_from = '{from}', town_to = '{to}', time_out = '{tout}', time_in = '{tin}' where trip_no = '{num}'",
                db.QueryUpdateTrip(id, num, plane, from, to, tout, tin));
        }

        [TestCase(null)]
        public void TestQueryDeleteTripNull(string id)
        {
            Assert.IsNull(db.QueryDeleteTrip(id));
        }

        [TestCase(null, null, null, null, null, null, null)]
        public void TestQueryUpdateTripNull(string id, string num, string plane, string from, string to, string tout, string tin)
        {
            Assert.IsNull(db.QueryUpdateTrip(id, num, plane, from, to, tout, tin));
        }

        [TestCase("2", "1566", "10.10.2010")]
        public void TestQueryDeletePiT(string id, string num, string date)
        {
            Assert.AreEqual($"delete from Pass_in_trip where trip_no = '{num}' and date = '{date}' and ID_psg = '{id}'", db.QueryDeletePiT(id, num, date));
        }

        [TestCase("2", "1566", "10.10.2010", "1c", "2c")]
        public void TestQueryUpdatePiT(string id, string num, string date, string place, string newPlace)
        {
            Assert.AreEqual($"update Pass_in_trip set place = '{newPlace}' where trip_no = '{num}' and date = '{date}' and ID_psg = '{id}'",
                db.QueryUpdatePiT(id, num, date, newPlace));
        }

        [TestCase(null, null, null)]
        public void TestQueryDeletePiTNull(string id, string num, string date)
        {
            Assert.IsNull(db.QueryDeletePiT(id, num, date));
        }

        [TestCase(null, null, null, null, null)]
        public void TestQueryUpdatePiTNull(string id, string num, string date, string place, string newPlace)
        {
            Assert.IsNull(db.QueryUpdatePiT(id, num, date, newPlace));
        }
    }
}
