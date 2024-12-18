using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder) //TODO: hard coded limits becomes duplicated when fluent validation also used and validators set limits for the request models. Solution?
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.FirstName).IsRequired()
            .HasMaxLength(EntityConstraints.User.FirstNameMaxLength);

        builder.Property(p => p.LastName).IsRequired()
            .HasMaxLength(EntityConstraints.User.LastNameMaxLength);

        builder.Property(p => p.Email).IsRequired()
            .HasMaxLength(255); //max rfc limit.
                                //There are some discussions around here so can be set to a higher value like 320
                                //https://www.rfc-editor.org/rfc/rfc3696
                                //https://stackoverflow.com/questions/386294/what-is-the-maximum-length-of-a-valid-email-address
    }
}
