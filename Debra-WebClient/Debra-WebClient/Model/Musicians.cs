
namespace Debra_WebClient.Model
{
    public class Musicians
    {
        public Musicians(string musicianName, Task<string> task)
        {
            MusicianName = musicianName;
            Task = task;
        }

        public string Name { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
        public string MusicianName { get; }
        public Task<string> Task { get; }
    }
}
