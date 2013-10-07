using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedQuestionTree {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual int MaxQuestionsPerPage { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual bool AllowPartialCompletion { get; set; }
		public virtual bool AllowDuplicates { get; set; }
		public virtual AptifriedQuestionBranch RootQuestionBranch { get; set; }
		public virtual AptifriedKnowledgeCategory KnowledgeCategory { get; set; }
		public virtual AptifriedKnowledgeCaptureMode KnowledgeCaptureMode { get; set; }
		public virtual AptifriedKnowledgeTrackingType KnowledgeTrackingType { get; set; }
		public virtual AptifriedKnowledgeStatus KnowledgeStatus { get; set; }
		public virtual string Description { get; set; }
		public virtual string EraseMessage { get; set; }
	}
}