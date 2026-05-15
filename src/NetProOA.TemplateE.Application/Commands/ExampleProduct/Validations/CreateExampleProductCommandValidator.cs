using FluentValidation;
using NetProOA.TemplateE.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct.Validations
{
    public class CreateExampleProductCommandValidator : AbstractValidator<CreateExampleProductCommand>
    {
        public CreateExampleProductCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
        }
    }
}


