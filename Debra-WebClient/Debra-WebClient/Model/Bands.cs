
namespace Debra_WebClient.Model
{
    public class Bands
    {
        public Bands(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
