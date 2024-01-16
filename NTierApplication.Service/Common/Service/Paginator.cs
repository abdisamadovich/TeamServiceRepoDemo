using Microsoft.AspNetCore.Http;
using NTierApplication.DataAccess.Utils;
using NTierApplication.Service.Common.Interface;

namespace NTierApplication.Service.Common.Service;

public class Paginator : IPaginator
{
    public PaginationMetaData Paginate(long itemsCount, PaginationParams @params)
    {
        PaginationMetaData paginationMetaData = new PaginationMetaData();
        paginationMetaData.CurrentPage = @params.PageNumber;
        paginationMetaData.TotalItems = (int)itemsCount;
        paginationMetaData.PageSize = @params.PageSize;

        paginationMetaData.TotalPages = (int)Math.Ceiling((double)itemsCount / @params.PageSize);
        paginationMetaData.HasPrevious = paginationMetaData.CurrentPage > 1;
        paginationMetaData.HasNext = paginationMetaData.CurrentPage < paginationMetaData.TotalPages;

        return paginationMetaData;
    }
}
