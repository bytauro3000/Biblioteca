using Proy1Ejercicio1_MVC_BibliotecaDigital.Data;
using Proy1Ejercicio1_MVC_BibliotecaDigital.Data.Interfaces;
using Proy1Ejercicio1_MVC_BibliotecaDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Controllers {
    public class LibroController : Controller {
        [HttpGet]
        public ActionResult Buscar() {
            ViewBag.EsConforme = TempData["EsConforme"];
            ViewBag.Mensaje = TempData["Mensaje"];
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            List<Categoria> listaCategorias = categoriaDAO.ListarTodo();
            return View(listaCategorias);
        }

        [HttpPost]
        public ActionResult Buscar(FormCollection formulario) {
            int idCategoria = Convert.ToInt32(formulario["cmbCategoriaId"]);
            LibroDAO libroDAO = new LibroDAO();
            ViewBag.ListaLibrosBuscados = libroDAO.BuscarPorCategoria(idCategoria);
            ViewBag.IdCategoriaSelected = idCategoria;
            CategoriaDAO categoriaDAO = new CategoriaDAO();
            List<Categoria> listaCategorias = categoriaDAO.ListarTodo();
            return View(listaCategorias);
        }

        [HttpGet]
        public ActionResult Registrar() {
            return View(new CategoriaDAO().ListarTodo());
        }

        [HttpPost]
        public ActionResult Registrar(FormCollection formulario) {
            Libro libro = new Libro() {
                Titulo = formulario["txtTitulo"],
                Autor = formulario["txtAutor"],
                AnioPublicacion = Convert.ToInt32(formulario["nudAnio"]),
                Resumen = formulario["txaResumen"],
                NumPaginas = Convert.ToInt32(formulario["nudNumPaginas"]),
                Editorial = formulario["txtEditorial"],
                // Para registrar un libro solo se requiere el ID de la categoria
                Categoria = new Categoria() { Id = Convert.ToInt32(formulario["cmbCategoriaId"]) }
            };
            LibroDAO libroDAO = new LibroDAO();
            // Registrar el libro y obtener el ID generado
            int idGenerado = libroDAO.Registrar(libro);
            // Indicar si el registro fue exitoso o no para mostrar el mensaje correspondiente
            ViewBag.EsConforme = idGenerado > 0 ? true : false;
            // Mensaje de confirmacion o error en el registro del libro
            ViewBag.Mensaje = ViewBag.EsConforme ? $"Libro registrado exitosamente! (Libro_ID: {idGenerado})" : $"El libro {libro.Titulo} no se pudo registrar!";
            // Listar las categorias para mostrarlas en el combo
            return View(new CategoriaDAO().ListarTodo());
        }

        [HttpGet]
        public ActionResult Detalle(int id) {
            LibroDAO libroDAO = new LibroDAO();
            Libro libro = libroDAO.BuscarPorID(id);
            // Obtener el nombre de la categoria del libro
            string nombreCategoria = libroDAO.BuscarPorCategoria(libro.Categoria.Id)[0].Categoria.Nombre;
            // Asignar el nombre de la categoria al libro
            libro.Categoria.Nombre = nombreCategoria;
            return View(libro);
        }

        [HttpGet]
        public ActionResult Editar(int id) {
            LibroDAO libroDAO = new LibroDAO();
            Libro libro = libroDAO.BuscarPorID(id);
            ViewBag.Libro = libro;
            return View(new CategoriaDAO().ListarTodo());
        }

        [HttpPost]
        public ActionResult Editar(FormCollection formulario) {
            Libro libro = new Libro() {
                Id = Convert.ToInt32(formulario["txtId"]),
                Titulo = formulario["txtTitulo"],
                Autor = formulario["txtAutor"],
                AnioPublicacion = Convert.ToInt32(formulario["nudAnio"]),
                Resumen = formulario["txaResumen"],
                NumPaginas = Convert.ToInt32(formulario["nudNumPaginas"]),
                Editorial = formulario["txtEditorial"],
                // Para registrar un libro solo se requiere el ID de la categoria
                Categoria = new Categoria() { Id = Convert.ToInt32(formulario["cmbCategoriaId"]) }
            };
            LibroDAO libroDAO = new LibroDAO();
            ViewBag.EsConforme = libroDAO.Actualizar(libro);
            // Mensaje de confirmacion o error en el registro del libro
            ViewBag.Mensaje = ViewBag.EsConforme ? "Libro actualizado exitosamente!" : "Error al intentar actualizar el libro!";
            ViewBag.Libro = ViewBag.EsConforme ? libro : null;
            return View(new CategoriaDAO().ListarTodo());
        }

        [HttpGet]
        public ActionResult Eliminar(int id) {
            LibroDAO libroDAO = new LibroDAO();
            Libro libro = libroDAO.BuscarPorID(id);
            string nombreCategoria = libroDAO.BuscarPorCategoria(libro.Categoria.Id)[0].Categoria.Nombre;
            libro.Categoria.Nombre = nombreCategoria;
            return View(libro);
        }

        [HttpPost]
        public ActionResult Eliminar(FormCollection formulario) {
            int idLibro = Convert.ToInt32(formulario["txtIdLibro"]);
            LibroDAO libroDAO = new LibroDAO();
            bool esConforme = libroDAO.Eliminar(idLibro);
            TempData["EsConforme"] = esConforme;
            TempData["Mensaje"] = esConforme ? "Libro eliminado exitosamente!" : "Error al intentar eliminar el libro!";
            return RedirectToAction("Buscar", "Libro");
        }
    }
}