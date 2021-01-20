using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model.Rubric;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public class RecapResultListItemModel : ResultModelBase, IIndexedRenderer
    {
        #region Properties

        public ResultTypes ResultType { get; set; }
        public int TreeLevel { get; set; }
        public bool HasAutoSetAncestor { get; set; }

        #endregion

        #region Services

        public string Render(int index)
        {
            var sb = new StringBuilder();
            //TODO recuperer les données
            string editResultUrl = "http://todo";
            /*
            var url = U.Url;
            string editResultUrl = ResultType == ResultTypes.Rubric ? url.Action(nameof(RubricResultController.Edit),
                                                                                 nameof(RubricResultController).Controller(),
                                                                                 new
                                                                                 {
                                                                                     id = ResultId,
                                                                                     farmInspectionId = FarmInspectionId,
                                                                                     rubricId = Id
                                                                                     rubricId = Id
                                                                                 }) :
                                   ResultType == ResultTypes.PointGroup ? url.Action(nameof(PointGroupResultController.Edit),
                                                                                     nameof(PointGroupResultController).Controller(),
                                                                                     new
                                                                                     {
                                                                                         id = ResultId,
                                                                                         farmInspectionId = FarmInspectionId,
                                                                                         pointGroupId = Id
                                                                                     }) :
                                   ResultType == ResultTypes.Point ? url.Action(nameof(PointResultController.Edit),
                                                                                nameof(PointResultController).Controller(),
                                                                                new
                                                                                {
                                                                                    id = ResultId,
                                                                                    farmInspectionId = FarmInspectionId,
                                                                                    pointId = Id
                                                                                }) : "#";*/
            string okColor = ResultOutcome == InspectionOutcome.Ok ? Css.BtnSuccess : Css.BtnDefault;
            string pokColor = ResultOutcome == InspectionOutcome.PartiallyOk ? Css.BtnWarning : Css.BtnDefault;
            string nokColor = ResultOutcome == InspectionOutcome.NotOk ? Css.BtnDanger : Css.BtnDefault;
            string ncColor = ResultOutcome == InspectionOutcome.NotInspected ? Css.BtnPrimary : Css.BtnDefault;
            string naColor = ResultOutcome == InspectionOutcome.NotApplicable ? Css.BtnPrimary : Css.BtnDefault;
            string commentsColor = ResultOutcome == InspectionOutcome.Ok ? Css.BtnSuccess :
                                   ResultOutcome == InspectionOutcome.NotOk ? Css.BtnDanger :
                                   ResultOutcome == InspectionOutcome.PartiallyOk ? Css.BtnWarning : Css.BtnPrimary;

            if (HasAnyAttachment)
                sb.AppendLine($"<a href=\"{editResultUrl}\" class=\"{Css.Btn} {commentsColor} {Css.BtnSm} {Css.PullRight} {Css.MarginR5}\"><i class=\"{Fa.Base} {Fa.Paperclip}\"></i></a>");
            if (HasAnyPhoto)
                sb.AppendLine($"<a href=\"{editResultUrl}\" class=\"{Css.Btn} {commentsColor} {Css.BtnSm} {Css.PullRight} {Css.MarginR5}\"><i class=\"{Fa.Base} {Fa.PictureO}\"></i></a>");
            if (HasFailure)
                sb.AppendLine($"<a href=\"{editResultUrl}\" class=\"{Css.Btn} {commentsColor} {Css.BtnSm} {Css.PullRight} {Css.MarginR5}\"><i class=\"{Fa.Base} {Fa.CommentingO}\"></i></a>");

            sb.AppendLine($"<h4 class=\"{Css.ListGroupItemHeading}\">{ConjunctElementCode}</h4>");
            sb.AppendLine($"<p class=\"{Css.ListGroupItemText}\">{ShortName}</p>");
            sb.AppendLine($"<div class=\"{Css.Btn} {okColor} {Css.BtnCircle} {Css.BtnCircleSm}\"><i class=\"{Fa.Base} {Fa.ThumbsOUp}\"></i></div>");
            sb.AppendLine($"<div class=\"{Css.Btn} {pokColor} {Css.BtnCircle} {Css.BtnCircleSm}\">P</div>");
            sb.AppendLine($"<div class=\"{Css.Btn} {nokColor} {Css.BtnCircle} {Css.BtnCircleSm}\"><i class=\"{Fa.Base} {Fa.ThumbsODown}\"></i></div>");
            sb.AppendLine($"<div class=\"{Css.Btn} {ncColor} {Css.BtnCircle} {Css.BtnCircleSm}\">NC</div>");
            sb.AppendLine($"<div class=\"{Css.Btn} {naColor} {Css.BtnCircle} {Css.BtnCircleSm}\">NA</div>");
            sb.AppendLine($"<a href=\"{editResultUrl}\" class=\"{Css.Btn} {Css.BtnDefault} {Css.BtnCircle} {Css.BtnCircleSm}\">...</a>");

            return sb.ToString();
        }

        #endregion

        public enum ResultTypes
        {
            Rubric,
            PointGroup,
            Point
        }
    }
}
