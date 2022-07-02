using DeveloperRoadmapApi.DTOs.SubTopicDto;
using DeveloperRoadmapApi.DTOs.TopicDto;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperRoadmapApi.Controllers
{
    // localhost:port/api/subtopic
    [Route("api/[controller]")]
    [ApiController]
    public class SubTopicController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubTopicController(ApplicationDbContext context)
        {
            _context = context;
        }

        // localhost:port/api/subtopic/addsubtopic
        [HttpPost("addsubtopic")]
        public IActionResult Create([FromBody]CreateSubTopicDto model)
        {
            if (String.IsNullOrEmpty(model.Name) && model.TopicId <= 0) return BadRequest("Name and Topic is required");

            var topic = _context.Topics.Find(model.TopicId);

            if (topic == null) return BadRequest("Topics does not exist in the database");

            SubTopic subtopic = new SubTopic();

            subtopic.Name = model.Name;
            subtopic.TopicId = model.TopicId;

            // language.Name = model.Name;
            
            _context.SubTopics.Add(subtopic);
            _context.SaveChanges();

            return new CreatedResult("/getSubTopicById", new { subtopic = subtopic, message = "Sub Topic created successfully" });
        }

        [HttpGet("getSubTopics")]
        public IActionResult GetAll()
        {
            List<SubTopic> allSubTopics = _context.SubTopics.ToList();

            return Ok(allSubTopics);
        }

        //CRUD - UPDATE A SUBTOPIC
        // localhost:port/api/subtopic/subUpdateTopic/1
        [HttpPut("updateSubTopic")]
        public IActionResult Update([FromQuery]int Id, [FromBody]UpdateSubTopicDto model)
        {
            if (String.IsNullOrEmpty(model.Name) || Id <= 0) return BadRequest("Name and  Id is required");

            SubTopic? subTopic = _context.SubTopics.FirstOrDefault(subtopic => subtopic.Id == Id);

            if (subTopic != null)
            {
                subTopic.Name = model.Name;

                if (model.TopicId > 0)
                {
                    subTopic.TopicId = model.TopicId;
                }

                _context.SubTopics.Update(subTopic);
                _context.SaveChanges();

                return Ok(subTopic);
            }
            
            return BadRequest($"Sub Topic with id: {Id} does not exist in the database");
        }


        //CRUD - DELETE A SUB TOPIC
        // localhost:port/api/subTopic/deleteSubTopic/1
        [HttpDelete("deleteSubTopic")]
        public IActionResult Delete([FromQuery]int Id)
        {
            if (Id <= 0) return BadRequest("Id is required");

            SubTopic? subTopic = _context.SubTopics.FirstOrDefault(subTopic => subTopic.Id == Id);

            if (subTopic != null)
            {
                _context.SubTopics.Remove(subTopic);
                _context.SaveChanges();

                return Ok();
            }
            
            return BadRequest($"Sub Topic with id: {Id} does not exist in the database");
        }
    }
}