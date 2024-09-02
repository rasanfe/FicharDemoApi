using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FicharApi.Models
{
    [Table("Usuarios_mobile", Schema = "dbo")]
    [SqlParameter("usuario", typeof(string))]
    [SqlParameter("password", typeof(string))]
    [SqlWhere("(usuario = :usuario)")]
    [SqlAndWhere("(pin = :password)")]
    public class Usuarios
    {
        [Key]
        [SqlColumn("usuario")]
        public string? Usuario { get; set; }
        
        [SqlColumn("pin")]
        public string? Pin { get; set; }
        
        [SqlColumn("empresa")]
        public string? Empresa { get; set; }
        
        [SqlColumn("empleado")]
        public string? Empleado { get; set; }

        [SqlColumn("grupo")]
        public string? Grupo { get; set; }


        [SqlColumn("activo")]
        public bool? Activo { get; set; }

        [NotMapped]
        public string? ConnectionID { get; set; }


        [NotMapped]
        public bool IsValid { get; set; }

        [NotMapped]
        public string? Token { get; set; }

        [NotMapped]
        public DateTime Expiration { get; set; }

    }

}



