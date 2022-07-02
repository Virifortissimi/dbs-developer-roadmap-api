using DeveloperRoadmapApi.DTOs.LanguageDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DeveloperRoadmapApi.Controllers
{
    // localhost:port/api/language
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LanguageController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // localhost:port/api/language/addLanguage
        [HttpPost("addLanguage")]
        public IActionResult Create([FromBody]CreateLanguageDto model)
        {
            if (String.IsNullOrEmpty(model.Name)) return BadRequest("Name is required");

            Language language = new Language();

            language.Name = model.Name;
            
            _context.Languages.Add(language);
            _context.SaveChanges();

            return new CreatedResult("/getLanguageById", new { language = language, message = "Language created successfully" });
        }

        // localhost:port/api/language/getLanguage
        [HttpGet("getLanguages")]
        public IActionResult GetAll()
        {
            List<Language> languages = _context.Languages.ToList();
            return Ok(languages);
        }

        [HttpGet("getLanguagesWithTopics")]
        public IActionResult GetAllWithTopics()
        {
            List<ReadLanguageTopicDto> getAllLanguagesWithTopics = _context.Languages.Select(x => new ReadLanguageTopicDto()
            {
                LanguageId = x.Id,
                LanguageName = x.Name,
                Topics = x.Topics.Select(n => new ReadTopicDto()
                {
                    TopicId = n.Id,
                    TopicName = n.Name
                }).ToList()
            }).ToList();

            if (getAllLanguagesWithTopics == null) return NotFound();

            return Ok(getAllLanguagesWithTopics);
        }

        [HttpGet("getLanguageWithTopics")]
        public IActionResult GetWithTopics([FromQuery] int Id)
        {
            ReadLanguageTopicDto? getWithTopics = _context.Languages.Where(x => x.Id == Id).Select(x => new ReadLanguageTopicDto()
            {
                LanguageId = x.Id,
                LanguageName = x.Name,
                Topics = x.Topics.Select(n => new ReadTopicDto()
                {
                    TopicId = n.Id,
                    TopicName = n.Name
                }).ToList()
            }).FirstOrDefault();

            if (getWithTopics == null) return NotFound();

            return Ok(getWithTopics);
        }

        //CRUD - READ BUT WE ARE READING A SINGLE LANGUAGE FROM THE DATABASE
        // localhost:port/api/language/getLanguageById
        [HttpGet("getLanguageById")]
        public IActionResult GetById([FromQuery]int Id)
        {
            ReadLanguageDto? readLanguageDto = _context.Languages.Where(lang => lang.Id == Id).Select(n => new ReadLanguageDto()
            {
                Id = n.Id,
                Name = n.Name
            }).FirstOrDefault();

            if (readLanguageDto == null) return BadRequest($"Language with id: {Id} does not exist in the database");

            return Ok(readLanguageDto);
        }

        //CRUD - UPDATE A LANGUAGE
        // localhost:port/api/language/updateLanguage/1
        [HttpPut("updateLanguage")]
        public IActionResult Update([FromQuery]int Id, [FromBody]UpdateLanguageDto model)
        {
            if (String.IsNullOrEmpty(model.Name) || Id <= 0) return BadRequest("Name and  Id is required");

            Language? language = _context.Languages.FirstOrDefault(lang => lang.Id == Id);

            if (language != null)
            {
                language.Name = model.Name;

                _context.Languages.Update(language);
                _context.SaveChanges();

                return Ok(language);
            }
            
            return BadRequest($"Language with id: {Id} does not exist in the database");
        }


        //CRUD - DELETE A LANGUAGE
        // localhost:port/api/language/deleteLanguage/1
        [HttpDelete("deleteLanguage")]
        public IActionResult Delete([FromQuery]int Id)
        {
            if (Id <= 0) return BadRequest("Id is required");

            Language? language = _context.Languages.FirstOrDefault(lang => lang.Id == Id);

            if (language != null)
            {
                _context.Languages.Remove(language);
                _context.SaveChanges();

                return Ok();
            }
            
            return BadRequest($"Language with id: {Id} does not exist in the database");
        }
    }
}