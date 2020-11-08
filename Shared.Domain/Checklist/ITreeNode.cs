﻿using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface ITreeNode
    {
        ITreeNode Parent { get; }
        SortedList<string, ITreeNode> Children { get; }
        string ConjunctElementCode { get; }
        string Name { get; }
        string ElementCode { get; }
        string ShortName { get; }
    }
}