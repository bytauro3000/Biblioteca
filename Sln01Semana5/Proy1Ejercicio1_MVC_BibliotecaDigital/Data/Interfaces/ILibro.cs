using Proy1Ejercicio1_MVC_BibliotecaDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Data.Interfaces {
    internal interface ILibro {
        // Métodos CRUD
        int Registrar(Libro libro);
        Libro BuscarPorID(int id);
        bool Actualizar(Libro libro);
        bool Eliminar(int id);

        // Métodos Adicionales
        //List<Libro> BuscarPorTitulo(String titulo);
        //List<Libro> BuscarPorAutor(String autor);
        //List<Libro> BuscarPorAnio(int anio);
        List<Libro> BuscarPorCategoria(int idCategoria);
    }
}
