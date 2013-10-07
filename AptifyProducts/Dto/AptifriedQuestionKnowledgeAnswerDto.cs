using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionKnowledgeAnswerDto {
		public int Id { get; set; }
		public AptifriedQuestionDto Question { get; set; }
		public int Sequence { get; set; }
		public AptifriedKnowledgeAnswerDto KnowledgeAnswer { get; set; }
		public bool AnswerRequired { get; set; }
	}
}