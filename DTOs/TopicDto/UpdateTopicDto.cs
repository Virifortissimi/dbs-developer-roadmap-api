namespace DeveloperRoadmapApi.DTOs.TopicDto
{
    public class UpdateTopicDto
    {
        public string Name { get; set; } = default!;

        public int LanguageId { get; set; }
    }
}