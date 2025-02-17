using Proy1Ejercicio1_MVC_BibliotecaDigital.Data.Interfaces;
using Proy1Ejercicio1_MVC_BibliotecaDigital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Data {
    public class CategoriaDAO : ICategoria {
        public List<Categoria> ListarTodo() {
            // Crear la lista de categorias
            List<Categoria> listaCategorias = new List<Categoria>();
            // Obtener la conexion
            SqlConnection cnx = DataBase.ObtenerConexion();
            // Abrir la conexion
            cnx.Open();
            // Crear el comando
            SqlCommand cmd = cnx.CreateCommand();
            // Establecer el tipo de comando
            cmd.CommandType = System.Data.CommandType.Text;
            // Establecer la consulta
            cmd.CommandText = "SELECT * FROM Categoria";
            // Ejecutar el comando
            SqlDataReader dr = cmd.ExecuteReader();
            // Recorrer el DataReader
            while (dr.Read()) {
                // Crear un objeto de la clase Categoria
                Categoria c = new Categoria((int)dr["ID"], dr["Nombre"] as string);
                // Agregar el objeto a la lista
                listaCategorias.Add(c);
            }
            // Cerrar el DataReader y la conexion
            dr.Close(); cnx.Close();
            // Retornar la lista de categorias
            return listaCategorias;
        }
    }
}