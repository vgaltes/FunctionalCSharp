using System;

namespace FunctionalCSharp.Tests
{
    public static class InvoiceTaxer
    {
        /*
         * Si estamos en temporada alta:
         * - En Europa y USA => 1.5
         * - En Asia y el resto del mundo => 2.0
         * - Si el cliente es premium => 1.25
         * Si estamos en temporada baja:
         * - Si el cliente es premium: 1.0
         * - Si el cliente es common: 1.25
         */

        public static double CalculatePriceMultiplier(Region region, ClientType clientType, SeasonType seasonType)
        {
            var multiplier = 1.0;

            if (seasonType == SeasonType.Peak)
            {
                if (clientType == ClientType.Common)
                {
                    switch (region)
                    {
                        case Region.Europe:
                        case Region.USA:
                            multiplier = 1.5;
                            break;
                        case Region.Asia:
                        case Region.Other:
                            multiplier = 2.0;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(region), region, "Wrong region");
                    }
                }
                else
                {
                    multiplier = 1.25;
                }
            }
            else
            {
                if (clientType == ClientType.Common) multiplier = 1.25;
                if (clientType == ClientType.Premium) multiplier = 1.0;
            }

            return multiplier;
        }
    }

    public enum Region
    {
        Europe,
        Asia,
        USA,
        Other
    }

    public enum ClientType
    {
        Common,
        Premium
    }

    public enum SeasonType
    {
        Peak,
        Low
    }
}
