using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model.Rubric;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Shared;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public class RecapResultListItemModel : ResultModelBase
    {
        #region Properties
        public int[] AttachmentList { get; set; }
        public string ConjunctElementCode { get; set; }
        public int DefectId { get; set; }
        public string DefectName { get; set; }
        public string ElementCode { get; set; }
        public int FarmInspectionId { get; set; }
        public bool HasAnyAttachment { get; set; }
        public bool HasAnyPhoto { get; set; }
        public bool HasDefect => DefectId != default(int);
        public bool HasFailure => true;//TODO new ResultHasFailureSpecification().IsSatisfiedBy(this);
        public bool HasResult => ResultOutcome?.Value != -1; //TODO verifier
        public int Id { get; set; }
        public bool IsAutoSet { get; set; }
        public string Name { get; set; }
        public int[] PhotoList { get; set; }
        //public DefectRepetitions Repetition { get; set; }
        public string ResultDefectDescription { get; set; }
        public string ResultFarmerComment { get; set; }
        public int ResultId { get; set; }
        public string ResultInspectorComment { get; set; }
        public InspectionOutcome? ResultOutcome { get; set; }
        public double? ResultSize { get; set; }
        public DefectSeriousness? Seriousness { get; set; }
        public string ShortName { get; set; }
        public string Sort { get; set; }

        public ResultTypes ResultType { get; set; }
        public int TreeLevel { get; set; }
        public bool HasAutoSetAncestor { get; set; }

        #endregion

        public enum ResultTypes
        {
            Rubric,
            PointGroup,
            Point
        }

        public static RecapResultListItemModel FromDomain(ITreeNode<Result> node)
        {
            var model = new RecapResultListItemModel();
            model.ConjunctElementCode = node.ConjunctElementCode;
            model.ShortName = node.ShortName;
            return model;
        }

        public static List<RecapResultListItemModel> FromDomain(Checklist.Checklist checklist)
        {
            return new List<RecapResultListItemModel>();
        }
    }
}
