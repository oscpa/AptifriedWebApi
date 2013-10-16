using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aptify.Framework.BusinessLogic.GenericEntity;
using AptifyWebApi.Dto;
using AptifyWebApi.Models;
using NHibernate;

namespace AptifyWebApi.Controllers {
	public class AptifriedKnowledgeResultDetailController : AptifyEnabledApiController {
		public AptifriedKnowledgeResultDetailController(ISession session) : base(session) { }

		public AptifriedKnowledgeResultDetailDto Post(AptifriedKnowledgeResultDetailDto postDetail) {
			if (postDetail == null) {
				throw new HttpException(500, "Detail can't be null");
			}

			AptifyGenericEntityBase geDetail = AptifyApp.GetEntityObject("KnowledgeResultDetails", -1);
			if (geDetail == null) {
				throw new HttpException(500, "Null GE received for new KnowledgeResultDetails");
			}

			geDetail.SetValue("KnowledgeResultID", postDetail.KnowledgeResult.Id);
			geDetail.SetValue("Sequence", postDetail.Sequence);
			geDetail.SetValue("QuestionTreeID", postDetail.QuestionTree.Id);
			geDetail.SetValue("QuestionBranchID", postDetail.QuestionBranch.Id);
			geDetail.SetValue("QuestionID", postDetail.Question.Id);
			geDetail.SetValue("KnowledgeAnswerID", postDetail.KnowledgeAnswer.Id);
			geDetail.SetValue("KnowledgeAnswerValue", postDetail.KnowledgeAnswerValue);
			geDetail.SetValue("HTMLName", postDetail.HtmlName);

			if (!geDetail.Save(false)) {
				throw new HttpException(500, geDetail.LastUserError);
			}

			postDetail.Id = Convert.ToInt32(geDetail.RecordID);

			return postDetail;
		}
	}
}