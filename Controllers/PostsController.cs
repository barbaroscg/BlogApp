using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogApp.Controllers
{
    
    public class PostsController : Controller
    {
        private readonly IPostRepository _postrepository;
        private readonly ITagRepository _tagRepository;
       
        public PostsController(IPostRepository postRepository, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            _postrepository = postRepository;
        }
       
        public IActionResult Index()
        {
            //var model = _context.Posts.ToList();
            return View(
                new PostViewModel
                {
                    Posts = _postrepository.Posts.ToList(),
                    // Tags = _tagRepository.Tags.ToList()
                }
            );
        }

    }
}