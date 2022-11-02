namespace SecondEgSA.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Satellite")]
    public partial class Satellite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Satellite()
        {
            Sat_Station = new HashSet<Sat_Station>();
            Subsystems = new HashSet<Subsystem>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sat_ID { get; set; }

        public int? Nat_ID { get; set; }

        [StringLength(30)]
        public string Sat_name { get; set; }

        public DateTime? Launch_date { get; set; }

        public int? Mass { get; set; }

        public short? Sat_type { get; set; }

        [StringLength(10)]
        public string Orbit_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sat_Station> Sat_Station { get; set; }

        public virtual Station Station { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subsystem> Subsystems { get; set; }
    }
}
