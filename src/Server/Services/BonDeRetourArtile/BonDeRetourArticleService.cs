using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Domain.Entities;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourArticle;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourDtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Services.BonDeRetourArtile
{
    public interface IBonDeRetourArticleService
    {
        Task<Result<List<BonDeRetourArticleDto>>> GetBonDeRetourArticles();
        Task<Result<BonDeRetourArticleDto>> GetByIdBonDeRetourArticle(int id);
        Task<Result<string>> AddBonDeRetourArticle(BonDeRetourArticleDto request);
        Task<Result<string>> DeleteBonDeRetourArticle(int id);
    }
    public class BonDeRetourArticleService : IBonDeRetourArticleService
    {
        private string _BonDeRetourId;
        private readonly IStringLocalizer<BonDeRetourArticleService> _localizer;
        private readonly ICurrentUserService _currentUserService;
        private readonly UniContext _context;
        public BonDeRetourArticleService(
            IStringLocalizer<BonDeRetourArticleService> localizer,
            ICurrentUserService currentUserService,
            UniContext context,
            string bonDeRetour)
        {
            _localizer = localizer;
            _currentUserService = currentUserService;
            _context = context;
            _BonDeRetourId = bonDeRetour;
        }
        public async Task<Result<string>> AddBonDeRetourArticle(BonDeRetourArticleDto request)
        {
            var element = await _context.BonDeRetours.OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            BonDeRetourArticleDto bonDeRetourArticle = new BonDeRetourArticleDto
            {
                BonDeretourId = element.Id,
                ArticleId = request.ArticleId,
                Quantity = request.Quantity,
            };
            //var bonDeRetourArticle = request.Adapt<Domain.Entities.BonDeRetourArticle>();
            await _context.AddAsync(bonDeRetourArticle);
              await _context.SaveChangesAsync();
            return await Result<string>.SuccessAsync("Ajouter");
        }

        public async Task<Result<string>> DeleteBonDeRetourArticle(int id)
        {
            var existingBonDeRetour = await _context.BonDeRetourArticles.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBonDeRetour != null)
            {
                _context.BonDeRetourArticles.Remove(existingBonDeRetour);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le bordereau de retour supprimé");
            }
            else
            {
                return await Result<string>.FailAsync("le bordereau de retour n'existe pas");
            }
        }
        public async Task<Result<List<BonDeRetourArticleDto>>> GetBonDeRetourArticles()
        {
            var bonderetour = await _context.BonDeRetourArticles.OrderByDescending(x => x.Id).Include(c => c.Id).Include(c => c.Articles).ToListAsync();
            var bonderetourResponse = bonderetour.Adapt<List<BonDeRetourArticleDto>>();
            return await Result<List<BonDeRetourArticleDto>>.SuccessAsync(bonderetourResponse);
        }

        public Task<Result<BonDeRetourArticleDto>> GetByIdBonDeRetourArticle(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
