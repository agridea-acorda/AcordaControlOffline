using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Unit
{
    public class SortListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }

        // <summary>Gets all data fields to sort</summary>
        /// <returns>
        /// The values of the data fields to sort.
        /// </returns>
        public static List<SortListItem> GetSortListItems()
        {
            List<SortListItem> sortListItemsDatasource = new List<SortListItem>
            {
                new SortListItem()
                {
                    Id = "Surface (ares)",
                    Name = "Surface (ares)",
                },
                new SortListItem()
                {
                    Id = "Longueur (mètre)",
                    Name = "Longueur (mètre)",
                },
                new SortListItem()
                {
                    Id = "Nombre (tête, parcelle, analyse ou document manquant/incomplet, etc.)",
                    Name = "Nombre (tête, parcelle, analyse ou document manquant/incomplet, etc.)",
                },
                new SortListItem()
                {
                    Id = "Pourcent (%)",
                    Name = "Pourcent (%)",
                }
            };
            return sortListItemsDatasource;
        }

        // <summary>Gets the display name of a sortListitem</summary>
        /// <returns>
        /// The value of the display name.
        /// </returns>
        public static string GetSortListItemDisplayName(SortListItem sortListItem) => sortListItem != null ? $"{sortListItem.Name}" : null;
    }
}
