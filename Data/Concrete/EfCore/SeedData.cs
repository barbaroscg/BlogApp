using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }


                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text = "web-programlama", Url="web-programlama", Color = TagColors.secondary},
                        new Tag { Text = "back-end", Url="back-end", Color = TagColors.warning},
                        new Tag { Text = "front-end", Url="front-end", Color=TagColors.success},
                        new Tag { Text = "full-stack", Url="full-stack", Color = TagColors.primary},
                        new Tag { Text = "react", Url="react", Color= TagColors.info}
                    );
                    context.SaveChanges();
                }
                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User {UserName = "bciga", Image = "p1.jpg", Name="Barbaros CİGA", Email="bciga@gmail.com", Password="123456"},
                        new User {UserName = "etkin", Image="p2.jpg", Name="Erkin Tekin", Email="e.tekin@gmail.com", Password="123456"}
                    );
                    context.SaveChanges();
                }
                if(!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post { 
                            Title = "Asp.Net Core",
                            Content = "Asp.Net Core Dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "1.jpg",
                            UserId = 1,
                            Url = "asp-net-core",
                            Comments = new List<Comment> {
                                new Comment {Text = "Ben bu kurstan çok faydalandım. Ürün beklediğim gibi çıktı", PublishedOn = DateTime.Now.AddMonths(-5).AddHours(4), UserId = 1},
                                 new Comment {Text = "Ben çok anlamadım", PublishedOn = DateTime.Now.AddDays(-1), UserId = 2}
                            }
                        },
                         new Post { 
                            Title = "React Js",
                            Content = "React Dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "2.jpg",
                            UserId = 1,
                            Url = "react-js"
                        },
                         new Post { 
                            Title = "Mongo Db",
                            Content = "Mongo Db Dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            Image = "3.jpg",
                            UserId = 2,
                            Url = "mongo-db"
                        },
                        new Post { 
                            Title = "Ms SQL",
                            Content = "MS SQL Dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-4),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "1.jpg",
                            UserId = 1,
                            Url = "asp-net-core"
                        },
                         new Post { 
                            Title = "React Native",
                            Content = "React Native Dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-1),
                            Tags = context.Tags.Take(5).ToList(),
                            Image = "2.jpg",
                            UserId = 1,
                            Url = "react-native"
                        },
                         new Post { 
                            Title = "Python",
                            Content = "Python Dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "3.jpg",
                            UserId = 2,
                            Url = "python"
                        }
                        
                    );
                    context.SaveChanges();
                }
                
            }
        }
    }
}