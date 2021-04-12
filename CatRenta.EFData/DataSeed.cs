using System;
using System.Collections.Generic;
using System.Text;
using CatRenta.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace CatRenta.EFData
{
    public class DataSeed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.Cats.Any())
            {
                var cats = new List<AppCat>
                                {
                                    new AppCat
                                        {
                                            Name = "Барсік",
                                            Gender=true,
                                            Birthday=new DateTime(2018, 5,23),
                                            Details="Стан критичний",
                                            Image="https://ichef.bbci.co.uk/news/800/cpsprodpb/14236/production/_104368428_gettyimages-543560762.jpg"
                                        },

                                };

                foreach (var cat in cats)
                {
                    context.Add(cat);
                    context.SaveChanges();
                }
            }
            if (!context.CatPrices.Any())
            {
                var cat = context.Cats.FirstOrDefault();
                var catPrices = new List<AppCatPrice>
                                {
                                    new AppCatPrice
                                        {
                                            CatId=cat.Id,
                                            DateCreate=DateTime.Now,
                                            Price=450.23M
                                        },
                                    new AppCatPrice
                                        {
                                            CatId=cat.Id,
                                            DateCreate=new DateTime(2021,1,14),
                                            Price=250.75M
                                        }
                                };

                foreach (var price in catPrices)
                {
                    context.Add(price);
                    context.SaveChanges();
                }
            }

        }
        public static Task SeedDataAsync(DataContext context)
        {
            return Task.Run(() => SeedData(context));

        }
    }
}
