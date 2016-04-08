using Newtonsoft.Json;
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
            this.paginaWeb = paginaWeb.Equals("")?"No definido":paginaWeb;
            this.telefono = telefono.Equals("")?"No definido":telefono;
            this.celular = celular.Equals("")?"No definido":celular;
            this.pais = pais.Equals("")?"No definido":pais;
            this.ciudad = ciudad.Equals("")?"No definido":ciudad;
            this.direccion = direccion.Equals("")?"No definido":direccion;
            this.eMail = eMail.Equals("")?"No definido":eMail;
            this.facebook = facebook.Equals("")?"No definido":facebook;
        }

        public string DarContacto()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string PaginaWeb
        {
            get { return paginaWeb; }
        }

        public string Telefono
        {
            get { return telefono; }
        }

        public string Celular
        {
            get { return celular; }
        }

        public string Pais
        {
            get { return pais; }
        }

        public string Ciudad
        {
            get { return ciudad; }
        }

        public string Direccion
        {
            get { return direccion; }
        }

        public string EMail
        {
            get { return eMail; }
        }

        public string Facebook
        {
            get { return facebook; }
        }
    }
}
