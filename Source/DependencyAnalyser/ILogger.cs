using System.Text;

namespace DependencyAnalyser
{
    public interface ILogger
    {
        void WriteLine(string message);
    }

    public sealed class StringBuilderLogger : ILogger
    {
        private readonly StringBuilder _sb = new();
        public void WriteLine(string message) => _sb.AppendLine(message);
        public override string ToString() => _sb.ToString();
    }
}