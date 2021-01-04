using System.Collections.Generic;
using System.Linq;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain
{
    public class Canton : ValueObject
    {
        public const string Unselected = "?";
        public string Code { get; }
        public string Name { get; }
        public string FriendlyName { get; }
        public string ImgSrcBig { get; }
        public string ImgSrc { get; }
        public string ImgSrcInactive { get; }

        public Canton(string code,
                      string name,
                      string friendlyName,
                      string imgSrc,
                      string imgSrcInactive,
                      string imgSrcBig)
        {
            Code = code;
            Name = name;
            FriendlyName = friendlyName;
            ImgSrc = imgSrc;
            ImgSrcInactive = imgSrcInactive;
            ImgSrcBig = imgSrcBig;
        }

        public static Canton GE = new Canton
        (
            "GE",
            "Genève",
            "Canton de Genève",
            "img/cantons/GE.png",
            "img/cantons/GE_inactif.png",
            "img/cantons/GE_big.png"
        );
        public static Canton JU = new Canton
        (
            "JU",
            "Jura",
            "Canton du Jura",
            "img/cantons/JU.png",
            "img/cantons/JU_inactif.png",
            "img/cantons/JU_big.png"
        );
        public static Canton NE = new Canton
        (
            "NE",
            "Neuchâtel",
            "Canton de Neuchâtel",
            "img/cantons/NE.png",
            "img/cantons/NE_inactif.png",
            "img/cantons/NE_big.png"
        );
        public static Canton VD = new Canton
        (
            "VD",
            "Vaud",
            "Canton de Vaud",
            "img/cantons/VD.png",
            "img/cantons/VD_inactif.png",
            "img/cantons/VD_big.png"
        );
        public static Canton[] Cantons = { VD, JU, NE, GE };
        
        public static Canton None = new Canton(
            Unselected,
            "",
            "",
            "",
            "",
            ""
        );

        public static bool IsValid(string cantonCode)
        {
            return Cantons.Select(x => x.Code).Contains(cantonCode);
        }

        public static Canton ParseFromCode(string cantonCode)
        {
            return Cantons.FirstOrDefault(x => x.Code == cantonCode);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
