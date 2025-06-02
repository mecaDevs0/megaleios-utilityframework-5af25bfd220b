using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3;

namespace UtilityFramework.WebApi.Core3.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {

        [NonAction]
        public async Task<IActionResult> HandleActionAsync<T>(Func<Task<T>> action,
                                                                      string message = null)
                                                                      where T : class, new()
        {
            try
            {
                var data = await action();

                return Ok(Utilities.ReturnSuccess(message, data));
            }
            catch (CustomError ex)
            {
                return BadRequest(Utilities.ReturnErro(ex.Message));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }
        [NonAction]
        public async Task<IActionResult> HandleActionAsync(Func<Task> action,
                                                              string message = null)

        {
            try
            {
                await action();

                return Ok(Utilities.ReturnSuccess(message));
            }
            catch (CustomError ex)
            {
                return BadRequest(Utilities.ReturnErro(ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ReturnErro());
            }
        }

    }
}
