using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        IUserDao userDao;
        public UserController(IUserDao userDao){
            this.userDao = userDao;
        }

        // // GET api/user
        // [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // GET User/Inf
        [HttpGet]
        public UserResult Info(int UserID)
        {
            UserResult result = new UserResult();
            result.Data = userDao.GetUser(UserID);
            result.ResultCode = 1;
            result.Message = "OK";
            return result;
        }

    }
}
