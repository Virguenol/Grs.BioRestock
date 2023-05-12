﻿using FluentValidation;
using Microsoft.Extensions.Localization;
using Grs.BioRestock.Transfer.Requests.Identity;

namespace Grs.BioRestock.Transfer.Validators.Requests.Identity
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator(IStringLocalizer<TokenRequestValidator> localizer)
        {
            RuleFor(request => request.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Email requis"])
                .EmailAddress().WithMessage(x => localizer["Email incorrect"]);
            RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Mot de passe requis!"]);
        }
    }
}
