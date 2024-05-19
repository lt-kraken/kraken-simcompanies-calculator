using Newtonsoft.Json;

namespace SimCompaniesCalculator
{
    public class ResourceInput
    {
        [JsonProperty("amount")]
        public decimal? Amount;

        [JsonProperty("price")]
        public decimal? Price;

        [JsonProperty("quality")]
        public int? Quality;

        [JsonProperty("resource_kind")]
        public int? ResourceKind;
    }

    public class Production
    {
        [JsonProperty("base_production_per_hour")]
        public decimal? BaseProductionPerHour;

        [JsonProperty("betterPriceQuality")]
        public int? BetterPriceQuality;

        [JsonProperty("exchange_price")]
        public decimal? ExchangePrice;

        [JsonProperty("inputs")]
        public List<ResourceInput> Inputs;

        [JsonProperty("quality")]
        public int? Quality;

        [JsonProperty("resource")]
        public int? Resource;

        [JsonProperty("speed_modifier")]
        public int? SpeedModifier;

        [JsonProperty("transport_price")]
        public decimal? TransportPrice;

        [JsonProperty("transportation")]
        public decimal? Transportation;

        [JsonProperty("wages")]
        public decimal? Wages;
    }




}
