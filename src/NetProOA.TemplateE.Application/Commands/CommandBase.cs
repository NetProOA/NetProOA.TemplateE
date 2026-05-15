using NetProOA.Framework.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetProOA.TemplateE.Application.Commands
{
    public class CommandBase : ICommand
    {
        public UserIdentity UserIdentity { get; set; }
    }
}
