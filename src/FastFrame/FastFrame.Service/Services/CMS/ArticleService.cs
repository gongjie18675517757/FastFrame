using System.Threading.Tasks;
using FastFrame.Dto.CMS;
using FastFrame.Entity.CMS;
using FastFrame.Infrastructure.Interface;
using FastFrame.Repository.Basis;
using FastFrame.Repository.CMS;

namespace FastFrame.Service.Services.CMS
{
    public partial class ArticleService
    {
        private readonly ArticleContentRepository articleContentRepository;

        public ArticleService(ArticleCategoryRepository articleCategoryRepository, MeidiaRepository meidiaRepository, 
            ForeignRepository foreignRepository, UserRepository userRepository, ArticleRepository articleRepository, 
            ArticleContentRepository articleContentRepository,
            IScopeServiceLoader loader)
        : this(articleCategoryRepository, meidiaRepository, foreignRepository, userRepository, articleRepository,loader)
        {
            this.articleContentRepository = articleContentRepository;
        }
        public async Task ToggleRelease(string id)
        {
            var entity= await articleRepository.GetAsync(id);
            entity.IsRelease = !entity.IsRelease;
            await articleRepository.UpdateAsync(entity);
        }

        protected override async Task OnAdding(ArticleDto input, Article entity)
        {
            await base.OnAdding(input, entity);
            var contentEntity = await articleContentRepository.AddAsync(new ArticleContent()
            {
                Content = input.Content
            });
            entity.ArticleContent_Id = contentEntity.Id;
        }

        protected override async Task OnDeleteing(Article input)
        {
            await base.OnDeleteing(input);
            await articleContentRepository.DeleteAsync(input.ArticleContent_Id);
        }

        protected override async Task OnUpdateing(ArticleDto input, Article entity)
        {
            await base.OnUpdateing(input, entity);
            var contentEntity = await articleContentRepository.GetAsync(entity.ArticleContent_Id);
            contentEntity.Content = input.Content;
            await articleContentRepository.UpdateAsync(contentEntity); 
        }

        protected override async Task OnGeting(ArticleDto dto)
        {
            await base.OnGeting(dto);
            var contentEntity = await articleContentRepository.GetAsync(dto.ArticleContent_Id);
            dto.Content = contentEntity.Content;
        }
    }
}
