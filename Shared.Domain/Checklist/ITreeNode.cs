using System;
using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface ITreeNode<T> : IResult, IProgressable
        where T: ITreeNode<T>
    {
        ITreeNode<T> Parent { get; }
        SortedList<string, ITreeNode<T>> Children { get; }
        void SetParent(ITreeNode<T> parent);
        void Traverse(Action<ITreeNode<T>> action);
        IResult Find(Func<ITreeNode<Result>, bool> condition);
        IResult Find(string conjunctElementCode);
    }
}