using Davantes_SODV1255_LMS_Assignment3.Models;

namespace Davantes_SODV1255_LMS_Assignment3.Repositories {
    public static class UserRepository {
        private static List<User> _users = new List<User>(){
            new User(1, "Admin", "User", "admin@email.com", "password123"),
            new User(2, "The", "Librarian", "librarian@email.com", "password123")
        };

        public static User? Authenticate(string email, string password) {
            return _users.FirstOrDefault(user =>
                user.Email.ToLower() == email.ToLower()
                && user.Password == password
            );
        }

        public static User? GetById(int id) {
            var user = _users.FirstOrDefault(user => user.ID == id);

            if(user != null) {
                return user;
            }

            return null;
        }

        public static bool EmailExists(string email) {
            return _users.Any(user => user.Email.ToLower() == email.ToLower());
        }

        public static User Add(User user) {
            var nextID = (_users.Count == 0) ? 1 : _users.Max(u => u.ID) + 1;
            user.ID = nextID;
            _users.Add(user);
            return user;
        }
    }
}
