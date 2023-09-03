using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;


namespace DotnetApi{

    class DataContextDapper {

        private readonly IConfiguration _iconfig;

        public DataContextDapper(IConfiguration iconfig) {
            _iconfig = iconfig;
        }

        public IEnumerable<T> LoadDAta<T>(String sql) {
            IDbConnection dbConnection = new SqlConnection(_iconfig.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }

        public T LoadDAtaSingle<T>(String sql) {
                IDbConnection dbConnection = new SqlConnection(_iconfig.GetConnectionString("DefaultConnection"));
                return dbConnection.QuerySingle<T>(sql);
        }
        public bool ExecuteSql(String sql) { 
            IDbConnection dbConnection = new SqlConnection(_iconfig.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql)>0;
        }     

        public int ExecuteSqlWithRowCount(String sql) { 
            IDbConnection dbConnection = new SqlConnection(_iconfig.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql);
        }    
    }       
}