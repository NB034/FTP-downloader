
namespace FtpDownloader.Services.Accessories
{
    public class FileNamesDistributor
    {
        public string StringBeforeCounter { get; set; } = " (";
        public string StringAfterCounter { get; set; } = ")";
        public int StartCountWith { get; set; } = 1;

        public string GetValidName(string name, string directory)
        {
            int counter = StartCountWith;
            var files = new List<string>(Directory.GetFiles(directory)).Select(f => Path.GetFileName(f));
            var validName = name;

            while (true)
            {
                if (!files.Contains(validName)) break;
                validName = Path.GetFileNameWithoutExtension(name) 
                    + StringBeforeCounter + counter + StringAfterCounter
                    + Path.GetExtension(name);
                counter++;
            }

            return validName;
        }
    }
}
