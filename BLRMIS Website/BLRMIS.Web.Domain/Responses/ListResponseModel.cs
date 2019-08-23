using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class ListResponseModel<TModel>
    {
       // public int TotalRecords { get; set; }

        public List<TModel> Records { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        [JsonIgnore]
        public ErrorResponseModel Error { get; set; }

        [JsonIgnore]
        public bool IsError { get; set; }
    }
}
