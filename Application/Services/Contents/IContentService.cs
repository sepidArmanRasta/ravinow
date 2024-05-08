using Application.Dtos._HTTP;
using Application.Services.Contents.Dto;
using Domain.Enumeration.Content;

namespace Application.Services.Contents
{
    public interface IContentService
    {
        ResponseDto<List<ContentDto>> GetList(List<string>? contentSources);
        ResponseDto<ContentDto> GetItem(Guid? Id);
        ResponseDto<ContentDto> Insert(ContentDto contentDto);
    }
}
