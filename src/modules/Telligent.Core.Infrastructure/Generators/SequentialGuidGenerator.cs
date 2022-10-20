using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Telligent.Core.Infrastructure.Generators
{
    public sealed class SequentialGuidGenerator
    {
        private static readonly Lazy<SequentialGuidGenerator> Lazy = new(() => new SequentialGuidGenerator());
        private static SequentialGuidValueGenerator _sgvg;

        private SequentialGuidGenerator()
        {
            _sgvg = new SequentialGuidValueGenerator();
        }

        public static SequentialGuidGenerator Instance => Lazy.Value;

        public Guid GetGuid()
        {
            return _sgvg.Next(null!);
        }
    }
}
