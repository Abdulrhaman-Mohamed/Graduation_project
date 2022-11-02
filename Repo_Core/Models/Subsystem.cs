namespace SecondEgSA.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Subsystem")]
    public partial class Subsystem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sub_ID { get; set; }

        public int? Sat_ID { get; set; }

        [StringLength(30)]
        public string Sub_name { get; set; }

        [StringLength(30)]
        public string Sub_type { get; set; }

        public virtual Satellite Satellite { get; set; }
    }
}
