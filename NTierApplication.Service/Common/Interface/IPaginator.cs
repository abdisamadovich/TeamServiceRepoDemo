using NTierApplication.DataAccess.Utils;

namespace NTierApplication.Service.Common.Interface;

public interface IPaginator
{
    public PaginationMetaData Paginate(long itemsCount, PaginationParams @params);
}
