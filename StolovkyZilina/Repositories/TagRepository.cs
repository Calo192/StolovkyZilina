using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;

namespace StolovkyZilina.Repositories
{
    public class TagRepository : ITagRepository
    {
        public TagRepository(StolovkyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public StolovkyDbContext ctx { get; }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await ctx.Tags.AddAsync(tag);
            await ctx.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await ctx.Tags.FindAsync(id);
            if (tag != null)
            {
                ctx.Tags.Remove(tag);
                await ctx.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await ctx.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await ctx.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await ctx.Tags.FindAsync(tag.Id);
            if (existingTag != null) 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await ctx.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }
    }
}
