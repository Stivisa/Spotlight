using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Listings
{
    public class DonationRecordSeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ListingDbContext context = app.ApplicationServices.GetRequiredService<ListingDbContext>();

            if (!context.DonationRecords.Any())
            {
                context.DonationRecords.AddRange(
                    new DonationRecord
                    {
                        value = 150,
                        When = new DateTime(2020, 6, 1, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 45,
                        When = new DateTime(2020, 2, 13, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 2900,
                        When = new DateTime(2020, 1, 14, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 350,
                        When = new DateTime(2019, 12, 1, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 780,
                        When = new DateTime(2020, 4, 17, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 12000,
                        When = new DateTime(2020, 5, 1, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 780,
                        When = new DateTime(2020, 4, 17, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 905,
                        When = new DateTime(2019, 10, 17, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 850,
                        When = new DateTime(2020, 3, 26, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 100,
                        When = new DateTime(2020, 7, 22, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 75,
                        When = new DateTime(2020, 6, 5, 12, 32, 30),
                    },
                    new DonationRecord
                    {
                        value = 890,
                        When = new DateTime(2020, 3, 17, 12, 32, 30),
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
