using DSM.ERPerfect.Models.Entities;
using DSM.ERPerfect.Models.Errors;

namespace DSM.ERPerfect.BLL.Interfaces
{
    public interface IBannedIPBusiness
    {
        public ResultInfo<List<BannedIP>> GetBannedIPs();
        public ResultInfo<int> NewBannedIP(string ip);
    }
}