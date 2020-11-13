using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Checklist
{
    public class ChecklistSample
    {
        public ListItem[] Rubrics { get; set; }
        public class ListItem
        {
            public string ConjuntElementCode { get; set; }
            public string Title { get; set; }
            public int NumGroups { get; set; }
            public int NumPoints { get; set; }
        }
    }
}
