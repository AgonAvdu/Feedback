using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using com.sun.org.apache.xerces.@internal.impl.dv.xs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using static edu.stanford.nlp.pipeline.CoreNLPProtos;

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


            var context = new MLContext();

            var data = context.Data.LoadFromTextFile<SentimentData>("stock_data.csv", hasHeader: true, separatorChar: ',', allowQuoting: true);

            var pipeline = context.Transforms.Expression("Label", "(x) => x == 1 ? true : false", "Sentiment")
                .Append(context.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text)))
                .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);

            var predictionEngine = context.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            var prediction = predictionEngine.Predict(new SentimentData { Text = feedbackDto.Message });

            var roundedScore = (float)Math.Round(prediction.Score, 2);

            var feedback = new Feedback
            {
                Student = student,
                Teacher = teacher,
                Subject = subject,
                Message = feedbackDto.Message,
                SentimentScore = roundedScore,
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok();
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
