using BlogDataLibrary.Database;
using BlogDataLibrary.Models;

namespace BlogDataLibrary.Data
{
    public class SqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public UserModel? Authenticate(string username, string password)
        {
            var output = _db.LoadData<UserModel, dynamic>(
                "dbo.spUsers_Authenticate",
                new { UserName = username, Password = password },
                connectionStringName,
                true).GetAwaiter().GetResult();

            return output.FirstOrDefault();
        }
    }
}