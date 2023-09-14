using CookbookBLL;
using CookbookLibrary.Entities;
using CookbookLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectBLL
{
    public class UnitTestCommentsBLL
    {
        [Fact]
        public void TestGetAll()
        {
            var fakeRepository = new CommentRepoFake();
            var unitOfWork = new TestUnitOfWork(fakeRepository);
            var commentService = new CommentService(unitOfWork);
            var result = commentService.GetAll().Result;
            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void TestGetCommentById()
        {
            var fakeRepository = new CommentRepoFake();
            var unitOfWork = new TestUnitOfWork(fakeRepository);
            var commentService = new CommentService(unitOfWork);
            var commentId = 1;
            var fakeComment = new Comment { commentId = commentId, comment = "Comment 1", ratingId = 1 };
            fakeRepository.Insert(fakeComment);
            var result = commentService.GetCommentById(commentId).Result;
            Assert.NotNull(result);
            Assert.Equal("Comment 1", result.comment);
        }

        [Fact]
        public void TestUpdate()
        {
            var fakeRepository = new CommentRepoFake();
            var unitOfWork = new TestUnitOfWork(fakeRepository);
            var commentService = new CommentService(unitOfWork);
            var commentId = 1;
            var fakeComment = new Comment { commentId = commentId, comment = "Comment 1", ratingId = 1 };
            fakeRepository.Insert(fakeComment);
            var updatedComment = new Comment { commentId = commentId, comment = "Updated Comment", ratingId = 1 };
            commentService.Update(updatedComment);
            var result = fakeRepository.GetByID(commentId);
            Assert.NotNull(result);
            Assert.Equal("Updated Comment", result.comment);
        }

        [Fact]
        public void TestAdd()
        {
            var fakeRepository = new CommentRepoFake();
            var unitOfWork = new TestUnitOfWork(fakeRepository);
            var commentService = new CommentService(unitOfWork);
            var newComment = new Comment { commentId = 2, comment = "New Comment", ratingId = 2 };
            commentService.Add(newComment);
            var result = fakeRepository.GetByID(2);
            Assert.NotNull(result);
            Assert.Equal("New Comment", result.comment);
        }

        [Fact]
        public void TestDelete()
        {
            var fakeRepository = new CommentRepoFake();
            var unitOfWork = new TestUnitOfWork(fakeRepository);
            var commentService = new CommentService(unitOfWork);
            var commentId = 1;
            var fakeComment = new Comment { commentId = commentId, comment = "Comment 1", ratingId = 1 };
            fakeRepository.Insert(fakeComment);
            commentService.Delete(commentId);
            var result = fakeRepository.GetByID(commentId);
            Assert.Null(result);
        }
    }
}
