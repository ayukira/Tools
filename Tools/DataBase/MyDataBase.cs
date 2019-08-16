using System.Configuration;

namespace Tools
{
    class MyDataBase
    {
        private DbHelperSQL DbHelp { get; } = null;

        public static DbHelperSQL Inti(string DbName)
        {
            string Context = GetDbContext(DbName);
            return new DbHelperSQL(Context);
        }
        public static DbHelperSQL IntiContext(string Context)
        {
            return new DbHelperSQL(Context);
        }
        public static string GetDbContext(string DbName) {
            string Context = string.Empty;
            Context = ConfigurationManager.ConnectionStrings[DbName].ConnectionString.ToString();
            return Context;
        }
    }
}
