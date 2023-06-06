using NUnit.Framework;
using Robot4CareDashboard.DatabaseManagers;
using Robot4CareDashboard.Logics;
using Moq;
using Robot4CareDashboard.Models;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Dashboard.test
{
    public class Tests
    {
        private LocationLogics locationLogics;
        private Mock<LocationDatabaseManager> databaseManagerMock;

        [SetUp]
        public void Setup()
        {
            databaseManagerMock = new Mock<LocationDatabaseManager>();
            locationLogics = new LocationLogics(databaseManagerMock.Object);
        }

        [Test]
        public void Get_ShouldReturnListOfLocations()
        {
            // Arrange
            string connectionString = "Server=localhost;Port=3306;Database=Robot4Care;Uid=root;Pwd=root;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand("DELETE FROM locations;", conn))
                {
                    command.ExecuteNonQuery();
                }
            }

            locationLogics.Create(11, 22, "Location1");
            locationLogics.Create(33, 44, "Location2");

            //List<LocationModel> expectedLocations = new List<LocationModel>()
            //{
            //    new LocationModel { X = 11, Y = 22, Name = "Location1"},
            //    new LocationModel { X = 33, Y = 44, Name = "Location2" }
            //};

            // Act
            List<LocationModel> result = locationLogics.Get();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
        [Test]
        public void Create_WithValidData_ShouldReturnTrue()
        {
            // Act
            bool response = locationLogics.Create(22, 33, "location1");

            // Assert
            Assert.AreEqual(true, response);
        }
        [Test]
        public void Create_WithLocationBelowZero_ShouldReturnFalse()
        {
            // Act
            bool response = locationLogics.Create(16, -13, "location");

            // Assert
            Assert.AreEqual(false, response);
        }
        [Test]
        public void Create_WithNameIsNull_ShouldReturnFalse()
        {
            // Act
            bool response = locationLogics.Create(16, 13, null);

            // Assert
            Assert.AreEqual(false, response);
        }
    }
}