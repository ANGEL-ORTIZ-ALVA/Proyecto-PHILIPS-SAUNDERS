using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace waSysColegio.Validations
{
    [MetadataType(typeof(SeccionMetadata))]
    public partial class Seccion
    {
        // Esta clase permanece vacía.
        // Las validaciones se definen en SeccionMetadata.
    }

    public class SeccionMetadata
    {
        [Required(ErrorMessage = "El nombre de la sección es obligatorio.")]
        [StringLength(30, ErrorMessage = "El nombre no puede exceder los 30 caracteres.")]
        public string Nombre_Seccion { get; set; }

        [Required(ErrorMessage = "El aforo de la sección es obligatorio.")]
        [Range(15, 30, ErrorMessage = "El aforo debe estar entre 15 y 30.")]
        public int Aforo { get; set; }

        [Required(ErrorMessage = "El estado de registro es obligatorio.")]
        [RegularExpression("^(Registrado|Eliminado)$", ErrorMessage = "Estado no válido.")]
        public string Estado_Registro { get; set; }
    }
}