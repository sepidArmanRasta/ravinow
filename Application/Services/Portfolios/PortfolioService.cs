using Application.Contexts;
using Application.Dtos._HTTP;
using Application.Services.Blogs.Dtos;
using Application.Services.UploadImages;
using Application.Tools;
using Domain.Entities.Blogs;
using Domain.Entities.Portfolios;
using System.Net;

namespace Application.Services.Blogs
{
    public class PortfolioService(IDataBaseContext dataBaseContext, IUploadImageService uploadImageService) : IPortfolioService
    {
        public async Task<ResponseDto<bool>> Delete(Guid guid)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var item = dataBaseContext.Portfolios.Find(guid);

                if (item != null)
                {
                    dataBaseContext.Portfolios.Remove(item);
                    await dataBaseContext.SaveChangesAsync();
                }

                result.HttpStatusCode = HttpStatusCode.OK;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<BlogListDto>> GetList()
        {
            try
            {
                var entity = dataBaseContext.Portfolios.OrderBy(p => p.InsertTime).ToList();
                var mappedData = MappingHelper.Map<List<BlogListDto>>(entity);
                await Task.CompletedTask;
                return mappedData;
            }
            catch
            {
                throw;
            }


        }

        public async Task<ResponseDto<bool>> Insert(InsertPortfolioDto insertPortfolioDto)
        {
            var result = new ResponseDto<bool>();

            try
            {
                var spliter = ",";
                var photoCorrection = insertPortfolioDto.Photo[(insertPortfolioDto.Photo.IndexOf(spliter) + 1)..];
                byte[] byteArray = Convert.FromBase64String(photoCorrection);
                var imageUri = uploadImageService.UploadImage(byteArray, insertPortfolioDto.FileName);

                var itemExists = dataBaseContext.Blogs.Any(b => b.Title == insertPortfolioDto.Title);

                if (itemExists)
                {
                    result.HttpStatusCode = HttpStatusCode.BadRequest;
                    return result;
                }
                else
                {
                    var item = new Portfolio
                    {
                        Photo = imageUri?.Replace("\\", "/")!,
                        Title = insertPortfolioDto.Title,
                        InsertTime = DateTime.Now
                    };

                    dataBaseContext.Portfolios.Add(item);
                    await dataBaseContext.SaveChangesAsync();

                    result.HttpStatusCode = HttpStatusCode.Created;
                    result.Data = true;
                    return result;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}