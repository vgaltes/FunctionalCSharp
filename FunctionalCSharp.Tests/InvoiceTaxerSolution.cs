namespace FunctionalCSharp.Tests
{
    public static class InvoiceTaxerSolution
    {
        /*
         * Si estamos en temporada alta:
         * - En Europa y USA => 1.5
         * - En Asia y el resto del mundo => 2.0
         * - Si el cliente es premium, => 1.25
         * Si estamos en temporada baja:
         * - Si el cliente es premium: 1.0
         * - Si el cliente es common: 1.25
         */

        public static double CalculatePriceMultiplier(Region region, ClientType clientType, SeasonType seasonType)
        {
            return (region, clientType, seasonType) switch
                {
                    (_, ClientType.Common, SeasonType.Low) => 1.25,
                    (_, ClientType.Premium, SeasonType.Low) => 1.0,
                    (Region.Europe, ClientType.Common, SeasonType.Peak) => 1.5,
                    (Region.Asia, ClientType.Common, SeasonType.Peak) => 2.0,
                    (Region.USA, ClientType.Common, SeasonType.Peak) => 1.5,
                    (Region.Other, ClientType.Common, SeasonType.Peak) => 2.0,
                    (_, ClientType.Premium, SeasonType.Peak) => 1.25
                };

        }
    }
}
