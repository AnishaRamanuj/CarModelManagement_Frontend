using CarModelManagement.BLL.Models;
using FluentValidation;

namespace CarModelManagement.FluentValidation
{
    public class CarValidator : AbstractValidator<Cars>
    {
        public CarValidator()
        {
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Select a Brand");
            RuleFor(x => x.Class).NotEmpty().WithMessage("Select a Class");
            RuleFor(x => x.ModelName).NotEmpty().WithMessage("Model Name is required");
            RuleFor(x => x.ModelCode).NotEmpty().WithMessage("Model Code is required")
                                     .Matches("^[a-zA-Z0-9]+$").WithMessage("Model Code should be alphanumeric")
                                     .Length(10).WithMessage("Model Code must be 10 characters long");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Features).NotEmpty().WithMessage("Features is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
            RuleFor(x => x.ManufacturedOn).NotEmpty().WithMessage("Select manufactured date");
        }
    }
}
