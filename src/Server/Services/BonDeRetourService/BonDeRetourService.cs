using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Domain.Entities;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Shared.Enums.BonDeRetour;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourDtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Services.BonDeRetourService
{
    public interface IBonDeRetour
    {
        Task<Result<List<BonDeRetourDto>>> GetBonDeRetours();
        Task<Result<BonDeRetourDto>> GetByIdBonDeRetour(int id);
        Task<Result<string>> AddBonDeRetour(BonDeRetourDto bonderetour);
        Task<Result<string>> DeleteBonDeRetour(int id);
        Task<Result<string>> Validation(int id);
        Task<Result<string>> ChoixDepot(BonDeRetourDto request);
    }
    
       
    public class BonDeRetourService : IBonDeRetour
    {
        private readonly IStringLocalizer<BonDeRetourService> _localizer;
        private readonly ICurrentUserService _currentUserService;
        private readonly UniContext _context;

        public BonDeRetourService(
            IStringLocalizer<BonDeRetourService> localizer,
            ICurrentUserService currentUserService,
            UniContext context)
        {
            _localizer = localizer;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Result<string>> AddBonDeRetour(BonDeRetourDto request)
        {
            if (request.Id == 0)
            {
                var bonderetour = request.Adapt<BonDeRetour>();
                await _context.BonDeRetours.AddAsync(bonderetour);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le bordereau de retour créé");
            }
            else
            {
                var existingBonDeRetour =
                    await _context.BonDeRetours.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingBonDeRetour == null)
                {
                    return await Result<string>.SuccessAsync("le bordereau de retour n'existe pas");
                }
                else
                {
                    existingBonDeRetour.Depot = request.Depot;
                    existingBonDeRetour.Code = request.Code;
                    existingBonDeRetour.ArticleName = request.ArticleName;
                    existingBonDeRetour.ClientName = request.ClientName;
                    existingBonDeRetour.AnomalyType = request.AnomalyType;
                    existingBonDeRetour.Reference = request.Reference;
                    existingBonDeRetour.Quantity = request.Quantity;

                    existingBonDeRetour.Quantity = request.Quantity;
                    _context.BonDeRetours.Update(existingBonDeRetour);
                    await _context.SaveChangesAsync();
                    return await Result<string>.SuccessAsync("le bon de retour est mis à jour"); 
                }
            }
        }
        public async Task<Result<string>> DeleteBonDeRetour(int id)
        {
            var existingBonDeRetour = await _context.BonDeRetours.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBonDeRetour != null)
            {
                _context.BonDeRetours.Remove(existingBonDeRetour);
                 await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le bordereau de retour supprimé");
            }
            else
            {
                return  await Result<string>.FailAsync("le bordereau de retour n'existe pas");
            }
        }

        public async Task<Result<List<BonDeRetourDto>>> GetBonDeRetours()
        {
            var bonderetour = await _context.BonDeRetours.OrderByDescending(x=>x.Id).Include(c => c.Articles).Include(c => c.Customers).ToListAsync();
            var bonderetourResponse = bonderetour.Adapt<List<BonDeRetourDto>>();
            return await Result<List<BonDeRetourDto>>.SuccessAsync(bonderetourResponse);
        }

        public async Task<Result<BonDeRetourDto>> GetByIdBonDeRetour(int id)
        {
            var bonderetour = await _context.BonDeRetours
                .SingleOrDefaultAsync(x => x.Id == id);
            var bonderetourResponse = bonderetour.Adapt<BonDeRetourDto>();
            return await Result<BonDeRetourDto>.SuccessAsync(bonderetourResponse);
        }

        public async Task<Result<string>> Validation(int id)
        {
            var bonderetour = await _context.BonDeRetours.FirstOrDefaultAsync(x => x.Id == id);
        
            bonderetour.Status = BonDeRetourStatus.Validé;

            _context.BonDeRetours.Update(bonderetour);
            await _context.SaveChangesAsync();

            var respose = bonderetour.Adapt<BonDeRetourDto>();
            return await Result<string>.SuccessAsync("valider avec succès");
        }

        public async Task<Result<string>> ChoixDepot(BonDeRetourDto request)
        {
            var existingBonDeRetour = await _context.BonDeRetours.SingleOrDefaultAsync(x => x.Id == request.Id);
            if (existingBonDeRetour == null)
            {
                return await Result<string>.SuccessAsync("le bordereau de retour n'existe pas");
            }
            else
            {
                existingBonDeRetour.Depot = request.Depot;
                _context.BonDeRetours.Update(existingBonDeRetour);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("prêt à être envoyé au dépôt");
            }
        }

    }
}
