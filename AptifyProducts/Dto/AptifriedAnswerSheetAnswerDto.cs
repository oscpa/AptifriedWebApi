﻿namespace AptifyWebApi.Dto
{
    public class AptifriedAnswerSheetAnswerDto
    {
        public int Id { get; set; }
        public string QuestionCode { get; set; }
        public string StudentAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public decimal PointsEarned { get; set; }
    }
}