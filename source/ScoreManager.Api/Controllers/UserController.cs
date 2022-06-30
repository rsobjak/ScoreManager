using ScoreManager.Data;
using ScoreManager.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ScoreManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : FooController<User>
    {

        public UserController(ILogger<UserController> logger, IUser dal) : base(logger, dal)
        {
        }

        ///// <summary>
        ///// Get all users
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet(Name = "GetAllUsers")]
        //public async Task<IEnumerable<User>> Get()
        //{
        //    var data = await _dal.GetAllAsync();
        //    return data;
        //}

        ///// <summary>
        ///// Get user
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("{id}", Name = "GetUser")]
        //public async Task<User> Get(int id)
        //{
        //    var data = await _dal.GetByIdAsync(id);
        //    return data;
        //}

        ///// <summary>
        ///// Insert new user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpPost(Name = "InsertUser")]
        //public async Task Post(User user)
        //{
        //    await _dal.InsertAsync(user);
        //}

        ///// <summary>
        ///// Update User
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpPut("{id}", Name = "Update")]
        //public async Task Put(int id, User user)
        //{
        //    await _dal.UpdateAsync(id, user);
        //}

        ///// <summary>
        ///// Delete user
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpDelete(Name = "DeleteUser")]
        //public async Task Delete(int id)
        //{
        //    await _dal.RemoveAsync(id);
        //}
    }
}