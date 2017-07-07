using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class RankController : Controller
    {

        IRankDao rankDao;
        public RankController(IRankDao rankDao){
            this.rankDao = rankDao;
        }

        // POST Rank/Total
        [HttpGet]
        public RankResult Total(int Start, int Count)
        {

            RankResult result = new RankResult();
            
            List<RankUser> list = rankDao.TotalRank(Start, Count);
            
            result.Data = list;

            return result;

        }

        // POST Rank/Friend
        [HttpPost]
        public RankResult Friend([FromBody] RankRequest rankRequest)
        {

            RankResult result = new RankResult();
            
            List<RankUser> list = rankDao.FriendRank(rankRequest.FriendList);
            
            result.Data = list;

            return result;

        }

    }
}
