using System;

namespace Video
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }

        public Video(string name, string genre)
        {
            this.Name = name;
            this.Genre = genre;
        }

        public override string ToString()
        {
            return $"{Name}, {Genre}";
        }
    }
}
