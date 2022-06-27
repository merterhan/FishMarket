using FishMarket.Core;
using FishMarket.Entities.Concrete;

namespace FishMarket.DataAccess.Abstract
{
    public interface IUserDal : IRepository<User>
    {
        //custom operation like call stored procedure, or views, or join queries
    }
}
