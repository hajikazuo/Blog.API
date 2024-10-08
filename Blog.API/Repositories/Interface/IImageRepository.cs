﻿using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<IEnumerable<BlogImage>> GetAll();
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
    }
}
