﻿using System;
using System.Collections.Generic;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class Result : ITreeNode<Result>
    {
        public ITreeNode<Result> Parent { get; private set; }
        public SortedList<string, ITreeNode<Result>> Children { get; } = new SortedList<string, ITreeNode<Result>>();

        public string ConjunctElementCode { get; }
        public string ElementCode { get; }
        public string ShortName { get; }
        public string Name { get; }

        public InspectionOutcome Outcome { get; set; }
        public string InspectorComment { get; set; }
        public string FarmerComment { get; set; }
        
        public Defect Defect { get; private set; }
        public DefectSeriousness Seriousness { get; set; }
        IResult IResult.SetOutcome(InspectionOutcome outcome)
        {
            return SetOutcome(outcome);
        }

        public IResult SetInspectorComment(string comment)
        {
            InspectorComment = comment;
            return this;
        }

        public IResult SetFarmerComment(string comment)
        {
            FarmerComment = comment;
            return this;
        }

        public IResult SetDefect(Defect defect, DefectSeriousness seriousness)
        {
            Defect = defect;
            Seriousness = seriousness;
            return this;
        }

        public double Percent { get; private set; }

        public int NumGroups => Children?.Count(x => x.Value?.Children?.Any() ?? false) ?? 0;
        public int NumPoints => Children?.Count(x => !x.Value?.Children?.Any() ?? true) ?? 0;
        public List<ITreeNode<Result>> Points => (Children?.Where(child => !child.Value?.Children?.Any() ?? true) ?? Enumerable.Empty<KeyValuePair<string, ITreeNode<Result>>>())
                                                 .Select(x => x.Value)
                                                 .ToList();
        public List<ITreeNode<Result>> Groups => (Children?.Where(child => child.Value?.Children?.Any() ?? false) ?? Enumerable.Empty<KeyValuePair<string, ITreeNode<Result>>>())
                                                 .Select(x => x.Value)
                                                 .ToList();

        protected Result(string conjunctElementCode, string elementCode, string shortName, string name = "")
        {
            ConjunctElementCode = conjunctElementCode;
            Name = name;
            ElementCode = elementCode;
            ShortName = shortName;
        }

        public virtual void SetParent(ITreeNode<Result> parent)
        {
            Parent = parent ?? throw new ArgumentNullException();
        }

        public void Traverse(Action<ITreeNode<Result>> action)
        {
            action(this);
            foreach (var child in Children)
            {
                child.Value.Traverse(action);
            }
        }

        protected virtual Result AddChild(string sortKey, ITreeNode<Result> child)
        {
            if (string.IsNullOrWhiteSpace(sortKey))
                throw new ArgumentNullException($"{nameof(sortKey)} must not be empty.");
            
            if (child == null)
                throw new ArgumentNullException();
            
            Children.TryAdd(sortKey, child);
            child.SetParent(this);

            return this;
        }

        public IResult Find(Func<ITreeNode<Result>, bool> condition)
        {
            if (condition(this)) return this;
            foreach (var child in Children)
            {
                var found = child.Value.Find(condition);
                if (found != null) return found;
            }

            return null;
        }

        public IResult Find(string conjunctElementCode)
        {
            if (ConjunctElementCode == conjunctElementCode) return this;
            if (Children.TryGetValue(conjunctElementCode, out var found)) return found;
            return Children.Select(child => child.Value.Find(conjunctElementCode))
                           .FirstOrDefault(found2 => found2 != null);
        }

        internal virtual Result SetOutcome(InspectionOutcome outcome)
        {
            Outcome = outcome;
            return this;
        }
    }
}