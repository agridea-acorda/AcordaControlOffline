using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model.Rubric
{
    public abstract class ResultModelBase
    {
        #region Properties

        
        #endregion

        #region Services

        /*public virtual ResultModelBase MapFrom(IResult result, INode node, int farmInspectionId)
        {
            Id = node.Id;
            ConjunctElementCode = node.ConjunctElementCode;
            ElementCode = node.ElementCode;
            ShortName = node.ShortName;
            Name = node.Name;
            Sort = node.Sort;
            FarmInspectionId = farmInspectionId;
            ResultId = result?.Id ?? default(int);
            ResultOutcome = result?.Outcome;
            ResultFarmerComment = result?.FarmerComment;
            ResultInspectorComment = result?.InspectorComment;
            ResultDefectDescription = result?.DefectDescription;
            ResultSize = result?.Size;
            HasAnyPhoto = result?.PhotoList?.Any() ?? false;
            HasAnyAttachment = result?.AttachmentList.Any() ?? false;
            IsAutoSet = result?.IsAutoSet ?? false;
            DefectId = result?.DefectId ?? default(int);
            DefectName = result?.DefectName;
            Seriousness = result?.Seriousness;
            Repetition = result?.Repetition ?? DefectRepetitions.NoDefect;
            PhotoList = result?.PhotoList != null && result.PhotoList.Any() ? result.PhotoList.Select(x => x.Id).ToArray() : new int[0];
            AttachmentList = result?.AttachmentList != null && result.AttachmentList.Any() ? result.AttachmentList.Select(x => x.Id).ToArray() : new int[0];
            return this;
        }*/

        #endregion
    }

    /*public interface IResult : IInspectionResult
    {
        #region Properties

        string DefectDescription { get; set; }
        int DefectId { get; set; }
        string DefectName { get; set; }
        string FarmerComment { get; set; }
        int Id { get; set; }
        string InspectorComment { get; set; }
        bool IsAutoSet { get; set; }
        DefectRepetitions Repetition { get; set; }
        DefectSeriousnesses? Seriousness { get; set; }
        double? Size { get; set; }

        #endregion
    }

    public sealed class PointResult : AcordaControl.PointResult, IResult
    {
        #region Properties

        public int DefectId { get; set; }
        public string DefectName { get; set; }

        #endregion

        #region Initialization

        public PointResult(AcordaControl.PointResult result)
        {
            Id = result.Id;
            FarmInspection = result.FarmInspection;
            Outcome = result.Outcome;
            FarmerComment = result.FarmerComment;
            InspectorComment = result.InspectorComment;
            DefectDescription = result.DefectDescription;
            Size = result.Size;
            DefectActionList = result.DefectActionList;
            PhotoList = result.PhotoList;
            AttachmentList = result.AttachmentList;
            IsAutoSet = result.IsAutoSet;
            DefectId = result.Defect?.Id ?? 0;
            DefectName = result.Defect?.Name;
            Seriousness = result.Seriousness;
            Repetition = result.Repetition;
        }

        #endregion

        #region Services

        public static PointResult NewOrNull(AcordaControl.PointResult result)
        {
            if (result == null)
                return null;

            return new PointResult(result);
        }

        #endregion
    }

    public sealed class PointGroupResult : AcordaControl.PointGroupResult, IResult
    {
        #region Properties

        public int DefectId { get; set; }
        public string DefectName { get; set; }

        #endregion

        #region Initialization

        public PointGroupResult(AcordaControl.PointGroupResult result)
        {
            Id = result.Id;
            FarmInspection = result.FarmInspection;
            Outcome = result.Outcome;
            FarmerComment = result.FarmerComment;
            InspectorComment = result.InspectorComment;
            DefectDescription = result.DefectDescription;
            Size = result.Size;
            DefectActionList = result.DefectActionList;
            PhotoList = result.PhotoList;
            AttachmentList = result.AttachmentList;
            IsAutoSet = result.IsAutoSet;
            DefectId = result.Defect?.Id ?? 0;
            DefectName = result.Defect?.Name;
            Seriousness = result.Seriousness;
            Repetition = result.Repetition;
        }

        #endregion

        #region Services

        public static PointGroupResult NewOrNull(AcordaControl.PointGroupResult result)
        {
            if (result == null)
                return null;

            return new PointGroupResult(result);
        }

        #endregion
    }

    public sealed class RubricResult : AcordaControl.RubricResult, IResult
    {
        #region Properties

        public int DefectId { get; set; }
        public string DefectName { get; set; }

        #endregion

        #region Initialization

        public RubricResult(AcordaControl.RubricResult result)
        {
            Id = result.Id;
            FarmInspection = result.FarmInspection;
            Outcome = result.Outcome;
            FarmerComment = result.FarmerComment;
            InspectorComment = result.InspectorComment;
            DefectDescription = result.DefectDescription;
            Size = result.Size;
            DefectActionList = result.DefectActionList;
            PhotoList = result.PhotoList;
            AttachmentList = result.AttachmentList;
            IsAutoSet = result.IsAutoSet;
            DefectId = result.Defect?.Id ?? 0;
            DefectName = result.Defect?.Name;
            Seriousness = result.Seriousness;
            Repetition = result.Repetition;
        }

        #endregion

        #region Services

        public static RubricResult NewOrNull(AcordaControl.RubricResult result)
        {
            if (result == null)
                return null;

            return new RubricResult(result);
        }

        #endregion
    }

    public interface INode : AcordaControl.INode
    {
        #region Properties

        int Id { get; set; }
        string Sort { get; set; }

        #endregion
    }

    public sealed class Rubric : AcordaControl.Rubric, INode
    {
        #region Initialization

        public Rubric(AcordaControl.Rubric rubric)
        {
            Id = rubric.Id;
            ConjunctElementCode = rubric.ConjunctElementCode;
            ElementCode = rubric.ElementCode;
            ShortName = rubric.ShortName;
            Sort = rubric.Sort;
        }

        #endregion
    }

    public sealed class PointGroup : AcordaControl.PointGroup, INode
    {
        #region Initialization

        public PointGroup(AcordaControl.PointGroup pointGroup)
        {
            Id = pointGroup.Id;
            ConjunctElementCode = pointGroup.ConjunctElementCode;
            ElementCode = pointGroup.ElementCode;
            ShortName = pointGroup.ShortName;
            Sort = pointGroup.Sort;
        }

        #endregion
    }

    public sealed class Point : AcordaControl.Point, INode
    {
        #region Initialization

        public Point(AcordaControl.Point point)
        {
            Id = point.Id;
            ConjunctElementCode = point.ConjunctElementCode;
            ElementCode = point.ElementCode;
            ShortName = point.ShortName;
            Name = point.Name;
            Sort = point.Sort;
            CustomDescription = point.CustomDescription;
        }

        #endregion
    }

    public static class ResultModelBaseExtensions
    {
        #region Services

        public static Rubric AsINode(this AcordaControl.Rubric rubric)
        {
            return new Rubric(rubric);
        }

        public static RubricResult AsIResult(this AcordaControl.RubricResult result)
        {
            return RubricResult.NewOrNull(result);
        }

        public static PointGroup AsINode(this AcordaControl.PointGroup pointGroup)
        {
            return new PointGroup(pointGroup);
        }

        public static PointGroupResult AsIResult(this AcordaControl.PointGroupResult result)
        {
            return PointGroupResult.NewOrNull(result);
        }

        public static Point AsINode(this AcordaControl.Point point)
        {
            return new Point(point);
        }

        public static PointResult AsIResult(this AcordaControl.PointResult result)
        {
            return PointResult.NewOrNull(result);
        }

        #endregion
    }*/
}
