using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Spotlight.Models.News
{
    public class NewsSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) {
            NewsDbContext context = app.ApplicationServices.GetRequiredService<NewsDbContext>();

            if(!context.AllNewsPosts.Any()) {
                context.AllNewsPosts.AddRange(
                    new NewsPost { PostHeadline = "This is 1st piece of news", PostContent = "Cogito ergo sum.", TimeOfPosting = new DateTime(2020, 6, 1, 12, 32, 30) },
                    new NewsPost { PostHeadline = "This is 3rd piece of news", PostContent = "Per aspera ad astra.", TimeOfPosting = new DateTime(2013, 11, 11, 12, 32, 30) },
                    new NewsPost { PostHeadline = "This is 2nd piece of news", PostContent = "Consuetudinis magna vis est.", TimeOfPosting = new DateTime(2015, 2, 2, 12, 32, 30) }
                );

                context.SaveChanges();
            }
        }

    }
}
