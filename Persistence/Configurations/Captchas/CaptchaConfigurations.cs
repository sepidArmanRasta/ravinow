using Domain.Entities.Captchas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.Captchas
{
    public class CaptchaConfigurations : IEntityTypeConfiguration<Captcha>
    {
        public void Configure(EntityTypeBuilder<Captcha> builder)
        {
            builder.HasQueryFilter(x => !EF.Property<bool>(x, "IsRemove"));
        }
    }
}