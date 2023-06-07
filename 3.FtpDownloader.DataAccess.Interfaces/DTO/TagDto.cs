
namespace FtpDownloader.DataAccess.Interfaces.DTO
{
    public class TagDto
    {
        public int Id { get; } = 0;
        public string Name { get; set; } = string.Empty;


        public TagDto(int id)
        {
            Id = id;
        }
    }
}
