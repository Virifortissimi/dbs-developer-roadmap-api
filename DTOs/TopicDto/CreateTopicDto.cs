namespace DeveloperRoadmapApi.DTOs.TopicDto
{
    public class CreateTopicDto
    {
        public string Name { get; set; } = default!;
        public int LanguageId { get; set; }
    }
}