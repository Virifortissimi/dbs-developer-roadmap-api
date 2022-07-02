using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeveloperRoadmapApi.Models
{
    public class SubTopic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int TopicId { get; set; }
        [JsonIgnore]
        public Topic Topic { get; set; } = default!;
    }
}