using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionBranchDto {
		public int Id { get; set; }
		public int QuestionTreeId { get; set; }
		public AptifriedQuestionDto Question { get; set; }
		public AptifriedQuestionBranchDto NextQuestionBranch { get; set; }
		public bool CheckAnswers { get; set; }
		public bool PageBreak { get; set; }
		public bool ForceSave { get; set; }
		public bool AnswerRequired { get; set; }
	}
}