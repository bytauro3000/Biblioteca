using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Models {
    public class Categoria {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Categoria() {
            Id = 0;
            Nombre = string.Empty;
        }

        public Categoria(int id, string nombre) {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString() {
            return $"Categoria [id = {Id}, nombre = {Nombre}]";
        }
    }
}