using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.DAL.Interfaces
{
    public interface IBannedIPRepository
    {
        public ResultInfo<List<BannedIP>> GetBannedIPs();
        public ResultInfo<int> NewBannedIP(string ip);
    }
}