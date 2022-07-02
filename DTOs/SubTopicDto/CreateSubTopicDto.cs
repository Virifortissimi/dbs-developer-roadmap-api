namespace DeveloperRoadmapApi.DTOs.SubTopicDto
{
    public class CreateSubTopicDto
    {
        public string Name { get; set; } = default!;
        public int TopicId { get; set; }
    }
}