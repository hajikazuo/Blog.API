﻿using Blog.API.Models.Domain;

namespace Blog.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetById(Guid id);
        Task<Category?> CreateAsync(Category category);      
        Task<Category?> UpdateAsync(Category category);
        Task<Category?> DeleteAsync(Guid id);
    }
}
