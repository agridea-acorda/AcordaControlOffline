#region  Copyright (c) Agridea
/////////////////////////////////////////////////////////////////////////////////
//                                                                             //
// Copyright (c) Agridea                                                       //
//                                                                             //
/////////////////////////////////////////////////////////////////////////////////
#endregion

namespace Agridea.ProductInfo
{
    /// <summary>
    /// Gathers common assembly info.
    /// More info: http://stackoverflow.com/questions/64602/what-are-differences-between-assemblyversion-assemblyfileversion-and-assemblyin
    /// using git commits as revision: https://lostechies.com/joshuaflanagan/2010/04/08/adding-git-commit-information-to-your-assemblies/
    /// using julian date as buildnumber: https://intovsts.net/2015/08/24/tfs-build-2015-and-versioning/
    /// </summary>
    public class ProductInfo
    {
        #region This is changed by the build

        public const string Version = "0.1.0.0"; // major.minor.0.0. Minor is prototype number.
        public const string FileVersion = "0.1.20275.0"; // major.minor.{julian date}.{number of commits since the most recent tag}
        public const string InformationalVersion = "0.1 master g25d68aea"; // major.minor {branch name} {git commit ID truncated to 8 chars}
        public const string BuildDate = "2020.10.01 18:00:13";
        public const string Product = "AcordaControlOffline";

        #endregion

        public const string Company = "Agridea.";
        public const string Copyright = "Copyright© Agridea 2017. All rights reserved.";
        public const string Trademark = "Acorda® is a registered trademark of Agridea Inc.";
    }
}