namespace DeveloperRoadmapApi.DTOs.SubTopicDto
{
    public class UpdateSubTopicDto
    {
        public string Name { get; set; } = default!;

        public int TopicId { get; set; }
    }
}