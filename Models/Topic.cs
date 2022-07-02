using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeveloperRoadmapApi.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int LanguageId { get; set; }
        [JsonIgnore]
        public Language Language { get; set; } = default!;
        public List<SubTopic> SubTopics { get; set; } = default!;
    }
}