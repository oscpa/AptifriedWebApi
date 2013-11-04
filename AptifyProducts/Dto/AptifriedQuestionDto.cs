using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Text { get; set; }
		public AptifriedKnowledgeCategoryDto KnowledgeCategory { get; set; }
		public AptifriedQuestionTypeDto QuestionType { get; set; }
		public string Description { get; set; }
		public IEnumerable<AptifriedQuestionKnowledgeAnswerDto> QuestionKnowledgeAnswers { get; set; }
	}
}