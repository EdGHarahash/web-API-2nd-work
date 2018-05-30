using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateForum.Context;
using PrivateForum.Entities;
using PrivateForum.Entities.DTO;

namespace PrivateForum.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Topics")]
    public class TopicsController : Controller
    {
        private readonly ApplicationContext _context;
        private UserManager<ApplicationUser> _userManager;

        public TopicsController(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Topics
        [HttpGet]
        public IEnumerable<AllTopicDto> GetTopics()
        {

            return AllTopicDto.MakeList(_context.Topics.Include(topic=> topic.User).Include(topic => topic.Tag).ToList());
        }

        // GET: api/Topics/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var topic = await _context.Topics.Include(t=>t.User).Include(t => t.Tag).SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            TopicDetailDto topicDto = new TopicDetailDto(topic);
            return Ok(topicDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostTopic([FromBody] CreateTopicDto createTopic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Topic topic = createTopic.GetTopic();
            topic.User = _userManager.GetUserAsync(HttpContext.User).Result;
            Tag tag = _context.Tags.SingleOrDefault(t => t.Name == topic.Tag.Name.ToUpper());
            if (tag != null)
            {
                topic.Tag = tag;
            }
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopic", new { id = topic.Id }, new AllTopicDto(topic));
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}