using System.Configuration;

namespace Tools
{
    class MyDataBase
    {
        public DbHelperSQL Inti(string DbName)
        {
            string Context = GetDbContext(DbName);
            return new DbHelperSQL(Context);
        }
        public DbHelperSQL IntiContext(string Context)
        {
            return new DbHelperSQL(Context);
        }
        public string GetDbContext(string DbName)
        {
            string Context = string.Empty;
            Context = ConfigurationManager.ConnectionStrings[DbName].ConnectionString.ToString();
            return Context;
        }
    }
}