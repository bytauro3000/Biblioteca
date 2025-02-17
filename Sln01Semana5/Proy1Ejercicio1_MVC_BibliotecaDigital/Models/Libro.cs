using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Models {
    public class Libro {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        public string Resumen { get; set; }
        public int NumPaginas { get; set; }
        public string Editorial { get; set; }
        public Categoria Categoria { get; set; }

        public Libro() {
            Id = 0;
            Titulo = string.Empty;
            Autor = string.Empty;
            AnioPublicacion = 0;
            Resumen = string.Empty;
            NumPaginas = 0;
            Editorial = string.Empty;
            Categoria = new Categoria();
        }

        public Libro(int id, string titulo, string autor, int anioPublicacion, string resumen, int numPaginas, string editorial, Categoria categoria) {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            AnioPublicacion = anioPublicacion;
            Resumen = resumen;
            NumPaginas = numPaginas;
            Editorial = editorial;
            Categoria = categoria;
        }

        public override string ToString() {
            return $"Libro [id = {Id}, titulo = {Titulo}, autor = {Autor}, anioPublicacion = {AnioPublicacion}, resumen = {Resumen}, numPaginas = {NumPaginas}, editorial = {Editorial}, categoria = {Categoria}]";
        }
    }
}