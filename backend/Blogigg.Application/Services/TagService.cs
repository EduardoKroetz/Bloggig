using Bloggig.Application.Services.Interfaces;
using Bloggig.Domain.Entities;
using Bloggig.Domain.Repositories;

namespace Bloggig.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<Tag>> CreateTagsIfNotExistsAsync(string[] names)
    {
        var tagsWithOutHashtag = names.Select(x => x.Replace("#", string.Empty)).ToList();
        var tags = new List<Tag>();
        foreach (var name in tagsWithOutHashtag)
        {
            var tag = await _tagRepository.GetTagByName(name);
            if (tag != null)
            {
                tags.Add(tag);
                continue;
            }

            var newTag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = name,
            };

            await _tagRepository.AddAsync(newTag);

            tags.Add(newTag);
        }
        return tags;
    }

}
