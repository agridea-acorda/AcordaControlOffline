namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class DefectSeriousness : CodeNameValueObject
    {
        public static DefectSeriousness Empty => new DefectSeriousness(0, "");
        public static DefectSeriousness Small => new DefectSeriousness(1, "Minime");
        public static DefectSeriousness Medium => new DefectSeriousness(2, "Important");
        public static DefectSeriousness Serious => new DefectSeriousness(3, "Grave");
        public DefectSeriousness(int code, string name) : base(code, name) { }

        protected override void ValidateCtorParams(int code, string name)
        {
            bool IsEmpty() => code == 0 && name == "";
            if (IsEmpty()) return;
            base.ValidateCtorParams(code, name);
        }

        public static DefectSeriousness FromCode(int code)
        {
            return code == Empty.Code ? Empty :
                   code == Small.Code ? Small :
                   code == Medium.Code ? Medium :
                   code == Serious.Code ? Serious :
                   Empty;
        }
    }
}