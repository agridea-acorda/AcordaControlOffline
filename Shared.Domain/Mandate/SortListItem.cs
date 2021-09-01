/**
 * Project: Acorda Control Offline
 * Author : Tim Allemann
 * Date : 08.07.2021
 * Description : This is needed to use component sorting in mandatelist view
 * File : SortListItem.cs
 **/

using System.Collections.Generic;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class SortListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool IsAscending { get; set; }

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
                    Id = "NameASC",
                    Name = "Nom",
                    Icon = "<i class=\"fas fa-sort-alpha-down\"></i>",
                    IsAscending = true
                },
                new SortListItem()
                {
                    Id = "NameDESC",
                    Name = "Nom",
                    Icon = "<i class=\"fas fa-sort-alpha-down-alt\"></i>",
                    IsAscending = false
                },
                new SortListItem()
                {
                    Id = "CommuneASC",
                    Name = "Commune",
                    Icon = "<i class=\"fas fa-sort-alpha-down\"></i>",
                    IsAscending = true
                },
                new SortListItem()
                {
                    Id = "CommuneDESC",
                    Name = "Commune",
                    Icon = "<i class=\"fas fa-sort-alpha-down-alt\"></i>",
                    IsAscending = false
                },
                new SortListItem()
                {
                    Id = "LocaliteASC",
                    Name = "Localité",
                    Icon = "<i class=\"fas fa-sort-alpha-down\"></i>",
                    IsAscending = true
                },
                new SortListItem()
                {
                    Id = "LocaliteDESC",
                    Name = "Localité",
                    Icon = "<i class=\"fas fa-sort-alpha-down-alt\"></i>",
                    IsAscending = false
                },
                new SortListItem()
                {
                    Id = "KtidbASC",
                    Name = "Ktidb",
                    Icon = "<i class=\"fas fa-sort-alpha-down\"></i>",
                    IsAscending = true
                },
                new SortListItem()
                {
                    Id = "KtidbDESC",
                    Name = "Ktidb",
                    Icon = "<i class=\"fas fa-sort-alpha-down-alt\"></i>",
                    IsAscending = false
                },
                new SortListItem()
                {
                    Id = "FormeExASC",
                    Name = "Forme d'exploitation",
                    Icon = "<i class=\"fas fa-sort-alpha-down\"></i>",
                    IsAscending = true
                },
                new SortListItem()
                {
                    Id = "FormeExDESC",
                    Name = "Forme d'exploitation",
                    Icon = "<i class=\"fas fa-sort-alpha-down-alt\"></i>",
                    IsAscending = false
                }
            };
            return sortListItemsDatasource;
        }

        // <summary>Gets the display name of a sortListitem</summary>
        /// <returns>
        /// The value of the display name.
        /// </returns>
        public static string GetSortListItemDisplayName(SortListItem sortListItem) => sortListItem != null ? $"{sortListItem.Name} {sortListItem.Icon}" : null;

    }
}
