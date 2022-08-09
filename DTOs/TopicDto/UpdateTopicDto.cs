namespace DeveloperRoadmapApi.DTOs.TopicDto
{
    public class UpdateTopicDto
    {
        public string Name { get; set; } = default!;

        public int LanguageId { get; set; }

        public int TopicId { get; set; }
    }
}