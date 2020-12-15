using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist
{
    public class ChecklistDeserializationDto
    {
        public int FarmInspectionId { get; set; }
        public SortedList<string, Result> Rubrics { get; set; }

        public class Result
        {
            public SortedList<string, Result> Children { get; set; }

            public string ConjunctElementCode { get; set; }
            public string Name { get; set; }
            public string ElementCode { get; set; }
            public string ShortName { get; set; }
            public InspectionOutcome Outcome { get; set; }
            public string InspectorComment { get; set; }
            public string FarmerComment { get; set; }
            public Defect Defect { get; set; }
            public DefectSeriousness Seriousness { get; set; }
        }

        public class InspectionOutcome
        {

            public string Text { get; set; }
            public int Value { get; set; }
            public string Code { get; set; }

        }

        public class DefectSeriousness
        {

            public int Code { get; set; }
            public string Name { get; set; }
        }

        public class Defect
        {
            public string Description { get; set; }
            public Measurement Size { get; set; }


            public class Measurement
            {
                public double Size { get; set; }
                public string Unit { get; set; }

            }
        }
    }
}
