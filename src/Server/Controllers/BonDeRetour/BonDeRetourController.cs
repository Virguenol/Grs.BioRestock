using Grs.BioRestock.Server.Services.BonDeRetourService;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Controllers.BonDeRetour
{

    public class BonDeRetourController : BaseApiController<BonDeRetourController>
    {
        private readonly IBonDeRetour _bonDeRetour;

        public BonDeRetourController(IBonDeRetour bonDeRetour)
        {
            _bonDeRetour = bonDeRetour;
        }
        [HttpGet(nameof(GetAll))]
        public async Task<Result<List<BonDeRetourDto>>> GetAll()
        {
           return await _bonDeRetour.GetBonDeRetours();
        }
        [HttpPost(nameof(Add))]
        public async Task<Result<string>> Add(BonDeRetourDto request)
        {
            return await _bonDeRetour.AddBonDeRetour(request);
        }
        [HttpGet(nameof(GetById)+"/{id}")]
        public async Task<Result<BonDeRetourDto>> GetById(int id)
        {
            return await _bonDeRetour.GetByIdBonDeRetour(id);
        }
        [HttpDelete(nameof(Delete) + "/{id}")]
        public async Task<Result<string>> Delete(int id)
        {
           return await _bonDeRetour.DeleteBonDeRetour(id);
           
        }
        [HttpPost(nameof(Validation))]
        public async Task<Result<string>> Validation(int id)
        {
            return await _bonDeRetour.Validation(id);
        }

        [HttpPost(nameof(ChoixDepots))]
        public async Task<Result<string>> ChoixDepots(BonDeRetourDto request)
        {
            return await _bonDeRetour.ChoixDepot(request);
        }
    }
}
