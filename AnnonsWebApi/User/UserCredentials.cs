namespace AnnonsWebApi.User
{
    public class UserCredentials
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() // Full read/write access
            {
                UserName = "richard_admin",
                EmailAddress = "richard_admin@email.se",
                Password = "passwordAdmin",
                GivenName = "Richard",
                SurName = "chalk",
                Role = "Admin",
            },
            new UserModel() // Can only Read
            {
                UserName = "richard_user",
                EmailAddress = "richard_user@email.se",
                Password = "passwordUser",
                GivenName = "Richard",
                SurName = "Chalk",
                Role = "User",
            },

            new UserModel() // Full read/write access
            {
                UserName = "alexis_admin",
                EmailAddress = "alexis_admin@email.se",
                Password = "passwordAdmin",
                GivenName = "Alexis",
                SurName = "Briceno",
                Role = "Admin",
            },
            new UserModel() // Can only Read
            {
                UserName = "alexis_user",
                EmailAddress = "alexis_user@email.se",
                Password = "passwordUser",
                GivenName = "Alexis",
                SurName = "Briceno",
                Role = "User",
            }
        };
    }
}
