﻿using System;
using System.Collections.Generic;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public abstract class Result : ITreeNode<Result>
    {
        public ITreeNode<Result> Parent { get; private set; }
        public SortedList<string, ITreeNode<Result>> Children { get; } = new SortedList<string, ITreeNode<Result>>();

        public string ConjunctElementCode { get; }
        public string Name { get; }
        public string ElementCode { get; }
        public string ShortName { get; }
        public InspectionOutcome Outcome { get; set; }
        public string InspectorComment { get; set; }
        public string FarmerComment { get; set; }
        public string DefectDescription { get; set; }
        public double? Size { get; set; }
        public DefectSeriousness Seriousness { get; set; }
        public IList<DefectAction> DefectActions { get; private set; }
        public double Percent { get; private set; }

        protected Result(string conjunctElementCode, string elementCode, string shortName, string name = "")
        {
            DefectActions = new List<DefectAction>();
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

        public IResult Find(string key)
        {
            IResult match = null;
            Traverse(x =>
            {
                if (ConjunctElementCode == key) match = x;
            });
            return match;
        }

        internal virtual Result SetOutcome(InspectionOutcome outcome)
        {
            Outcome = outcome;
            return this;
        }
    }
}