using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DPEMoveDAL.ViewModels
{
    public class ReportEvent1DbQuery
    {
        [Column("PROV_CODE")]
        public string ProvCode { get; set; }
        [Column("PROV_NAMT")]
        public string ProvNamt { get; set; }
        [Column("NO_OF_EVENTS")]
        public int NoOfEvents { get; set; }
    }

}
