using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProOA.TemplateE.Application.Grains.Impl
{
    public class TestGrain : Grain, ITestGrain
    {
        public async Task<string> TestMethod(string name)
        {
            return await Task.FromResult($"test, {name}!");
        }
    }
}
