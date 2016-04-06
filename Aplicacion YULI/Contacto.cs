using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion_YULI
{
    public class Contacto
    {
        private string paginaWeb;
        private string telefono;
        private string celular;
        private string pais;
        private string ciudad;
        private string direccion;
        private string eMail;
        private string facebook;

        public Contacto(string paginaWeb, string telefono, string celular, string pais, string ciudad, string direccion, string eMail, string facebook)
        {
            this.paginaWeb = paginaWeb;
            this.telefono = telefono;
            this.celular = celular;
            this.pais = pais;
            this.ciudad = ciudad;
            this.direccion = direccion;
            this.eMail = eMail;
            this.facebook = facebook;
        }
    }
}
