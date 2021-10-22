using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.Config;
using Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Api;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Combo;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Unit;
using Blazored.Toast.Services;
using Blazorise;
using EnsureThat;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using InspectionOutcome = Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.Pages
{
    public partial class Checklist2
    {
        [Inject] private NavigationManager Navigation { get; set; }
        [Inject] IRepository Repository { get; set; }
        [Inject] IToastService Toast { get; set; }
        [Inject] AppConfiguration Config { get; set; }
        [Inject] IJSRuntime Js { get; set; }
        [Inject] IApiClient Api { get; set; }

        [Parameter] public int FarmInspectionId { get; set; }
        const string FarmIdUriKey = "FarmId";
        int farmId;
        Checklist checklist;
        ResultModel parent;
        List<ResultModel> children = new();
        readonly Dictionary<string, (ResultModel, List<ResultModel>)> viewCache = new();
        AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList.InspectionInfo progressBarModel;
        bool saving;
        bool needsSaving;
        bool showAutoSet;
        Blazorise.Modal confirmDelete;
        Blazorise.Modal info;
        Blazorise.Modal edit;
        string conjunctElementCodeToDelete;
        string conjunctElementCodeForInfo;
        MarkupString infoText;
        ResultModel editModel = null;
        //Blazorise.Validations validations;
        private readonly List<SortListItem> sortListItemsDatasource = SortListItem.GetSortListItems();

        protected override async Task OnInitializedAsync()
        {
            farmId = int.Parse(Navigation.QueryString(FarmIdUriKey));
            DateTime started = DateTime.Now;
            Console.WriteLine($"Reading checklist data (started {started.ToDetailedTime()})...");
            await Task.Delay(1);
            checklist = await Repository.ReadChecklistAsync(FarmInspectionId);
            TimeSpan elapsed = DateTime.Now - started;
            Console.WriteLine($"Checklist data read succesfully (elapsed {elapsed}).");
            parent = null;
            if (checklist != null)
            {
                children = checklist.Rubrics.Select(r => ResultModel.MapFrom(r.Value))
                                    .Where(x => x != null)
                                    .ToList();
                progressBarModel = AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList.InspectionInfo.FromChecklist(checklist);
                viewCache.Add("", (parent, children));
            }
        }

        async Task NodeViewChanged(string conjunctElementCode)
        {
            if (string.IsNullOrWhiteSpace(conjunctElementCode))
            {
                //parent = null;
                //children = checklist.Rubrics.Select(r => ChecklistItem.ResultModel.MapFrom(r.Value))
                //                    .Where(x => x != null)
                //                    .ToList();
                await Task.Delay(1);
                var parentOutcomeResult = GetParentOutcomeBasedOnChildren(children);
                //Console.WriteLine($"parentOutcomeResult :" + JsonSerializer.Serialize(parentOutcomeResult, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve }));

                parent = viewCache[""].Item1;
                children = viewCache[""].Item2;

                children.Where(x => x.ConjunctElementCode == parentOutcomeResult.ParentConjunctElementCode).Single().Outcome = parentOutcomeResult.ParentOutcome;

                //Console.WriteLine($"parent :" + JsonSerializer.Serialize(parent, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve }));
                //Console.WriteLine($"children :" + JsonSerializer.Serialize(children, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve }));

                Console.WriteLine($"conjunctElementCode is null or space");
                return;
            }

            Console.WriteLine($"Finding node with conjunctElementCode={conjunctElementCode}");
            var node = checklist.Find(conjunctElementCode);
            if (node == null)
            {
                //parent = null;
                //children = checklist.Rubrics.Select(r => ChecklistItem.ResultModel.MapFrom(r.Value))
                //                    .Where(x => x != null)
                //                    .ToList();

                parent = viewCache[""].Item1;
                children = viewCache[""].Item2;
                Console.WriteLine($"node is null");
                return;
            }

            if (viewCache.TryGetValue(node.ConjunctElementCode, out var entry))
            {
                await Task.Delay(1);
                var parentOutcomeResult = GetParentOutcomeBasedOnChildren(children);

                parent = entry.Item1;
                children = entry.Item2;

                var temp = children.SingleOrDefault(x => x.ConjunctElementCode == parentOutcomeResult.ParentConjunctElementCode);
                if (temp != null)
                {
                    temp.Outcome = parentOutcomeResult.ParentOutcome;
                }
                Console.WriteLine($"from view cache");
                return;
            }
            parent = ResultModel.MapFrom(node);
            children = node.Children.Select(x => ResultModel.MapFrom(x.Value))
                           .Where(x => x != null)
                           .ToList();
            viewCache.TryAdd(node.ConjunctElementCode, (parent, children));

        }

        async Task NodeOutcomeChanged(ResultModel model)
        {
            Console.WriteLine($"Node {model.ConjunctElementCode}'s outcome was set to {model.Outcome}.");
            var node = checklist.Find(model.ConjunctElementCode);
            var previousOutcome = node.Outcome;
            node.SetOutcome(model.Outcome);
            if (previousOutcome != model.Outcome) node.SetAuto(false);
            await Task.Delay(1);
            GetParentOutcomeBasedOnChildren(children);
            await CheckRulesFromParentToChildren(model.ConjunctElementCode, model.Outcome);
            progressBarModel.SetOutcome(checklist.OutcomeComputed);
            progressBarModel.Progress(checklist.Percent);
            needsSaving = true;
        }

        void NodeEditing(string conjunctElementCode)
        {
            var model = children.SingleOrDefault(x => x.ConjunctElementCode == conjunctElementCode);
            Ensure.That(model, nameof(model)).IsNotNull();
            editModel = model;
            edit.Show();
        }

        void EditNode()
        {
            Console.WriteLine($"Modifying result for node {editModel.ConjunctElementCode}...");

            var model = children.SingleOrDefault(x => x.ConjunctElementCode == editModel.ConjunctElementCode);
            Ensure.That(model, nameof(model)).IsNotNull();

            model.UpdateFrom(editModel);

            checklist.Find(editModel.ConjunctElementCode)
                .SetInspectorComment(editModel.InspectorComment)
                .SetFarmerComment(editModel.FarmerComment)
                .SetDefect(new Defect(editModel.DefectDescription, new Defect.Measurement(editModel.DefectSize, editModel.DefectUnit)), DefectSeriousness.FromCode(editModel.SeriousnessCode))
                .SetDefectId(editModel.DefectId)
                .SetUnit(editModel.Unit)
                .SetAuto(false);

            GetParentOutcomeBasedOnChildren(children);
            progressBarModel.Progress(checklist.Percent);
            progressBarModel.SetOutcome(checklist.OutcomeComputed);
            needsSaving = true;
            editModel = default;

            Console.WriteLine($"Modified result for node {editModel.ConjunctElementCode}.");
        }

        void EditOk()
        {
            Validate();
            edit.Hide();
        }

        void EditCancelled()
        {
            Validate();
            edit.Hide();
        }

        private void Validate()
        {
            //if (validations.ValidateAll())
            //{
            //    edit.Hide();
            //}
            //else
            //{
            //    editModel.DefectDescription = null;
            //}
        }

        public void ValidateDefectDescription(ValidatorEventArgs e)
        {
            e.Status = editModel.DefectId != null &&
                       editModel.DefectId != 0 &&
                       !string.IsNullOrEmpty(editModel.DefectDescription)
                ? ValidationStatus.Error
                : ValidationStatus.None;
        }

        void NodeDeleting(string conjunctElementCode)
        {
            conjunctElementCodeToDelete = conjunctElementCode;
            confirmDelete.Show();
        }

        async Task DeleteNode()
        {
            Console.WriteLine($"Deleting result for node {conjunctElementCodeToDelete}...");
            confirmDelete.Hide();

            var model = children.SingleOrDefault(x => x.ConjunctElementCode == conjunctElementCodeToDelete);
            Ensure.That(model, nameof(model)).IsNotNull();

            model.Clear();
            await Task.Delay(1);

            checklist.Find(model.ConjunctElementCode)
                .SetOutcome(InspectionOutcome.Unset)
                .SetInspectorComment("")
                .SetFarmerComment("")
                .SetDefect(Defect.None, DefectSeriousness.Empty)
                .SetDefectId(null)
                .SetAuto(false);

            await Task.Delay(1);
            GetParentOutcomeBasedOnChildren(children);

            progressBarModel.Progress(checklist.Percent);
            progressBarModel.SetOutcome(checklist.OutcomeComputed);
            needsSaving = true;
            conjunctElementCodeToDelete = "";
            Console.WriteLine($"Result deleted for node {model.ConjunctElementCode}.");
        }

        void CancelDeleteNode()
        {
            conjunctElementCodeToDelete = "";
            confirmDelete.Hide();
        }

        void NodeDisplayingInfo(string conjunctElementCode)
        {
            var model = children.SingleOrDefault(x => x.ConjunctElementCode == conjunctElementCode);
            Ensure.That(model, nameof(model)).IsNotNull();
            infoText = (MarkupString)model.Name?.CurateAsReadableText();
            conjunctElementCodeForInfo = conjunctElementCode;
            info.Show();
        }

        void HideInfo()
        {
            infoText = default;
            conjunctElementCodeForInfo = default;
            info.Hide();
        }

        async Task Save()
        {
            if (saving || !needsSaving) return;
            saving = true;
            await Task.Delay(1);
            StateHasChanged();
            GetParentOutcomeBasedOnChildren(children);
            try
            {
                Console.WriteLine("Saving checklist...");
                await Js.InvokeVoidAsync("console.time", "Checklist2.Save");

                await Repository.SaveChecklistAsync(checklist);

                // below should be performed with domain event dispatching mechanism
                var checklistEvent = new NodeOutcomeChanged(checklist.OutcomeComputed, checklist.Percent, FarmInspectionId);
                await UpdateAndSaveInspectionPercentAndOutcome(Repository, farmId, checklistEvent);
                await UpdateAndSaveInspectionInfoPercentAndOutcome(Repository, farmId, checklistEvent);
                Console.WriteLine("...checklist saved.");
                Toast.Success();
            }
            finally
            {
                saving = false;
                needsSaving = false;
                await Js.InvokeVoidAsync("console.timeEnd", "Checklist2.Save");
                StateHasChanged();
            }
        }

        private async Task UpdateAndSaveInspectionPercentAndOutcome(IRepository repository, int farmId, NodeOutcomeChanged e)
        {
            var mandate = await repository.ReadMandateAsync(farmId);
            if (mandate == null)
            {
                string error = $"Mandat introuvable (FarmId={farmId})";
                Console.WriteLine(error);
                Toast.Error(error);
                return;
            }

            var inspection = mandate.Inspections.FirstOrDefault(x => x.FarmInspectionId == e.FarmInspectionId);
            if (inspection == null)
            {
                string error = $"Contrôle introuvable (FarmInspectionId={e.FarmInspectionId})";
                Console.WriteLine(error);
                Toast.Error(error);
                return;
            }
            inspection.Progress(e.Percent * 100);
            inspection.SetOutcome(e.Outcome);
            await repository.SaveMandateAsync(mandate, farmId);
        }

        private async Task UpdateAndSaveInspectionInfoPercentAndOutcome(IRepository repository, int farmId, NodeOutcomeChanged e)
        {
            var mandates = await repository.ReadAllMandatesAsync();
            var mandate = mandates.FirstOrDefault(x => x.Farm.Id == farmId);
            if (mandate == null)
            {
                string error = $"Mandat introuvable (FarmId={farmId})";
                Console.WriteLine(error);
                Toast.Error(error);
                return;
            }

            var inspectionInfo = mandate.Inspections.FirstOrDefault(x => x.FarmInspectionId == e.FarmInspectionId);
            if (inspectionInfo == null)
            {
                string error = $"Contrôle introuvable (FarmInspectionId={e.FarmInspectionId})";
                Console.WriteLine(error);
                Toast.Error(error);
                return;
            }

            inspectionInfo.SetOutcome(e.Outcome);
            inspectionInfo.Progress(e.Percent);
            await repository.SaveMandatesAsync(mandates);
        }

        /// <summary>Checks if InspectionOutcome of parent is correct (Could be incorrect if children's InspectionOutcome have a value)</summary>
        /// <param name="conjunctElementCode">The code of the id of the node of a InspectionOutcome</param>
        /// <param name="check">The InspectionOutcome to check</param>
        async Task CheckRulesFromParentToChildren(string conjunctElementCode, InspectionOutcome check)
        {
            var node = checklist.Find(conjunctElementCode);
            var tempChildren = new List<ResultModel>();
            if (viewCache.TryGetValue(conjunctElementCode, out var entry))
            {
                await Task.Delay(1);
                tempChildren = entry.Item2;
            }
            else
            {
                tempChildren = node.Children.Select(x => ResultModel.MapFrom(x.Value))
                .Where(x => x != null)
                .ToList();
            }

            var temp = tempChildren.Where(x => x.ParentConjunctElementCode == conjunctElementCode).ToList();
            int cptNOk = 0;
            int cptPOk = 0;
            int cptOk = 0;
            int cptNa = 0;
            int cptNc = 0;

            //Console.WriteLine($"check child count :" + temp.Count);
            foreach (var child in temp)
            {
                //Console.WriteLine($"check child :" + child.Outcome.Code);

                switch (child.Outcome.Code)
                {
                    case "NOk":
                        cptNOk++;
                        break;
                    case "PartiallyOk":
                        cptPOk++;
                        break;
                    case "Ok":
                        cptOk++;
                        break;
                    case "NotApplicable":
                        cptNa++;
                        break;
                    case "NotInspected":
                        cptNc++;
                        break;
                }
            }

            //Console.WriteLine(check.Code);
            //Console.WriteLine(cptNOk);
            //Console.WriteLine(cptPOk);
            //Console.WriteLine(cptOk);
            //Console.WriteLine(cptNa);
            //Console.WriteLine(cptNc);
            if (cptNOk > 0 && check == InspectionOutcome.NotOk)
            {
                //Toast.ShowWarning("Le point de contrôle est cohérent (NOk)");
            }
            else if (cptPOk > 0 && check == InspectionOutcome.PartiallyOk && cptNOk == 0)
            {
                //Toast.ShowWarning("Le point de contrôle est cohérent (POk)");
            }
            else if (cptOk > 0 && check == InspectionOutcome.Ok && cptNOk == 0 && cptPOk == 0)
            {
                //Toast.ShowWarning("Le point de contrôle est cohérent (Ok)");
            }
            else if (cptNa > 0 && check == InspectionOutcome.NotApplicable && cptNOk == 0 && cptPOk == 0 && cptOk == 0)
            {
                //Toast.ShowWarning("Le point de contrôle est cohérent (Na)");
            }
            else if (cptNc > 0 && check == InspectionOutcome.NotInspected && cptNOk == 0 && cptPOk == 0 && cptOk == 0 && cptNa == 0)
            {
                //Toast.ShowWarning("Le point de contrôle est cohérent (Nc)");
            }
            else if (cptNOk == 0 && cptPOk == 0 && cptOk == 0 && cptNa == 0 && cptNc == 0)
            {

            }
            else
            {
                Toast.ShowWarning($"Le point de contrôle est incohérent ({check.Code}), vérifiez le sous-groupe", "ATTENTION");
            }
        }

        /// <summary>Calculates and sets the InspectionOutcome's value of the parents nodes based on children</summary>
        /// <param name="childNodes">The children, needed to calculate the outcome</param>
        ParentOutcomeResult GetParentOutcomeBasedOnChildren(List<ResultModel> childNodes)
        {
            //Console.WriteLine($"children :" + JsonSerializer.Serialize(children, new JsonSerializerOptions(){ReferenceHandler = ReferenceHandler.Preserve }));
            ParentOutcomeResult parentOutcomeResult = new ParentOutcomeResult();

            if (childNodes != null)
            {

                parentOutcomeResult.ParentConjunctElementCode = childNodes.FirstOrDefault()?.ParentConjunctElementCode;

                int cptNOk = 0;
                int cptPOk = 0;
                int cptOk = 0;
                int cptNa = 0;
                int cptNc = 0;
                //Console.WriteLine($"children :" + children.Count);
                foreach (var child in childNodes)
                {
                    //Console.WriteLine($"child :" + child.Outcome.Code);

                    switch (child.Outcome.Code)
                    {
                        case "NOk":
                            cptNOk++;
                            break;
                        case "PartiallyOk":
                            cptPOk++;
                            break;
                        case "Ok":
                            cptOk++;
                            break;
                        case "NotApplicable":
                            cptNa++;
                            break;
                        case "NotInspected":
                            cptNc++;
                            break;
                    }
                }


                if (cptNOk > 0)
                {
                    //Console.WriteLine($"outcomeParent SHOULD BE NOK");
                    parentOutcomeResult.ParentOutcome = InspectionOutcome.NotOk;
                }
                else if (cptPOk > 0)
                {
                    //Console.WriteLine($"outcomeParent SHOULD BE POK");
                    parentOutcomeResult.ParentOutcome = InspectionOutcome.PartiallyOk;
                }
                else if (cptOk > 0)
                {
                    //Console.WriteLine($"outcomeParent SHOULD BE OK");
                    parentOutcomeResult.ParentOutcome = InspectionOutcome.Ok;
                }
                else if (cptNa > 0)
                {
                    //Console.WriteLine($"outcomeParent SHOULD BE NA");
                    parentOutcomeResult.ParentOutcome = InspectionOutcome.NotApplicable;
                }
                else if (cptNc > 0)
                {
                    //Console.WriteLine($"outcomeParent SHOULD BE NC");
                    parentOutcomeResult.ParentOutcome = InspectionOutcome.NotInspected;
                }
                else
                {
                    //Console.WriteLine($"outcomeParent SHOULD BE NOTHING");
                    parentOutcomeResult.ParentOutcome = InspectionOutcome.Unset;
                }
            }

            string parentConjunctElementCode = parent?.ConjunctElementCode;
            if (parentConjunctElementCode != null)
            {
                //Console.WriteLine($"parentConjunctElementCode :" + JsonSerializer.Serialize(parentConjunctElementCode, new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve }));
                var node = checklist.Find(parentConjunctElementCode);
                //Console.WriteLine(JsonSerializer.Serialize(node, new JsonSerializerOptions(){ReferenceHandler = ReferenceHandler.Preserve}));
                var previousOutcome = node.Outcome;
                node.SetOutcome(parentOutcomeResult.ParentOutcome);
                parent.Outcome = parentOutcomeResult.ParentOutcome;
                if (previousOutcome != parentOutcomeResult.ParentOutcome) node.SetAuto(false);
            }

            progressBarModel.SetOutcome(checklist.OutcomeComputed);
            progressBarModel.Progress(checklist.Percent);

            return parentOutcomeResult;
        }

        private class ParentOutcomeResult
        {
            public string ParentConjunctElementCode { get; set; }
            public InspectionOutcome ParentOutcome { get; set; }
        }

        public class ResultModel
        {
            public int NumGroups { get; set; }
            public int NumPoints { get; set; }
            public string ConjunctElementCode { get; set; }
            public string ParentConjunctElementCode { get; set; }
            public string ShortName { get; set; }
            public string Name { get; set; }
            public InspectionOutcome Outcome { get; set; }
            public bool IsAutoSet { get; set; }
            public string InspectorComment { get; set; }
            public string FarmerComment { get; set; }
            public string DefectDescription { get; set; }
            public double DefectSize { get; set; }
            public string DefectUnit { get; set; }
            public string Unit { get; set; }
            public int SeriousnessCode { get; set; }
            public string Id { get; set; }
            public IEnumerable<SelectListItem<int>> ComboSeriousnesses { get; set; }
            public int? DefectId { get; set; }
            public IEnumerable<SelectListItem<string>> ComboDefects { get; set; }
            public int? PointId { get; set; }
            public bool HasChildren { get; set; }
            public string Sort { get; set; }

            public void Clear()
            {
                Outcome = InspectionOutcome.Unset;
                InspectorComment = "";
                FarmerComment = "";
                DefectDescription = Defect.None.Description;
                DefectSize = Defect.None.Size.Size;
                DefectUnit = Defect.None.Size.Unit;
                Unit = "";
                SeriousnessCode = DefectSeriousness.Empty.Code;
                IsAutoSet = false;
            }

            public void UpdateFrom(ResultModel other)
            {
                Outcome = other.Outcome;
                InspectorComment = other.InspectorComment;
                FarmerComment = other.FarmerComment;
                DefectDescription = other.DefectDescription;
                DefectSize = other.DefectSize;
                DefectUnit = other.DefectUnit;
                Unit = other.Unit;
                SeriousnessCode = other.SeriousnessCode;
                IsAutoSet = false;
            }

            public static ResultModel MapFrom(ITreeNode<Result> node)
            {
                if (node == null) return null;
                return new ResultModel
                {
                    NumGroups = node.NumGroups,
                    NumPoints = node.NumPoints,
                    ConjunctElementCode = node.ConjunctElementCode,
                    ParentConjunctElementCode = node.Parent?.ConjunctElementCode ?? "",
                    ShortName = node.ShortName,
                    Name = node.Name,
                    Outcome = node.Outcome,
                    IsAutoSet = node.IsAutoSet,
                    Id = node.ConjunctElementCode.CurateAsElementId(),
                    InspectorComment = node.InspectorComment,
                    FarmerComment = node.FarmerComment,
                    DefectDescription = node.Defect.Description,
                    DefectSize = node.Defect.Size.Size,
                    DefectUnit = node.Defect.Size.Unit,
                    Unit = node.Unit,
                    SeriousnessCode = node.Seriousness.Code,
                    ComboSeriousnesses = Combo.Seriousnesses(),
                    ComboDefects = node.ComboDefects != null ? node.ComboDefects : new List<SelectListItem<string>>(),
                    PointId = node.PointId,
                    DefectId = node.DefectId,
                    HasChildren = node.Children?.Any() ?? false,
                    Sort = node.Sort
                };
            }
        }
    }
}
