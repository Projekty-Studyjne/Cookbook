﻿using CookbookBLL.Interfaces;
using CookbookLibrary.Entities;
using CookbookLibrary.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookbookBLL
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            var comment = await _unitOfWork.CommentRepository.GetAsync(includeProperties: "Rating");
            return comment;
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            var comment = _unitOfWork.CommentRepository.GetByID(commentId);
            return await Task.FromResult(comment);
        }

        public async Task<Comment> GetCommentByRating(int ratingId)
        {
            try
            {
                var comment = _unitOfWork.CommentRepository
                    .GetAsync(r => r.Rating.ratingId == ratingId).Result.FirstOrDefault(x=>x.ratingId==ratingId);

                return comment;
            }
            catch (Exception e)
            {
                Console.WriteLine("An errror occured while getting recipes");
                throw;
            }
        }

        public async Task Update(Comment comment)
        {
            try
            {
                _unitOfWork.CommentRepository.Update(comment);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating comment");
                throw;
            }
        }

        public async Task Add(Comment comment)
        {
            try
            {
                var commentRepos = _unitOfWork.CommentRepository;
                commentRepos.Insert(comment);
                _unitOfWork.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occured while adding comment");
                throw;
            }
        }

        public async Task Delete(int commentId)
        {
            try
            {
                var commentRepos = _unitOfWork.CommentRepository;
                commentRepos.Delete(commentId);
                _unitOfWork.Save();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while deleting comment");
                throw;
            }
        }
    }
}
