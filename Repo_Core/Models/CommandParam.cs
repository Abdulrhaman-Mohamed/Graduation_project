﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightControlCenter.Model1;

namespace Repo_Core.Models
{
    public class CommandParam
    {
        public int Id { get; set; }
        public int ParamTypeId { get; set; }
        public int CommandId { get; set; }

        public virtual ParamType ParamType { get; set; }
        public virtual Command Command { get; set; }
        public virtual List<ParamValue> ParamValues { get; set; } = new();


    }
}