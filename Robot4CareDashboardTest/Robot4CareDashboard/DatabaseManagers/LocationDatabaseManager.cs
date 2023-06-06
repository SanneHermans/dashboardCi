using MySql.Data.MySqlClient;
using Robot4CareDashboard.Models;
using System.Configuration;

namespace Robot4CareDashboard.DatabaseManagers
{
    public class LocationDatabaseManager
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public LocationDatabaseManager()
        {
            configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                      .AddJsonFile("appsettings.json")
                                                      .Build();
            connectionString = configuration.GetConnectionString("Robot4Care");
        }

        public LocationDatabaseManager(IConfiguration Configuration)
        {
            configuration = Configuration;
            connectionString = configuration.GetConnectionString("Robot4Care");
        }

        public List<LocationModel> Get() 
        {
            List<LocationModel> locations = new List<LocationModel>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                string query = "SELECT ID, X, Y, Name FROM locations";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            locations.Add(new LocationModel
                            {
                                X = (Double)sdr["X"],
                                Y = (Double)sdr["Y"],
                                Name = (String)sdr["Name"],
                                Id = (int)sdr["Id"]
                            });
                        }
                    }
                    con.Close();
                }
            }
            return locations;
        }

        public LocationModel Create(LocationModel locationModel)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using MySqlCommand command = new MySqlCommand(@"INSERT INTO locations(X, Y, Name) VALUES(@X, @Y, @Name);", conn);
                conn.Open();

                command.Parameters.AddWithValue("@X", locationModel.X);
                command.Parameters.AddWithValue("@Y", locationModel.Y);
                command.Parameters.AddWithValue("@Name", locationModel.Name);

                command.ExecuteScalar();
            }
            return locationModel;
        }

        public bool Delete(int Id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using MySqlCommand command = new MySqlCommand(@"DELETE from locations WHERE ID=@Id", conn);

                conn.Open();

                command.Parameters.AddWithValue("@ID", Id);
                try
                {
                    command.ExecuteScalar();
                }
                catch(MySqlException ex)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
