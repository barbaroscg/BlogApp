using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApp.Controllers
{
    
    public class PostsController : Controller
    {
        private readonly IPostRepository _postrepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository, ICommentRepository commentRepository)
        {
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
            _postrepository = postRepository;
        }
       
        public IActionResult Index(string tag)
        {
            var claims = User.Claims;
            //var model = _context.Posts.ToList();
            var posts = _postrepository.Posts;
            if(!string.IsNullOrEmpty(tag)){
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
                return View(posts.ToList());
            }
            else{
                // new PostViewModel
                // {
                //     Posts = _postrepository.Posts.ToList(),
                //     // Tags = _tagRepository.Tags.ToList()
                // };
                return View(posts.ToList());
            }
            
        }

        public async Task<IActionResult> Details(string? url)
        {
            return View(await _postrepository
            .Posts
            .Include(x => x.Tags)
            .Include(x => x.Comments)
            .ThenInclude( x => x.User)
            .FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string Text)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId ?? "")
            };
            _commentRepository.CreateComment(entity);
            return Json(new {
                username,
                Text,
                entity.PublishedOn,
                avatar
            });
        }

    }
}