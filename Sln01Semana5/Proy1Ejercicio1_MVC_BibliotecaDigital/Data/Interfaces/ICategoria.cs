using Proy1Ejercicio1_MVC_BibliotecaDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy1Ejercicio1_MVC_BibliotecaDigital.Data.Interfaces {
    internal interface ICategoria {
        List<Categoria> ListarTodo();
    }
}
