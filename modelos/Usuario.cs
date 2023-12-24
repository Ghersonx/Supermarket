using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATACHORI.modelos
{
    class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string genero { get; set; }

        public Usuario(int id, string nombre, string apellido, string genero)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.genero = genero;
        }
        public Usuario(string nombre, string apellido, string genero)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.genero = genero;
        }
        public Usuario()
        {

        }
    }
}
