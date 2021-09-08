using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Filter
    {
        public string Ktidb { get; set; }
        public string FarmName { get; set; }

        public List<SortListItem> Domains = new List<SortListItem>();
        // Datasource to populate
        public List<SortListItem> DomainsDS = new List<SortListItem>();

        public List<SortListItem> Inspectors = new List<SortListItem>();
        // Datasource to populate
        public List<SortListItem> InspectorsDS = new List<SortListItem>();

        public List<SortListItem> Reasons = new List<SortListItem>();
        // Datasource to populate
        public List<SortListItem> ReasonsDS = new List<SortListItem>();

        public List<SortListItem> Campaigns = new List<SortListItem>();
        // Datasource to populate
        public List<SortListItem> CampaignsDS = new List<SortListItem>();
    }
}
