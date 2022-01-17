using IndexedDB.Blazor;
using Microsoft.JSInterop;
using System;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.IndexedDb
{
    public class FormInscriptionDb : IndexedDB.Blazor.IndexedDb
    {
        public FormInscriptionDb(IJSRuntime jSRuntime, string name, int version) : base(jSRuntime, name, version) { }
        public IndexedSet<FormInscription> FormInscriptions { get; set; }
    }

    public class FormInscription
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int FarmId { get; set; }
        public byte[] FileData { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
