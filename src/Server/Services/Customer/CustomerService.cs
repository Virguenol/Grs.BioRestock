using Grs.BioRestock.Application.Interfaces.Services;
using Grs.BioRestock.Domain.Entities;
using Grs.BioRestock.Infrastructure.Contexts;
using Grs.BioRestock.Shared.Enums.BonDeRetour;
using Grs.BioRestock.Shared.Wrapper;
using Grs.BioRestock.Transfer.DataModels.BonDeRetourDtos;
using Grs.BioRestock.Transfer.DataModels.Client;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grs.BioRestock.Server.Services.Cliient
{
    public interface ICustomerService
    {
        Task<Result<List<CustomerDto>>> GetCustomer();
        Task<Result<string>> AddCustomer(CustomerDto customer);
        Task<Result<string>> DeleteCustomer(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly IStringLocalizer<CustomerService> _localizer;
        private readonly ICurrentUserService _currentUserService;
        private readonly UniContext _context;

        public CustomerService(IStringLocalizer<CustomerService> localizer,
            ICurrentUserService currentUserService,
            UniContext context)
        {
            _localizer = localizer;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<Result<string>> AddCustomer(CustomerDto request)
        {
            if (request.Id == 0)
            {
                var customer = request.Adapt<Customer>();
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le bordereau de retour créé");
            }
            else
            {
                var existingCustomers =
                    await _context.Customers.SingleOrDefaultAsync(x => x.Id == request.Id);
                if (existingCustomers == null)
                {
                    return await Result<string>.SuccessAsync("le bordereau de retour n'existe pas");
                }
                else
                {
                    existingCustomers.FirstName = request.FirstName;
                    existingCustomers.LastName = request.LastName;
                    existingCustomers.PhoneNumber = request.PhoneNumber;
                    existingCustomers.Adresse = request.Adresse;

                    _context.Customers.Update(existingCustomers);
                    await _context.SaveChangesAsync();
                    return await Result<string>.SuccessAsync("le bon de retour est mis à jour");
                }
            }
        }
        public async Task<Result<string>> DeleteCustomer(int id)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer != null)
            {
                _context.Customers.Remove(existingCustomer);
                await _context.SaveChangesAsync();
                return await Result<string>.SuccessAsync("le bordereau de retour supprimé");
            }
            else
            {
                return await Result<string>.FailAsync("le le clients n'existe pas");
            }
        }

        public async Task<Result<List<CustomerDto>>> GetCustomer()
        {
            var customer = await _context.Customers.OrderByDescending(x => x.Id).ToListAsync();
            var customerResponse = customer.Adapt<List<CustomerDto>>();
            return await Result<List<CustomerDto>>.SuccessAsync(customerResponse);
        }

    }
}
