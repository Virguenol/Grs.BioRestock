using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Domain.Entities;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.Article;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourDtos;
using Grs.BioRestock.Transfer.DataModels.Client;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grs.BioRestock.Server.Services.Article
{
    public interface IArticleService
    {
        Task<Result<List<ArticleDto>>> GetArticle();
        Task<Result<string>> AddArcticle(ArticleDto customer);
        Task<Result<string>> DeleteArticle(int id);
    }
    public class ArticleService : IArticleService
    {
        private readonly IStringLocalizer<ArticleService> _localizer;
        private readonly ICurrentUserService _currentUserService;
        private readonly UniContext _context;

        public ArticleService(IStringLocalizer<ArticleService> localizer,
            ICurrentUserService currentUserService,
            UniContext context)
        {
            _localizer = localizer;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Result<string>> AddArcticle(ArticleDto request)
        {
            if (request.Id == 0)
            {
                var article = request.Adapt<Domain.Entities.Article>();
                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le bordereau de retour créé");
            }
            else
            {
                var existingArticle =
                    await _context.Articles.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingArticle == null)
                {
                    return await Result<string>.SuccessAsync("le bordereau de retour n'existe pas");
                }
                else
                {
                    existingArticle.Name = request.Name;
                    existingArticle.Designation = request.Designation;
                    existingArticle.Price = request.Price;

                    _context.Articles.Update(existingArticle);
                    await _context.SaveChangesAsync();
                    return await Result<string>.SuccessAsync("l'article est mis à jour");
                }
            }
        }
        public async Task<Result<string>> DeleteArticle(int id)
        {
            var existingArticle = await _context.Articles.FirstOrDefaultAsync(x => x.Id == id);
            if (existingArticle != null)
            {
                _context.Articles.Remove(existingArticle);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("l'article supprimé");
            }
            else
            {
                return await Result<string>.FailAsync("l'article n'existe pas");
            }
        }

        public async Task<Result<List<ArticleDto>>> GetArticle()
        {
            var article = await _context.Customers.OrderByDescending(x => x.Id).ToListAsync();
            var articleResponse = article.Adapt<List<ArticleDto>>();
            return await Result<List<ArticleDto>>.SuccessAsync(articleResponse);
        }
    }
}
