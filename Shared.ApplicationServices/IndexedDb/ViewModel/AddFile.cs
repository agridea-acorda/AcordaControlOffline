using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.IndexedDb.ViewModel
{
    public class AddFile
    {
        public string ConjunctElementCode { get; set; }
        public List<FileChecklistVm> fileChecklistVms { get; set; }
    }
}
