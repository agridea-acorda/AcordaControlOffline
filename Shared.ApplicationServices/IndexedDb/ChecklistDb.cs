using IndexedDB.Blazor;
using Microsoft.JSInterop;
using System;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.IndexedDb
{
    public class ChecklistDb : IndexedDB.Blazor.IndexedDb
    {
        public ChecklistDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }
        public IndexedSet<FileChecklist> FileChecklists { get; set; }
    }

    public class FileChecklist
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string ConjunctElementCode { get; set; }
        public int FarmInspectionId { get; set; }
        public bool IsPicture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
