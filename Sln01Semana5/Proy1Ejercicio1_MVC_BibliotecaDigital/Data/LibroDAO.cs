using Proy1Ejercicio1_MVC_BibliotecaDigital.Data.Interfaces;
using Proy1Ejercicio1_MVC_BibliotecaDigital.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Data {
    public class LibroDAO : ILibro {
        public int Registrar(Libro libro) {
            SqlConnection cnx = DataBase.ObtenerConexion();
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO Libro (Titulo, Autor, Anio_publicacion, Resumen, NumPaginas, Editorial, Categoria_ID) VALUES (@pTitulo, @pAutor, @pAnio, @pResumen, @pNPaginas, @pEditorial, @pIdCategoria); SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@pTitulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@pAutor", libro.Autor);
            cmd.Parameters.AddWithValue("@pAnio", libro.AnioPublicacion);
            cmd.Parameters.AddWithValue("@pResumen", libro.Resumen);
            cmd.Parameters.AddWithValue("@pNPaginas", libro.NumPaginas);
            cmd.Parameters.AddWithValue("@pEditorial", libro.Editorial);
            cmd.Parameters.AddWithValue("@pIdCategoria", libro.Categoria.Id);
            // Ejecutar el comando y obtener el ID generado
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            cnx.Close();
            return id;
        }

        public Libro BuscarPorID(int id) {
            Libro libro = new Libro() { Id = -1, Titulo = "No encontrado" };
            SqlConnection cnx = DataBase.ObtenerConexion();
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "SELECT * FROM Libro WHERE ID = @pId";
            cmd.Parameters.AddWithValue("@pId", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read()) {
                libro = new Libro((int)dr["ID"], (string)dr["Titulo"], (string)dr["Autor"],
                                        (int)dr["Anio_publicacion"], (string)dr["Resumen"], (int)dr["NumPaginas"],
                                        (string)dr["Editorial"], new Categoria((int)dr["Categoria_ID"], "---"));
            }
            dr.Close();
            cnx.Close();
            return libro;
        }

        public bool Actualizar(Libro libro) {
            SqlConnection cnx = DataBase.ObtenerConexion();
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_ActualizarLibro";
            cmd.Parameters.AddWithValue("@pTitulo", libro.Titulo);
            cmd.Parameters.AddWithValue("@pAutor", libro.Autor);
            cmd.Parameters.AddWithValue("@pAnio", libro.AnioPublicacion);
            cmd.Parameters.AddWithValue("@pResumen", libro.Resumen);
            cmd.Parameters.AddWithValue("@pNPaginas", libro.NumPaginas);
            cmd.Parameters.AddWithValue("@pEditorial", libro.Editorial);
            cmd.Parameters.AddWithValue("@pIdCategoria", libro.Categoria.Id);
            cmd.Parameters.AddWithValue("@pIdLibro", libro.Id);
            int resultado = Convert.ToInt32(cmd.ExecuteScalar());
            cnx.Close();
            bool confirmado = resultado == 1;
            return confirmado;
        }

        public bool Eliminar(int id) {
            SqlConnection cnx = DataBase.ObtenerConexion();
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "DELETE FROM Libro WHERE ID = @pIdLibro";
            cmd.Parameters.AddWithValue("@pIdLibro", id);
            int filasAfectadas = cmd.ExecuteNonQuery();
            cnx.Close();
            // Si se elimino por lo menos 1 fila entonces será verdadero
            bool confirmacion = filasAfectadas > 0;
            return confirmacion;
        }

        public List<Libro> BuscarPorCategoria(int idCategoria) {
            List<Libro> librosBuscados = new List<Libro>();
            SqlConnection cnx = DataBase.ObtenerConexion();
            cnx.Open();
            SqlCommand cmd = cnx.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_ObtenerLibrosPorCategoria";
            cmd.Parameters.AddWithValue("@categoriaID", idCategoria);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                Categoria categoria = new Categoria(idCategoria, (string)dr["NombreCategoria"]);
                Libro libro = new Libro((int)dr["ID"], (string)dr["Titulo"], (string)dr["Autor"],
                                        (int)dr["Anio_publicacion"], (string)dr["Resumen"], (int)dr["NumPaginas"],
                                        (string)dr["Editorial"], categoria);
                librosBuscados.Add(libro);
            }
            dr.Close();
            cnx.Close();
            return librosBuscados;
        }
    }
}