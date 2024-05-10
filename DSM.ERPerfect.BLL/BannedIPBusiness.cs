using DSM.ERPerfect.BLL.Interfaces;
using DSM.ERPerfect.DAL.Interfaces;
using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL
{
    public class BannedIPBusiness : IBannedIPBusiness
    {
        private IBannedIPRepository _bannedIPRepository { get; init; }

        public BannedIPBusiness(IBannedIPRepository bannedIPRepository)
        {
            _bannedIPRepository = bannedIPRepository;
        }

        #region Métodos públicos

        public ResultInfo<List<BannedIP>> GetBannedIPs()
        {
            return _bannedIPRepository.GetBannedIPs();
        }

        public ResultInfo<int> NewBannedIP(string ip)
        {
            return _bannedIPRepository.NewBannedIP(ip);
        }

        #endregion

        #region Métodos privados



        #endregion

    }
}
