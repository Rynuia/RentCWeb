using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace RentCWeb.DBConnections
{
    public class DBConnection
    {
        private SqlConnection sqlCon;
        private SqlCommand sqlCom;

        public DBConnection()
        {
            sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
            sqlCom = new SqlCommand();
        }

        public SqlConnection SqlConnectionObj
        {
            get
            {
                return sqlCon;
            }
        }

        public SqlCommand SqlCommandObj
        {
            get
            {
                sqlCom.Connection = sqlCon;
                return sqlCom;
            }
        }
    }
}