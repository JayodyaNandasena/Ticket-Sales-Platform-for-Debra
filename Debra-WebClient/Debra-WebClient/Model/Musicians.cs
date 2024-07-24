
namespace Debra_WebClient.Model
{
    public class Musicians
    {
        public Musicians(string name, string image)
        {
            Name = name;
            Image = image;
        }

        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
    }
}
