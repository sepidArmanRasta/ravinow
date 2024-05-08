using Application.Contexts;
using Application.Dtos._HTTP;
using Application.Services.Contents.Dto;
using Application.Tools;
using Domain.Entities.Contents;
using Domain.Enumeration.Content;
using System.Net;

namespace Application.Services.Contents
{
    public class ContentService(IDataBaseContext dataBaseContext) : IContentService
    {
        public ResponseDto<List<ContentDto>> GetList(List<string>? contentSources)
        {
            try
            {
                var result = new ResponseDto<List<ContentDto>>();

                var entity = dataBaseContext.Contents.AsQueryable();

                if (contentSources != null)
                {
                    entity = entity.Where(c => contentSources.Contains(c.Source)).AsQueryable();
                }

                var mappedData = MappingHelper.Map<List<ContentDto>>(entity.ToList());
                result.Data = mappedData;
                result.HttpStatusCode = HttpStatusCode.OK;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public ResponseDto<ContentDto> GetItem(Guid? Id)
        {
            try
            {
                var result = new ResponseDto<ContentDto>();

                var entity = dataBaseContext.Contents.AsQueryable();

                if (Id != null)
                {
                    entity = entity.Where(c => c.Id == Id).AsQueryable();
                }

                var mappedData = MappingHelper.Map<ContentDto>(entity.FirstOrDefault());
                result.Data = mappedData;
                result.HttpStatusCode = HttpStatusCode.OK;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public ResponseDto<ContentDto> Insert(ContentDto contentDto)
        {
            var result = new ResponseDto<ContentDto>();

            var items = dataBaseContext.Contents.Where(c => c.Source == contentDto.Source).ToList();
            dataBaseContext.Contents.RemoveRange(items);

            var model = new Content
            {
                Id = contentDto.Id,
                Source = contentDto.Source,
                CallBackURL = contentDto.CallBackURL,
                ContentType = contentDto.ContentType,
                Description = contentDto.Description
            };

            dataBaseContext.Contents.Add(model);
            dataBaseContext.SaveChanges();

            result.HttpStatusCode = HttpStatusCode.Created;
            result.Data = MappingHelper.Map<ContentDto>(model);
            return result;
        }
    }
}
