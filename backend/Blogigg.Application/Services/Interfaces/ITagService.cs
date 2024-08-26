using Bloggig.Domain.Entities;

namespace Bloggig.Application.Services.Interfaces;

public interface ITagService
{
    Task<List<Tag>> CreateTagsIfNotExistsAsync(string[] names); 
}
