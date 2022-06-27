using FishMarket.Core.EntityFrameworkCore;
using FishMarket.DataAccess.Abstract;
using FishMarket.Entities.Concrete;

namespace FishMarket.DataAccess.Concrete.EntityFrameworkCore
{
    public class EFFishDal : EFRepository<Fish, FishMarketContext>, IFishDal
    {
    }
}
