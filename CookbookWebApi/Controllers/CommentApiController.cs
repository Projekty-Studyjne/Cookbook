using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CookbookWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentApiController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentApiController(ICommentService commentService)
        {
            this._commentService = commentService;
        }
        [HttpGet]
        public IEnumerable<Comment> GetAll()
        {
            IEnumerable<Comment> comments = new List<Comment>();
            comments = _commentService.GetAll().Result;
            return comments;
        }

        [HttpGet("{id}")]
        public Comment GetOne(int id)
        {
            Comment comment = null;
            comment = _commentService.GetCommentById(id).Result;
            return comment;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_commentService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post(Comment comment)
        {
            if (_commentService.Add(comment).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(Comment comment)
        {
            if (_commentService.Update(comment).IsCompletedSuccessfully)
                return true;
            return false;
        }

    }
}
