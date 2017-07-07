using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class UpdateResultController : Controller
    {
        IUserDao userDao;

        public UpdateResultController(IUserDao userDao){
            this.userDao = userDao;
        }

        // POST UpdateResult/Post
        [HttpPost]
        public ResultBase Post([FromBody] StageRequest request)
        {

            ResultBase result = new ResultBase();

            User user = this.userDao.GetUser(request.UserID);

            user.Point = user.Point + request.Point;

            this.userDao.UpdateUser(user);

            result.ResultCode = 1;
            result.Message = "Success";

            return result;

        }

    }
}
