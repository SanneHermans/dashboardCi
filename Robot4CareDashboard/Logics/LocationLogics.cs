using Robot4CareDashboard.DatabaseManagers;
using Robot4CareDashboard.Models;

namespace Robot4CareDashboard.Logics
{
    public class LocationLogics
    {
        LocationDatabaseManager databaseManager;
        public LocationLogics(LocationDatabaseManager locationDatabaseManager) {
            databaseManager = locationDatabaseManager;
        }

        public List<LocationModel> Get()
        {
            return databaseManager.Get();
        }

        public bool Create(int X, int Y, string Name) 
        {
            if (X < 0)
            {
                return false;
            }
            if (Y < 0)
            {
                return false;
            }
            if (Name == null)
            {
                return false;
            }
            LocationModel model = new LocationModel
            {
                X = X,
                Y = Y,
                Name = Name
            };
            try
            {
                LocationModel locations = databaseManager.Create(model);
                if (locations == null)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool Delete(int Id) 
        {
            if (Id < 0)
            {
                return false;
            }
            try
            {
                bool response = databaseManager.Delete(Id);
                if (response == false)
                {
                    return false;
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return true;
        }
    }
}
