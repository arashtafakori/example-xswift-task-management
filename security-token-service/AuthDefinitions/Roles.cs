namespace IdentityServerHost
{
    public class Roles
    {      
        // Members who can create, modify and delete build definitions and manage projects.
        public const string Admin = "admin";

        // Members who can add, modify, and delete tasks within the project which related to himself/herself,
        // and also can read other contributers task,
        // and also can read the sprints informatios.
        public const string Contributer = "contributer";
    }
}
