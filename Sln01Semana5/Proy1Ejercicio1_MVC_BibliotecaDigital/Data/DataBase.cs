using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Data {
    public class DataBase {
        public static SqlConnection ObtenerConexion() {
            // Forma 1
            //SqlConnectionStringBuilder generadorCadenaCnx = new SqlConnectionStringBuilder();
            //generadorCadenaCnx.DataSource = "localhost";
            //generadorCadenaCnx.InitialCatalog = "BibliotecaDigital";
            //generadorCadenaCnx.UserID = "sa";
            //generadorCadenaCnx.Password = "sql";
            //string cadenaCnx = generadorCadenaCnx.ConnectionString;
            string cadenaCnx = ConfigurationManager.ConnectionStrings["DB_Biblioteca_Digital"].ConnectionString;
            SqlConnection cnx = new SqlConnection(cadenaCnx);
            return cnx;
        }
    }
}