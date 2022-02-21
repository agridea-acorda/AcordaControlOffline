using System.Collections.Generic;
using System.Runtime.Serialization;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Farm;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm
{
    public class FarmFactory : AggregateRootFactoryBase<Domain.Farm.Farm>
    {
        public override Domain.Farm.Farm Parse(string json)
        {
            if (json == null) return null;
            var dto = JsonConvert.DeserializeObject<FarmDeserializationDto.Root>(json);
            return Parse(dto);
        }

        public override string Serialize(Domain.Farm.Farm aggregateRoot)
        {
            return JsonConvert.SerializeObject(aggregateRoot,
                                               Formatting.None,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new AggregateRootContractResolver()
                                               });
        }

        private Domain.Farm.Farm Parse(FarmDeserializationDto.Root dto)
        {
            if (dto == null) return null;

            var targetInstance = (Domain.Farm.Farm)FormatterServices.GetUninitializedObject(typeof(Domain.Farm.Farm));
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.Id), targetInstance, dto.Id);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.Ktidb), targetInstance, dto.Ktidb);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.FarmName), targetInstance, dto.FarmName);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.Address), targetInstance, dto.Address);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.TownZip), targetInstance, dto.TownZip);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.FarmType), targetInstance, dto.FarmType);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.FarmTypeCode), targetInstance, dto.FarmTypeCode);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.Email), targetInstance, dto.Email);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.PhoneNumber), targetInstance, dto.PhoneNumber);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.AgriculturalArea), targetInstance, dto.AgriculturalArea);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.NonAgriculturalArea), targetInstance, dto.NonAgriculturalArea);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.BovineStandardUnits), targetInstance, dto.BovineStandardUnits);
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.BovineStandardUnitsFromBdta), targetInstance, dto.BovineStandardUnitsFromBdta);

            var badgeList = new List<Badge>();
            foreach (var dtoBadge in dto.Badges)
            {
                badgeList.Add(Parse(dtoBadge));
            }
            SetPropertyValueViaBackingField(typeof(Domain.Farm.Farm), nameof(Domain.Farm.Farm.Badges), targetInstance, badgeList);
            return targetInstance;
        }

        private Badge Parse(FarmDeserializationDto.Badge dto)
        {
            if (dto == null) return Badge.Empty;
            var targetInstance = (Badge)FormatterServices.GetUninitializedObject(typeof(Badge));
            SetPropertyValueViaBackingField(typeof(Badge), nameof(Badge.Category), targetInstance, dto.Category);
            SetPropertyValueViaBackingField(typeof(Badge), nameof(Badge.Name), targetInstance, dto.Name);
            SetPropertyValueViaBackingField(typeof(Badge), nameof(Badge.Title), targetInstance, dto.Title);
            return targetInstance;
        }
    }
}
