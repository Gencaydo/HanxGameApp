using FluentValidation;
using HanxGame.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanxGame.Service.Validations
{
    public class GameDtoValidator:AbstractValidator<GameDto>
    {
        public GameDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.KeyTypeId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.DefaultBuyingPrice).InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");
            RuleFor(x => x.DefaultSellingPrice).InclusiveBetween(1, decimal.MaxValue).WithMessage("{PropertyName} must be greater 0");
        }
    }
}
