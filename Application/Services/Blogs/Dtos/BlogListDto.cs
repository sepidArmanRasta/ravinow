using Application.Services.BlogCategories.Dtos;
using Application.Services.Users.Dtos;
using MD.PersianDateTime.Standard;

namespace Application.Services.Blogs.Dtos
{
    public class BlogListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public DateTime InsertTime { get; set; }
        public string PersianInsertTime
        {
            get
            {
                return new PersianDateTime(InsertTime).ToString("yyyy/MM/dd");
            }
        }
        public DateTime? UpdateTime { get; set; }
        public string PersianUpdateTime
        {
            get
            {
                if (UpdateTime != null)
                    return new PersianDateTime(UpdateTime).ToString("yyyy/MM/dd");
                return "بدون تغییر";
            }
        }
        public UserDto? Author { get; set; }
        public List<BlogCategoriesItemDto>? Categories { get; set; }
    }
}
