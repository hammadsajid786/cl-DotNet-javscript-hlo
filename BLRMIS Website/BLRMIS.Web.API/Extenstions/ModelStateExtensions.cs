using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLRMIS.Web.API.Extenstions
{
    public static class ModelStateExtensions
    {
        #region Public Methods

        public static Dictionary<string, string> AllErrors(this ModelStateDictionary modelState)
        {
            var result = new Dictionary<string, string>();
            foreach (var erroneousField in modelState.Where(ms => ms.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors }))
            {
                var fieldKey = erroneousField.Key;
                foreach (var error in erroneousField.Errors)
                {
                    result[fieldKey] = error.ErrorMessage;
                }
            }

            return result;
        }

        #endregion Public Methods
    }
}
