
namespace FtpDownloader.DataAccess.Interfaces.DTO
{
    public class TagEntityDto
    {
        public int Id { get; } = 0;
        public string Name { get; set; } = string.Empty;


        public TagEntityDto(int id)
        {
            Id = id;
        }
    }
}
