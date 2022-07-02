namespace DeveloperRoadmapApi.DTOs.LanguageDto
{
    public class ReadLanguageTopicDto
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; } = default!;
        public List<ReadTopicDto> Topics { get; set; } = new List<ReadTopicDto>();
    }

    public class ReadTopicDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; } = default!;
    }
}