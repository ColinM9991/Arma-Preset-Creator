using System.IO;

namespace ArmaPresetCreator.Web.IntegrationTests
{
    public interface IFileReader
    {
        string Read(string fileName);
    }

    public class FileReader : IFileReader
    {
        public string Read(string fileName)
            => File.ReadAllText(Path.Combine("TestFiles", $"{fileName}.json"));
    }
}
