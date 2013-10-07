using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedQuestionTreeDto {
		public int Id { get; set; }
		public string Name { get; set; }
		public int MaxQuestionsPerPage { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool AllowPartialCompletion { get; set; }
		public bool AllowDuplicates { get; set; }
		public AptifriedQuestionBranchDto RootQuestionBranch { get; set; }
		public AptifriedKnowledgeCategoryDto KnowledgeCategory { get; set; }
		public AptifriedKnowledgeCaptureModeDto KnowledgeCaptureMode { get; set; }
		public AptifriedKnowledgeTrackingTypeDto KnowledgeTrackingType { get; set; }
		public AptifriedKnowledgeStatusDto KnowledgeStatus { get; set; }
		public string Description { get; set; }
		public string EraseMessage { get; set; }
	}
}