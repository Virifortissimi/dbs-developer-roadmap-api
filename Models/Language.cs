using System.ComponentModel.DataAnnotations;

namespace DeveloperRoadmapApi.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public List<Topic> Topics { get; set; } = default!;
    }
}