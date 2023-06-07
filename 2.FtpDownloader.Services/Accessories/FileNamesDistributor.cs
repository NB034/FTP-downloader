
namespace FtpDownloader.Services.Accessories
{
    internal class FileNamesDistributor
    {
        public string StringBeforeCounter { get; set; } = " (";
        public string StringAfterCounter { get; set; } = ")";
        public int StartCountWith { get; set; } = 1;

        public string GetValidName(string name, string directory)
        {
            int counter = StartCountWith;
            var files = new List<string>(Directory.GetFiles(directory)).Select(f => Path.GetFileName(f));

            while (true)
            {
                if (!files.Contains(name)) break;
                name = Path.GetFileNameWithoutExtension(name) 
                    + StringBeforeCounter + counter + StringAfterCounter
                    + Path.GetExtension(name);
                counter++;
            }

            return name;
        }
    }
}
