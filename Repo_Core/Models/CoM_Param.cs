namespace SecondEgSA.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public partial class CoM_Param
    {
        public CoM_Param() => Param_Value = new HashSet<Param_Value>();

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int param_ID { get; set; }

        
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int com_id { get; set; }

        
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sub_Id { get; set; }

        public int? param_type { get; set; }

        public virtual Command Command { get; set; }

        public virtual param_TB_type param_TB_type { get; set; }

        
        public virtual ICollection<Param_Value> Param_Value { get; set; }
    }
}
