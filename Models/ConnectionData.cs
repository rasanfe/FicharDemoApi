using SnapObjects.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FicharApi.Models
{
    [Table("ConnectionData", Schema = "dbo")]
    [UpdateWhereStrategy(UpdateWhereStrategy.KeyColumns)]
    public class ConnectionData
    {
        [Key]
        [SqlColumn("Id")]
        public int Id { get; set; }
        
        [SqlColumn("Data_Source")]
        public string Data_Source { get; set; }
        
        [SqlColumn("Initial_Catalog")]
        public string Initial_Catalog { get; set; }
        
        [SqlColumn("Integrated_Security")]
        public bool Integrated_Security { get; set; }
        
        [SqlColumn("User_ID")]
        public string User_ID { get; set; }
        
        [SqlColumn("Password")]
        public string Password { get; set; }
        
        [SqlColumn("Pooling")]
        public bool Pooling { get; set; }
        
        [SqlColumn("Min_Pool_Size")]
        public int Min_Pool_Size { get; set; }
        
        [SqlColumn("Max_Pool_Size")]
        public int Max_Pool_Size { get; set; }

        [SqlColumn("MultipleActiveResultSets")]
        public bool MultipleActiveResultSets { get; set; }

        [SqlColumn("Encrypt")]
        public bool Encrypt { get; set; }

        [SqlColumn("TrustServerCertificate")]
        public bool TrustServerCertificate { get; set; }

        [SqlColumn("ApplicationIntent")]
        public string ApplicationIntent { get; set; }

    }
}
