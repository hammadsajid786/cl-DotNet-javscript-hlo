using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public partial class PagingModel
    {

        #region Public Properties

        public bool Ascending { get; set; } = true;

        [RegularExpression(@"[a-zA-Z]+$", ErrorMessage = "Order By field has invalid characters.")]
        public string OrderBy { get; set; }

        [Range(1, 100)]
        public int? Page { get; set; }

        [Range(1, 1000)]
        public int? PageSize { get; set; }

        #endregion Public Properties
    }

}
