//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace waSysColegio
{
    using System;
    using System.Collections.Generic;
    
    public partial class Personal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personal()
        {
            this.Curso = new HashSet<Curso>();
        }
    
        public int ID_Personal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public System.DateTime Fecha_Nacimiento { get; set; }
        public string DNI { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public byte[] Firma { get; set; }
        public byte[] Sello { get; set; }
        public string Estado_Registro { get; set; }
        public Nullable<int> ID_Tipo_Personal { get; set; }
        public Nullable<int> ID_Genero { get; set; }
        public Nullable<int> ID_Grado { get; set; }
        public Nullable<int> ID_Seccion { get; set; }
        public Nullable<int> ID_Usuario { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Curso> Curso { get; set; }
        public virtual Genero Genero { get; set; }
        public virtual Grado Grado { get; set; }
        public virtual Seccion Seccion { get; set; }
        public virtual Tipo_Personal Tipo_Personal { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
