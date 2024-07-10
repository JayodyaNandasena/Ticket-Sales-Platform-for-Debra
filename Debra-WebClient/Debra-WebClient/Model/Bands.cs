
namespace Debra_WebClient.Model
{
    public class Bands
    {
        public Bands(string bandName, Task<string> task)
        {
            BandName = bandName;
            Task = task;
        }

        public string Name { get; set; } = null!;
        public string ImageBase64 { get; set; } = null!;
        public string BandName { get; }
        public Task<string> Task { get; }
    }
}
