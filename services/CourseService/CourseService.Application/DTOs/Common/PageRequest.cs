using CourseService.Application.Constants;

namespace CourseService.Application.DTOs.Common;

public record PageRequest
{
    private readonly int _maxPageSize = PageConsts.MaxPageSize;
    private readonly int _pageSize = PageConsts.DefaultPageSize;
    private readonly int _pageNumber = PageConsts.DefaultPageNumber;

    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = Math.Max(PageConsts.DefaultPageNumber, value);
    }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value > _maxPageSize ? _maxPageSize
            : Math.Max(PageConsts.DefaultPageSize, value);
    }
}
