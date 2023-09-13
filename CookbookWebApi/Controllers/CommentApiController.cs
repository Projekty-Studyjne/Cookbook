using CookbookBLL;
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
        public IEnumerable<CommentResponse> GetAll()
        {
            //IEnumerable<Comment> comments = new List<Comment>();
            //comments = _commentService.GetAll().Result;
            //return comments;
            return _commentService.GetAll().Result.Select(x => new CommentResponse(x.commentId, x.comment, x.ratingId));
        }

        [HttpGet("{id}")]
        public CommentResponse GetOne(int id)
        {
            Comment comment = null;
            comment = _commentService.GetCommentById(id).Result;
            if (comment != null)
            {
                return new CommentResponse(comment.commentId, comment.comment, comment.ratingId);
            }
            return null;
        }

        [HttpGet("/CommentApi/ByRating/ratingId")]
        public IEnumerable<CommentResponse> GetCommentByRating(int ratingId)
        {
            return _commentService.GetCommentByRating(ratingId).Result.Select(x => new CommentResponse(x.commentId, x.comment, x.ratingId));
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            if (_commentService.Delete(id).IsCompletedSuccessfully)
                return true;
            return false;
        }

        [HttpPost]
        public bool Post([FromBody]CommentRequest commentReq)
        {
            Comment comment = new Comment();
            comment.comment=commentReq.comment;
            comment.ratingId=commentReq.ratingId;
            if (_commentService.Add(comment).IsCompletedSuccessfully)
            {
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool Put(int id,[FromBody]CommentRequest commentReq)
        {
            Comment? comment = _commentService.GetCommentById(id).Result;
            if (comment != null)
            {
                comment.comment=commentReq.comment;
                comment.ratingId = commentReq.ratingId;
                if (_commentService.Update(comment).IsCompletedSuccessfully)
                    return true;
            }
            return false;
        }

    }
}
