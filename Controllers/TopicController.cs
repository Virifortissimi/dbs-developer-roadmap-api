using DeveloperRoadmapApi.DTOs.TopicDto;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperRoadmapApi.Controllers
{
    // localhost:port/api/topic
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TopicController(ApplicationDbContext context)
        {
            _context = context;
        }

        // localhost:port/api/topic/addtopic
        [HttpPost("addtopic")]
        public IActionResult Create([FromBody]CreateTopicDto model)
        {
            if (String.IsNullOrEmpty(model.Name) && model.LanguageId <= 0) return BadRequest("Name is required");

            var language = _context.Languages.Find(model.LanguageId);

            if (language == null) return BadRequest("Language does not exist in the database");

            Topic topic = new Topic();

            topic.Name = model.Name;
            topic.LanguageId = model.LanguageId;

            // language.Name = model.Name;
            
            _context.Topics.Add(topic);
            _context.SaveChanges();

            return new CreatedResult("/getTopicById", new { topic = topic, message = "Topic created successfully" });
        }

        [HttpGet("getTopics")]
        public IActionResult GetAll()
        {
            List<Topic> allTopics = _context.Topics.ToList();

            return Ok(allTopics);
        }

         [HttpGet("getTopicById")]
        public IActionResult GetById([FromQuery]int Id)
        {
            UpdateTopicDto? updateTopicDto = _context.Topics.Where(topic => topic.Id == Id).Select(n => new UpdateTopicDto()
            {
                TopicId = n.Id,
                Name = n.Name
            }).FirstOrDefault();

            if (updateTopicDto == null) return BadRequest($"Topic with id: {Id} does not exist in the database");

            return Ok(updateTopicDto);
        }


        //CRUD - UPDATE A TOPIC
        // localhost:port/api/topic/updateTopic/1
        [HttpPut("updateTopic")]
        public IActionResult Update([FromQuery]int Id, [FromBody]UpdateTopicDto model)
        {
            if (String.IsNullOrEmpty(model.Name) || Id <= 0) return BadRequest("Name and  Id is required");

            Topic? topic = _context.Topics.FirstOrDefault(topic => topic.Id == Id);

            if (topic != null)
            {
                topic.Name = model.Name;

                if (model.LanguageId > 0)
                {
                    topic.LanguageId = model.LanguageId;
                }

                _context.Topics.Update(topic);
                _context.SaveChanges();

                return Ok(topic);
            }
            
            return BadRequest($"Topic with id: {Id} does not exist in the database");
        }


        //CRUD - DELETE A LANGUAGE
        // localhost:port/api/topic/deleteTopic/1
        [HttpDelete("deleteTopic")]
        public IActionResult Delete([FromQuery]int Id)
        {
            if (Id <= 0) return BadRequest("Id is required");

            Topic? topic = _context.Topics.FirstOrDefault(topic => topic.Id == Id);

            if (topic != null)
            {
                _context.Topics.Remove(topic);
                _context.SaveChanges();

                return Ok();
            }
            
            return BadRequest($"Topic with id: {Id} does not exist in the database");
        }
    }
}