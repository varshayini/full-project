using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniTutor.Interface;
using static Org.BouncyCastle.Math.EC.ECCurve;
using UniTutor.Model;
using UniTutor.DTO;
using Microsoft.EntityFrameworkCore;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubject _subject;
        private readonly ITutor _tutor;
        private IConfiguration _config;
        public SubjectController(ISubject subject, IConfiguration config, ITutor tutor)
        {
            _subject = subject;
            _config = config;
            _tutor = tutor;
        }
        //[HttpGet("getallsubject")]
        //public IActionResult Get()
        //{
        //    return ;
        //}
        //create subject by the tutor id
        // POST method to create a new subject
        [HttpPost("{tutorId}/subjects")]
        public async Task<IActionResult> CreateSubject(int tutorId, [FromBody] SubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subject.CreateSubject(tutorId, request);
            if (!result)
            {
                return NotFound(new { message = "Tutor not found" });
            }

            return CreatedAtAction(nameof(GetSubject), new { tutorId, id = request.title }, request);
        }

        // Example method to get a subject by id (not fully implemented)
        [HttpGet("{tutorId}/subjects/{id}")]
        public async Task<IActionResult> GetSubject(int tutorId, int id)
        {
            var subject =  _subject.GetSubject(tutorId, id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        //get the subjects by tutorid
        [HttpGet("{tutorId}/subjects")]
        public async Task<IActionResult> GetSubjects(int tutorId)
        {
            var subjects =  _subject.GetSubjects(tutorId);
            if (subjects == null)
            {
                return NotFound();
            }
            return Ok(subjects);
        }





    }
    
}
