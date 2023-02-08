using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;

namespace API.Controllers
{
    public class FeedbackController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FeedbackController(UserManager<AppUser> userManager, DataContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("eedbacks")]
        public async Task<ActionResult<List<FeedbackDto>>> GetFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Select(f => new FeedbackDto
                {
                    Id = f.Id,
                    Student = f.Student,
                    Teacher = f.Teacher,
                    Subject = f.Subject,
                    Message = f.Message,
                    SentimentScore = f.SentimentScore,
                })
                .ToListAsync();
           
            return feedbacks;
        }
        [HttpGet("anonymous-feedbacks")]
        public async Task<ActionResult<List<AnonymousFeedbackDto>>> GetAnonymousFeedbacks()
        {
            var feedbacks = await _context.Feedbacks
                .Select(f => new AnonymousFeedbackDto
                {
                    Id = f.Id,
                    Subject = f.Subject,
                    Message = f.Message,
                    SentimentScore = f.SentimentScore,
                })
                .ToListAsync();
                

            return feedbacks;

        }
        

            // [Authorize(Roles = "Admin")]
            [HttpGet("{id}", Name = "GetFeedback")]
        public async Task<ActionResult<Feedback>> GetFeedback(int id)
        {
            return await _context.Feedbacks.FindAsync(id);
        }


        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromForm] CreateFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _userManager.FindByIdAsync(feedbackDto.StudentId);
            if (student == null)
            {
                return BadRequest("Student not found");
            }

            var teacher = await _userManager.FindByIdAsync(feedbackDto.TeachertId);
            if (teacher == null)
            {
                return BadRequest("Teacher not found");
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == feedbackDto.SubjectId);
            if (subject == null)
            {
                return BadRequest("Subject not found");
            }

            var existingFeedback = await _context.Feedbacks
                .FirstOrDefaultAsync(x => x.StudentId == feedbackDto.StudentId && x.SubjectId == feedbackDto.SubjectId);
            if (existingFeedback != null)
            {
                return BadRequest("Feedback already exists for this student and subject");
            }


            var feedback = new Feedback
            {
                Student = student,
                Teacher = teacher,
                Subject = subject,
                Message = feedbackDto.Message,
            };
            feedback.SetScore(feedbackDto.Message);

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(feedback);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedback([FromForm] UpdateFeedbackDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == feedbackDto.Id);
            if (feedback == null)
            {
                return BadRequest("Feedback not found");
            }

            feedback.Message= feedbackDto.Message;
            feedback.SetScore(feedbackDto.Message);

            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok(feedback);
            return BadRequest(new ProblemDetails { Title = "Problem updating Feedback" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbacl([FromForm] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedback = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
            if (feedback == null)
            {
                return NotFound();
            }


            _context.Feedbacks.Remove(feedback);
            var result = await _context.SaveChangesAsync() > 0;
            if (result) return Ok();
            return BadRequest(new ProblemDetails { Title = "Problem deleting Feedback" });
        }

        [HttpPost("TestSentiment")]
        public async Task<IActionResult> TestSentiment(string Message)
        {

            var context = new MLContext();

            var data = context.Data.LoadFromTextFile<SentimentData>("stock_data.csv", hasHeader: true, separatorChar: ',', allowQuoting: true);

            var pipeline = context.Transforms.Expression("Label", "(x) => x == 1 ? true : false", "Sentiment")
                .Append(context.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text)))
                .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);

            var predictionEngine = context.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            var prediction = predictionEngine.Predict(new SentimentData { Text = Message });


            return Ok(prediction);
        }

    }
}
