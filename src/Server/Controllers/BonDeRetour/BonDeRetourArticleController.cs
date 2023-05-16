using Grs.BioRestock.Domain.Entities;
using Grs.BioRestock.Server.Services.BonDeRetourArtile;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourArticle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Controllers.BonDeRetour
{
    public class BonDeRetourArticleController : ControllerBase
    {
        private readonly IBonDeRetourArticleService _bonDeRetourArticleService;

        public BonDeRetourArticleController(IBonDeRetourArticleService bonDeRetourArticleService)
        {
            _bonDeRetourArticleService = bonDeRetourArticleService;
        }

        [HttpPost(nameof(Add))]
        public async Task<Result<string>> Add(BonDeRetourArticleDto request)
        {
            return await _bonDeRetourArticleService.AddBonDeRetourArticle(request);
        }

        [HttpGet(nameof(GetAll))]
        public async Task<Result<List<BonDeRetourArticleDto>>> GetAll()
        {
            return await _bonDeRetourArticleService.GetBonDeRetourArticles();
        }

        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<Result<string>> Delete(int id)
        {
            return await _bonDeRetourArticleService.DeleteBonDeRetourArticle(id);
        }
    }
}
