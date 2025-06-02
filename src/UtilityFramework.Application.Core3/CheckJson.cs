using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UtilityFramework.Application.Core3.ViewModels;

namespace UtilityFramework.Application.Core3
{

    public class CheckJson : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var mapErrors = context.ModelState.Values.SelectMany(v => v.Errors).ToList();

                if (mapErrors.Count(x => x.Exception != null || Utilities.ContainsIgnoreCase(x.ErrorMessage, "error converting")) > 0)
                {
                    var errors = new Dictionary<string, string>();

                    for (int i = 0; i < mapErrors.Count(); i++)
                    {
                        try
                        {
                            var error = mapErrors[0];
                            var errorMessage = error.Exception?.Message ?? error.ErrorMessage;

                            if (string.IsNullOrEmpty(errorMessage) == false)
                            {
                                var parts = errorMessage
                                .Split(new string[] { "Path" }, StringSplitOptions.None)
                                .ToList();

                                if (parts.Count() > 1)
                                {
                                    var field = parts[1].Split(',')[0].Replace("Path", "").Replace("\'", "").ToString().Trim();
                                    if (errors.ContainsKey(field) == false)
                                    {
                                        var type = Regex.Replace(parts[0], @"(.*)( to )", "").Replace("'", "").Replace("type", "").Trim().TrimEnd('.').Split(':')[0];

                                        errors.Add(field, $"O valor do campo \"{field}\" é inválido, informe um '{type}'");
                                    }
                                }
                            }
                        }
                        catch (Exception) { /*unused*/ }
                    }

                    var response = new ReturnViewModel();

                    response.Erro = true;
                    response.Errors = errors;
                    response.Message = errors.FirstOrDefault().Value ?? "Json inválido, verifique os dados informados e tente novamente";

                    context.Result = new BadRequestObjectResult(response);
                }
            }
            catch (Exception) { /*unused*/ }
        }
    }
}