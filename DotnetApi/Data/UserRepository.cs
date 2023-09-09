using DotnetApi.Models;

namespace DotnetApi.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFrameWork;

        public UserRepository(IConfiguration _config)
        {
            _entityFrameWork = new DataContextEF(_config);
        }
        public bool SaveChanges()
        {
            return _entityFrameWork.SaveChanges() > 0;
        }
        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                 _entityFrameWork.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
                _entityFrameWork.Remove(entityToRemove);
        }

        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFrameWork.Users.ToList<User>();
            return users;
        }

        public User GetUser(int id)
        {
            User? result = _entityFrameWork.Users.Where(u => u.UserId == id).FirstOrDefault<User>();

            if (result != null)
            {
                return result;
            }

            throw new Exception("User Could not be found!");
        }
    }
}