using NTierApplication.DataAccess.Utils;
using NTierApplication.Service.ViewModels;

namespace NTierApplication.Service
{
    public interface IItemService
    {
        void CreateNew(ItemViewModel item);
        void Update(ItemViewModel item);
        void Delete(long itemId);
        ItemViewModel GetById(long id);
        ItemGetAllViewModel GetItems(PaginationParams @params);
    }
}
