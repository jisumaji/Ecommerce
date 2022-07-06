using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Model.Dto;
using Ecommerce.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MyUserController : Controller
    {
        private readonly IMyUserService _iMyUserService;
        public MyUserController(IMyUserService _iMyUserService)
        {
            this._iMyUserService = _iMyUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MyUser myUser)
        {
            await _iMyUserService.AddUserAsync(myUser);
            return Created($"MyUser", myUser);
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(string userid)
        {
            try
            {
                var myuser = await _iMyUserService.GetMyUserAsync(userid);

                if (myuser == null)
                {
                    return NotFound();
                }

                return Ok(myuser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
//mukul changes