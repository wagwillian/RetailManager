using Microsoft.Extensions.Configuration;
using RMDataManager.Library.Models;
using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _sql;

        public UserData(ISqlDataAccess sql)
        {
            _sql = sql;
        }
        public List<UserModel> GetUserById(string Id)
        {

            var output = _sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", new { Id }, "RMData");

            return output;
        }

        public void CreateUser(UserModel user)
        {
            _sql.SaveData("dbo.spUser_Insert", new { user.Id, user.FirstName, user.LastName, user.EmailAddress }, "RMData");
        }
    }
}
