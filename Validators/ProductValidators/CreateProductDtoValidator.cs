using FluentValidation;
using WebApiAdvance.Entities.Dtos.Products;

namespace WebApiAdvance.Validators.ProductValidators;

public class CreateProductDtoValidator:AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(cp => cp.Name).NotNull()
                              .NotEmpty()
                              .MaximumLength(150).WithMessage("Name mast not be greater than 150 characters!");
                              //.Must(StartWithA);
        //.Must((string name) => name.StartsWith('A'));
        RuleFor(cp => cp.Price).GreaterThanOrEqualTo(0)
                             .LessThanOrEqualTo(1000)
                             .NotNull();  
    }
    public bool StartWithA(string name)
    {
        return name.StartsWith('A');
    }
}
