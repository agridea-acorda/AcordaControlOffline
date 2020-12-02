using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection
{
    public class InspectionDeserializationDto
    {
        public class Domain
        {
            public int Id { get; set; }
            public string ShortName { get; set; }
        }

        public class Campaign
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Reason
        {
            public int Code { get; set; }
            public string Name { get; set; }
        }

        public class Mode
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        public class Appointment
        {
            public Mode Mode { get; set; }
            public object FirstContactDate { get; set; }
            public object Date { get; set; }
        }

        public class Status
        {
            public int Code { get; set; }
            public string Name { get; set; }
        }

        public class OutcomeComputed
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public string Code { get; set; }
        }

        public class Signature
        {
            public string Signatory { get; set; }
            public string Proxy { get; set; }
            public string Data { get; set; }
            public string DataUrl { get; set; }
            public bool HasProxy { get; set; }
            public bool HasSigned { get; set; }
        }

        public class Compliance
        {
            public string ActionsOrDocuments { get; set; }
            public object DueDate { get; set; }
            public bool DueDateNotRespected { get; set; }
            public bool DueDateRespected { get; set; }
            public bool FurtherInvestigationNeeded { get; set; }
            public bool IncompleteOrNonCompliant { get; set; }
        }

        public class FinishStatus
        {
            public string DoneByInspector { get; set; }
            public object DoneOn { get; set; }
            public bool IsFinished { get; set; }
        }

        public class CloseStatus
        {
            public object CloseDate { get; set; }
            public string ClosedBy { get; set; }
            public bool IsClosed { get; set; }
        }

        public class ReopenStatus
        {
            public object ReopenDate { get; set; }
            public string ReopenedBy { get; set; }
            public bool IsReopened { get; set; }
        }

        public class Root
        {
            public int FarmInspectionId { get; set; }
            public string InspectionId { get; set; }
            public int ChecklistId { get; set; }
            public int FarmId { get; set; }
            public Domain Domain { get; set; }
            public Campaign Campaign { get; set; }
            public Reason Reason { get; set; }
            public string Comment { get; set; }
            public Appointment Appointment { get; set; }
            public string CommentForFarmer { get; set; }
            public string CommentForOffice { get; set; }
            public Status Status { get; set; }
            public double PercentComputed { get; set; }
            public DateTime DateComputed { get; set; }
            public OutcomeComputed OutcomeComputed { get; set; }
            public Signature InspectorSignature { get; set; }
            public Signature Inspector2Signature { get; set; }
            public Signature FarmerSignature { get; set; }
            public Compliance Compliance { get; set; }
            public FinishStatus FinishStatus { get; set; }
            public CloseStatus CloseStatus { get; set; }
            public ReopenStatus ReopenStatus { get; set; }
            public int Id { get; set; }
        }
    }
}
