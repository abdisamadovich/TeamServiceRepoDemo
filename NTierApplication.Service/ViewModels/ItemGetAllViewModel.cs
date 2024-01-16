using NTierApplication.DataAccess.Utils;

namespace NTierApplication.Service.ViewModels;

public class ItemGetAllViewModel
{
    public ICollection<ItemViewModel> Items { get; set; }
    public PaginationMetaData PaginationMetaData { get; set; }
}
