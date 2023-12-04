using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Util;

namespace StolovkyZilina.Controllers
{
    [Authorize(Roles = "Admin")]
	public class AdminBlogPostsController : Controller
    {

        private readonly ITagRepository tagRepository;
        private readonly IRepository<BlogPost> blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IRepository<BlogPost> ibloGpostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = ibloGpostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            var model = new BlogPostRequest
			{
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(BlogPostRequest addBlogPostRequest, IFormFile fileInput)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDesc = addBlogPostRequest.ShortDesc,
                UrlHandle = addBlogPostRequest.PageTitle.Replace(' ','-'),
                Author = addBlogPostRequest.Author,
                PublishDate = DateTime.Now,
                Visible = addBlogPostRequest.Visible,
            };

            var selectedTags = new List<Tag>();
            // map tags from selected tags
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var existingTag = await tagRepository.GetAsync(Guid.Parse(selectedTagId));

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tags = selectedTags;

            if (fileInput != null && fileInput.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileInput.CopyTo(memoryStream);

                    blogPost.FeaturedImage = ImageUtil.CropTo1x4AspectRatio(memoryStream.ToArray());
                }
            }

            await blogPostRepository.AddAsync(blogPost);

            return RedirectToAction("List", "BlogPosts");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Call the repository
            var blogPosts = await blogPostRepository.GetAllAsync();

            return View(blogPosts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await blogPostRepository.GetAsync(id);
            var tagDomainModel = await tagRepository.GetAllAsync();

            if (blogPost != null)
            {
                var model = new BlogPostRequest
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    FeaturedImage = blogPost.FeaturedImage,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDesc = blogPost.ShortDesc,
                    PublishDate = blogPost.PublishDate,
                    Visible = blogPost.Visible,
                    Tags = tagDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }
            
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogPostRequest editBlogPostrequest, IFormFile fileInput)
        {
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogPostrequest.Id,
                Heading = editBlogPostrequest.Heading,
                PageTitle = editBlogPostrequest.PageTitle,
                Content = editBlogPostrequest.Content,
                Author = editBlogPostrequest.Author,
                ShortDesc = editBlogPostrequest.ShortDesc,
                FeaturedImage = editBlogPostrequest.FeaturedImage,
                UrlHandle = editBlogPostrequest.UrlHandle,
                PublishDate = editBlogPostrequest.PublishDate,
                Visible = editBlogPostrequest.Visible,
            };

            var selectedTags = new List<Tag>();
            foreach (var tag in editBlogPostrequest.SelectedTags)
            {
                if (Guid.TryParse(tag, out var t))
                {
                    var foundaTag = await tagRepository.GetAsync(t);

                    if (foundaTag != null)
                    {
                        selectedTags.Add(foundaTag);
                    }
                }
            }

            blogPostDomainModel.Tags = selectedTags;
            if (fileInput != null && fileInput.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileInput.CopyTo(memoryStream);

                    blogPostDomainModel.FeaturedImage = ImageUtil.CropTo1x4AspectRatio(memoryStream.ToArray());
                }
            }

            var updatedBlog = await blogPostRepository.UpdateAsync(blogPostDomainModel);
            if (updatedBlog != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BlogPostRequest editBlogPostrequest)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostrequest.Id);
            if (deletedBlogPost != null) 
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editBlogPostrequest.Id});
        }
    }
}
