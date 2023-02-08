using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.ML;

namespace API.Entities
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public AppUser Student { get; set; }

        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public AppUser Teacher { get; set; }

        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }
        public string Message { get; set; }

        public float SentimentScore { get; set; }

        public void SetScore (string message)
        {
            var context = new MLContext();

            var data = context.Data.LoadFromTextFile<SentimentData>("stock_data.csv", hasHeader: true, separatorChar: ',', allowQuoting: true);

            var pipeline = context.Transforms.Expression("Label", "(x) => x == 1 ? true : false", "Sentiment")
                .Append(context.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text)))
                .Append(context.BinaryClassification.Trainers.SdcaLogisticRegression());

            var model = pipeline.Fit(data);

            var predictionEngine = context.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            var prediction = predictionEngine.Predict(new SentimentData { Text = message });

            var roundedScore = (float)Math.Round(prediction.Score, 2);

            SentimentScore = roundedScore;
        }


    }
}