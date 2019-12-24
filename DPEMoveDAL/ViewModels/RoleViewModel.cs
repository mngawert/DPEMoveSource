using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DPEMoveDAL.ViewModels
{
    public class RoleViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoleIdView { get; set; }
        public string RoleIdAdd { get; set; }
        public string RoleIdEdit { get; set; }
        public string RoleIdDelete { get; set; }
        public string RoleIdPrint { get; set; }
        public bool SelectedView { get; set; }
        public bool SelectedAdd { get; set; }
        public bool SelectedEdit { get; set; }
        public bool SelectedDelete { get; set; }
        public bool SelectedPrint { get; set; }
    }

    public class RoleDbQuery
    {
        [Column("ROLE_NAME")]
        public string Name { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("ROLE_ID_VIEW")]
        public string RoleIdView { get; set; }
        [Column("ROLE_ID_ADD")]
        public string RoleIdAdd { get; set; }
        [Column("ROLE_ID_EDIT")]
        public string RoleIdEdit { get; set; }
        [Column("ROLE_ID_DELETE")]
        public string RoleIdDelete { get; set; }
        [Column("ROLE_ID_PRINT")]
        public string RoleIdPrint { get; set; }
    }
}
