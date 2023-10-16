namespace StudentManagingSystem_API.Configuration
{
    public class DatabaseConfiguration
    {
        public string DatabaseProvider { get; set; }
        public string DatabaseConnectionString { get; set; }
    }
    public class DatabaseConnection
    {
        public const string DATABASE = "DatabaseConfiguration.SMS";
    }
}
