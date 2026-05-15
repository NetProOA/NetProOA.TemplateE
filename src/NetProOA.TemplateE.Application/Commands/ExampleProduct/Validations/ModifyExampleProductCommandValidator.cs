using FluentValidation;
using NetProOA.TemplateE.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Application.Commands.ExampleProduct.Validations
{
    public class ModifyExampleProductCommandValidator : AbstractValidator<ModifyExampleProductCommand>
    {
        public ModifyExampleProductCommandValidator()
        {
        }
    }
}



