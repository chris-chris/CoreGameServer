using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class UpgradeController : Controller
    {

        IUpgradeDao upgradeDao;
        IUserDao userDao;

        public UpgradeController(IUpgradeDao upgradeDao, IUserDao userDao){
            this.upgradeDao = upgradeDao;
            this.userDao = userDao;
        }

        // GET Upgrade/Info
        [HttpGet]
        public UpgradeResult Info()
        {
            UpgradeResult result = new UpgradeResult();

            result.Data = this.upgradeDao.GetUpgradeInfo();

            result.ResultCode = 1;
            result.Message = "OK";

            return result;
        }

        // POST Upgrade/Execute
        [HttpPost]
        public ResultBase Execute([FromBody] UpgradeRequest request)
        {

            ResultBase result = new ResultBase();

            User user = this.userDao.GetUser(request.UserID);
            UpgradeData upgradeInfo = null;
            if("Health".Equals(request.UpgradeType)){
                upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.HealthLevel + 1);
            }else if("Damage".Equals(request.UpgradeType)){
                upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.HealthLevel + 1);
            }else if("Defense".Equals(request.UpgradeType)){
                upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.HealthLevel + 1);
            }else if("Speed".Equals(request.UpgradeType)){
                upgradeInfo = this.upgradeDao.GetUpgradeInfo(request.UpgradeType, user.HealthLevel + 1);
            }else{
                // 유효하지 않은 업그레이드 타입입니다.
            }

            // 다이아몬드가 있는지?
            if(user.Diamond < upgradeInfo.UpgradeCost){
                // TODO: 돈이 없어서 업그레이드 못해줌
                result.ResultCode = 5;
                result.Message = "Not Enough Diamond";
                return result;

            }

            if(upgradeInfo == null){
                // 최대 레벨에 도달했습니다.
                result.ResultCode = 4;
                result.Message = "Upgrade Fail : Max Level";
                return result;
            }

            if("Health".Equals(request.UpgradeType)){

                user.HealthLevel = user.HealthLevel + 1;
                user.Health = user.Health + upgradeInfo.UpgradeAmount;
                user.Diamond = user.Diamond - upgradeInfo.UpgradeCost;

            }else if("Damage".Equals(request.UpgradeType)){
                
                user.DamageLevel = user.DamageLevel + 1;
                user.Damage = user.Damage + upgradeInfo.UpgradeAmount;
                user.Diamond = user.Diamond - upgradeInfo.UpgradeCost;

            }else if("Defense".Equals(request.UpgradeType)){
                
                user.DefenseLevel = user.DefenseLevel + 1;
                user.Defense = user.Defense + upgradeInfo.UpgradeAmount;
                user.Diamond = user.Diamond - upgradeInfo.UpgradeCost;

            }else if("Speed".Equals(request.UpgradeType)){

                user.SpeedLevel = user.SpeedLevel + 1;
                user.Speed = user.Speed + upgradeInfo.UpgradeAmount;
                user.Diamond = user.Diamond - upgradeInfo.UpgradeCost;

            }

            this.userDao.UpdateUser(user);

            result.ResultCode = 1;
            result.Message = "Success";

            return result;

        }

    }
}
