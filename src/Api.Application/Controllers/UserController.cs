using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Api.Domain.Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
    //http://localhost:5000/api/v1/user
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> Find()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400 bad request - solicita��o inv�lida
            }

            try
            {
                //List<Expression<Func<UserDtoFind, object>>> expressions = new List<Expression<Func<UserDtoFind, object>>>();
                //var properties = typeof(UserDtoFind).GetProperties();

                //foreach (var property in properties)
                //{
                //    Expression<Func<UserDtoFind, object>> x = u =>
                //    (
                //         u.GetType().InvokeMember(property.Name, BindingFlags.GetProperty, null, u, null)
                //    );

                //    expressions.Add(x);
                //}

                //var exp = Expression.Parameter(typeof(UserDtoFind), "filtro");

                //Expression<Func<UserDtoFind, bool>> predicate = a => a.Name = user.Name || a.Email = user.Email;


                return Ok(await _service.FindAsync().ConfigureAwait(true));

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //[Authorize("Bearer")]
        //[HttpGet]
        //public async Task<ActionResult> Find(string Name, string Email)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); //400 bad request - solicita��o inv�lida
        //    }

        //    try
        //    {
        //        //List<Expression<Func<UserDtoFind, object>>> expressions = new List<Expression<Func<UserDtoFind, object>>>();
        //        //var properties = typeof(UserDtoFind).GetProperties();

        //        //foreach (var property in properties)
        //        //{
        //        //    Expression<Func<UserDtoFind, object>> x = u =>
        //        //    (
        //        //         u.GetType().InvokeMember(property.Name, BindingFlags.GetProperty, null, u, null)
        //        //    );

        //        //    expressions.Add(x);
        //        //}

        //        //var exp = Expression.Parameter(typeof(UserDtoFind), "filtro");

        //        //Expression<Func<UserDtoFind, bool>> predicate = a => a.Name = user.Name || a.Email = user.Email;


        //        return Ok(await _service.FindAsync().ConfigureAwait(true));

        //    }
        //    catch (ArgumentException e)
        //    {

        //        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult> Find(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.FindAsync(id).ConfigureAwait(true));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var result = await _service.InsertAsync(user).ConfigureAwait(true);

                if (result != null)
                {
                    return Created(new Uri(Url.Link("FindtWithId", new { id = result.Id })), result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UserDtoUpdate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _service.UpdateAsync(user).ConfigureAwait(true);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.DeleteAsync(id).ConfigureAwait(true));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}
