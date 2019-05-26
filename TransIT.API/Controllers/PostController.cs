using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class PostController : DataController<Post, PostDTO>
    {
        private readonly IPostService _postService;
        
        public PostController(
            IMapper mapper, 
            IPostService postService,
            IFilterService<Post> odService
            ) : base(mapper, postService, odService)
        {
            _postService = postService;
        }
    }
}
