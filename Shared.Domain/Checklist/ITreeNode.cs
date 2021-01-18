using System;
using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface ITreeNode<T> : IResult where T: ITreeNode<T>
    {
        ITreeNode<T> Parent { get; }
        SortedList<string, ITreeNode<T>> Children { get; }
        List<ITreeNode<Result>> Points { get; }
        List<ITreeNode<Result>> Groups { get; }
        int NumGroups { get; }
        int NumPoints { get; }
        double Percent { get; }
        InspectionOutcome ComputedOutcome { get; }
        void SetParent(ITreeNode<T> parent);
        void Traverse(Action<ITreeNode<T>> action);
        ITreeNode<Result> Find(Func<ITreeNode<Result>, bool> condition);
        ITreeNode<Result> Find(string conjunctElementCode);
    }
}