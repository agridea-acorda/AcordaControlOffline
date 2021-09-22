using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public class ResultModel
    {
        #region Properties
        public string ConjunctElementCode { get; set; }
        public int DefectId { get; set; }
        public string DefectName { get; set; }
        public string ElementCode { get; set; }
        public bool HasDefect => DefectId != default(int);
        public bool IsAutoSet { get; set; }
        public string Name { get; set; }
        public string ResultDefectDescription { get; set; }
        public string ResultFarmerComment { get; set; }
        public string ResultInspectorComment { get; set; }
        public InspectionOutcome ResultOutcome { get; set; }
        public double? ResultSize { get; set; }
        public string ResultUnit { get; set; }
        public string Unit { get; set; }
        public string Sort { get; set; }
        public DefectSeriousness Seriousness { get; set; }
        public string ShortName { get; set; }
        
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

        public static ResultModel FromDomain(ITreeNode<Result> node)
        {
            var model = new ResultModel();
            model.ConjunctElementCode = node.ConjunctElementCode;
            model.ElementCode = node.ElementCode;
            model.ShortName = node.ShortName;
            model.Name = node.Name;
            model.ResultOutcome = node.Outcome;
            model.ResultInspectorComment = node.InspectorComment;
            model.ResultFarmerComment = node.FarmerComment;
            model.Unit = node.Unit;
            model.Sort = node.Sort;
            model.Seriousness = node.Seriousness;
            model.IsAutoSet = node.IsAutoSet;
            model.ResultType = node is PointResult ? ResultTypes.Point :
                               node is RubricResult ? ResultTypes.Rubric :
                               ResultTypes.PointGroup;
            if (node.Defect != Defect.None)
            {
                model.ResultDefectDescription = node.Defect.Description;
                if (node.Defect.Size != Defect.Measurement.Unspecified)
                {
                    model.ResultSize = node.Defect.Size.Size;
                    model.ResultUnit = node.Defect.Size.Unit;
                }
            }

            if (node is PointResult point && point.PredefinedDefect != null && point.PredefinedDefect != PredefinedDefect.None)
            {
                model.DefectName = point.PredefinedDefect.Name;
                model.DefectId = point.PredefinedDefect.Id;
            }

            return model;
        }

        public static List<ResultModel> FromDomain(Checklist.Checklist checklist)
        {
            var list = new List<ResultModel>();
            foreach (var r0 in checklist.Rubrics)
            {
                var rubric = r0.Value;
                MapToListRecursive(rubric, list, rubric.IsAutoSet);
            }

            return list;
        }

        private static void MapToListRecursive(ITreeNode<Result> node, List<ResultModel> list, bool hasAutoSetAncestor = false, int treeLevel = 0)
        {
            var model = FromDomain(node);
            model.TreeLevel = treeLevel;
            if (node.IsAutoSet || hasAutoSetAncestor) model.HasAutoSetAncestor = true;
            foreach (var kvp in node.Children)
            {
                
                MapToListRecursive(kvp.Value, list, model.HasAutoSetAncestor, treeLevel + 1);
            }
            list.Add(model);
        }
    }
}
