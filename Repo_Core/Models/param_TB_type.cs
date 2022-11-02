namespace SecondEgSA.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    public partial class param_TB_type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public param_TB_type()
        {
            CoM_Param = new HashSet<CoM_Param>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int param_ID { get; set; }

        [StringLength(60)]
        public string param_type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoM_Param> CoM_Param { get; set; }
    }
}
