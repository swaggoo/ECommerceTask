namespace ECommerce.Services.Helpers.Pagination;

public record PaginationHeader(int CurrentPage, int TotalPages, int PageSize, int TotalCount);