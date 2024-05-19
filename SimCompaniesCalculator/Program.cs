using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace SimCompaniesCalculator
{
    internal class Program
    {
        private static List<Facility> _productions = new List<Facility>();

        static void Main(string[] args)
        {
            CalculateAllProfits();

            Console.Write("\rObtaining Market Data - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("COMPLETE");
            Console.ResetColor();
            Console.WriteLine(string.Empty);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("---");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. List all potential market values");
                Console.WriteLine("2. List top 10 of most profitable per hour");
                Console.WriteLine("3. List top 10 of least profitable per hour");
                Console.WriteLine("4. Search by FacilityName");
                Console.WriteLine("5. Export as CSV");
                Console.WriteLine("6. Refresh data");
                Console.WriteLine("7. Credits");
                Console.WriteLine();
                Console.Write("Enter your choice: ");
                Console.WriteLine();

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ListAllPotentialMarketValues();
                        break;
                    case "2":
                        ListTop10MostProfitablePerHour();
                        break;
                    case "3":
                        ListTop10LeastProfitablePerHour();
                        break;
                    case "4":
                        Console.WriteLine();
                        Console.Write("Enter the name of the building: ");
                        var buildingName = Console.ReadLine();
                        SearchByName(buildingName);
                        break;
                    case "5":
                        Console.WriteLine("Exporting as CSV");
                        ExportAsCsv();
                        break;
                    case "6":
                        Console.WriteLine("Refreshing data");
                        _productions.Clear();
                        CalculateAllProfits();
                        Console.Write("\rObtaining Market Data - ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("COMPLETE");
                        Console.ResetColor();
                        Console.WriteLine(string.Empty);
                        break;
                    case "7":
                        Console.WriteLine();
                        Console.WriteLine("=== Credits ===");
                        Console.WriteLine("Developed by: Lt.Kraken");
                        Console.WriteLine("Website: https://krakensoftware.eu/");
                        Console.WriteLine("Data provided by: https://simcotools.app/");
                        Console.WriteLine("Code/Support: https://github.com/lt-kraken/kraken-simcompanies-calculator");
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }

        private static void SearchByName(string? buildingName)
        {
            var building = _productions.FirstOrDefault(x => x.FacilityName.Equals(buildingName, StringComparison.InvariantCultureIgnoreCase));
            if (building != null)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine($"Building: {building.FacilityName}");
                Console.WriteLine("-------------------------");

                foreach (var production in building.Productions)
                {
                    Console.Write($"{production.ResourceName} (@ {production.ExchangePrice:C}) = profit/h:");
                    Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;

                    Console.Write($"{production.ProfitPerLevelPerHour:C3}");
                    Console.ResetColor();
                    Console.Write($" | profit/day:");

                    Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.Write($"{production.ProfitPerLevelPerHour * 24:C3}");
                    Console.ResetColor();
                    Console.WriteLine(string.Empty);
                }

                Console.WriteLine(string.Empty);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - Building not found");
                Console.ResetColor();
            }
        }

        private static void ListTop10LeastProfitablePerHour()
        {
            // Order by profit per level per hour
            var ordered = _productions.SelectMany(x => x.Productions).OrderBy(x => x.ProfitPerLevelPerHour).Take(10).ToList();

            // Print the top 10 least profitable per hour
            Console.WriteLine("Top 10 least profitable per hour");
            Console.WriteLine("-------------------------");
            foreach (var production in ordered)
            {
                Console.Write($"{production.ResourceName} (@ {production.ExchangePrice:C}) = profit/h:");
                Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;

                Console.Write($"{production.ProfitPerLevelPerHour:C3}");
                Console.ResetColor();
                Console.Write($" | profit/day:");

                Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write($"{production.ProfitPerLevelPerHour * 24:C3}");
                Console.ResetColor();
                Console.WriteLine(string.Empty);
            }
        }

        private static void ListTop10MostProfitablePerHour()
        {
            // Order by profit per level per hour
            var ordered = _productions.SelectMany(x => x.Productions).OrderByDescending(x => x.ProfitPerLevelPerHour).Take(10).ToList();

            // Print the top 10 most profitable per hour
            Console.WriteLine("Top 10 most profitable per hour");
            Console.WriteLine("-------------------------");
            foreach (var production in ordered)
            {
                Console.Write($"{production.ResourceName} (@ {production.ExchangePrice:C}) = profit/h:");
                Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;

                Console.Write($"{production.ProfitPerLevelPerHour:C3}");
                Console.ResetColor();
                Console.Write($" | profit/day:");

                Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write($"{production.ProfitPerLevelPerHour * 24:C3}");
                Console.ResetColor();
                Console.WriteLine(string.Empty);
            }
        }

        private static void ListAllPotentialMarketValues()
        {
            // Loop through production buildings and write to console
            foreach (var building in _productions.OrderBy(x => x.FacilityName))
            {
                // Print the building name
                Console.WriteLine("-------------------------");
                Console.WriteLine($"Building: {building.FacilityName}");
                Console.WriteLine("-------------------------");

                Console.WriteLine($"Building: {building.FacilityName}");
                foreach (var production in building.Productions)
                {
                    Console.Write($"{production.ResourceName} (@ {production.ExchangePrice:C}) = profit/h:");
                    Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;

                    Console.Write($"{production.ProfitPerLevelPerHour:C3}");
                    Console.ResetColor();
                    Console.Write($" | profit/day:");

                    Console.ForegroundColor = production.ProfitPerLevelPerHour < 0 ? ConsoleColor.Red : ConsoleColor.Green;
                    Console.Write($"{production.ProfitPerLevelPerHour * 24:C3}");
                    Console.ResetColor();
                    Console.WriteLine(string.Empty);
                }

                Console.WriteLine(string.Empty);
            }
        }

        public static void CalculateAllProfits()
        {
            // Read the file en.json
            var json = File.ReadAllText("en.json");
            var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            // Get production buildings
            var productionBuildings = GetProductionBuildings();

            // Loop through production buildings
            foreach (var building in productionBuildings)
            {
                Console.Write("\rObtaining Market Data - {0:P}", (double)productionBuildings.IndexOf(building) / (double)productionBuildings.Count);

                // Match the building name with the value in jsonObject, leading with the prefix "bd-"
                var buildingName = jsonObject[$"bd-{building}"];

                // Save the production building
                var facility = new Facility()
                {
                    FacilityCode = building,
                    FacilityName = buildingName,
                    Productions = new List<ProductionOutcome>()
                };

                // Calculate profit per hour
                var production = GetProductionEstimates(building);

                // Loop through production estimates
                foreach (var productionEstimate in production)
                {
                    // Calculate profit per hour
                    var productionOutcome = CalculateProfitPerHour(building, productionEstimate);

                    // Add production outcome to facility
                    productionOutcome.ResourceName = jsonObject[$"re-{productionEstimate.Resource}"];
                    facility.Productions.Add(productionOutcome);
                }

                // Add facility to productions
                _productions.Add(facility);
            }
        }

        public static ProductionOutcome CalculateProfitPerHour(string building, Production production)
        {
            var inputTransportPrice = production.TransportPrice; // TransportPrice
            var inputIncludeTransportCost = true; // IncludeTransportCost
            //var inputContractDiscountPercent = 3m; // ContractDiscountPercent
            var inputAdminOverhead = 0m; // AdministrationOverhead
            var inputProductionBonus = 0m; // ProductionBonus
            var inputAbundance = building switch // Abundance
            {
                "M" => 95m,
                "O" => 95m,
                "Q" => 95m,
                _ => 100m
            };
            var inputHasRobotsInstalled = false; // HasRobotsInstalled
            var inputBuildingLevel = 1m; // BuildingLevels
            //var inputCalculatePerDay = false; // CalculatePerDay

            var exchangePrice = production.ExchangePrice ?? 0m;
            var materialCost = production.Inputs.Sum(i => (i.Price ?? 0m) * (i.Amount ?? 0m));


            var transportation = inputIncludeTransportCost ? production.Transportation * inputTransportPrice : 0;
            var fee = exchangePrice * .03m;
            //var discountPercent = production.ExchangePrice.HasValue ? h * inputContractDiscountPercent / 100m : 0;
            var productionUnitsPerHour = production.BaseProductionPerHour / (1 - inputProductionBonus / 100m) * (inputAbundance / 100m) * (1 + production.SpeedModifier / 100m);
            var labor = production.Wages * (inputHasRobotsInstalled ? .97m : 1) / productionUnitsPerHour;
            var adminOverhead = labor * inputAdminOverhead / 100m;
            var productionCost = materialCost + labor + adminOverhead;
            var profitPerUnit = exchangePrice - fee - productionCost - transportation;
            var profitPerHour = profitPerUnit * productionUnitsPerHour;
            //var x = h - discountPercent - k - transportation / 2;
            //var C = x * productionPerLevelPerHour;

            var result = new ProductionOutcome()
            {
                ExchangePrice = exchangePrice,
                MaterialCost = materialCost,
                LaborCost = labor,
                AdministrationOverhead = adminOverhead,
                Fee = fee,
                TransportCost = transportation,
                UnitsPerHour = productionUnitsPerHour,
                ProfitPerHour = profitPerHour,
                ProfitPerLevelPerHour = profitPerHour * inputBuildingLevel
            };

            return result;
        }

        public static List<string> GetProductionBuildings(int realm = 0)
        {
            // Create HTTP Client to obtain information from https://simcotools.app/api/v2/buildings/?category=production&realm=0
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://simcotools.app/api/v2/buildings/?category=production&realm={realm}");
            var response = client.GetAsync(client.BaseAddress).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<string>>(responseString);
        }

        public static List<Production> GetProductionEstimates(string building, int realm = 0, int quality = 0)
        {
            // Create HTTP Client to obtain information from https://simcotools.app/api/v3/profitcalculator/?realm=0&building=M&quality=0
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"https://simcotools.app/api/v3/profitcalculator/?realm={realm}&building={building}&quality={quality}");
            var response = client.GetAsync(client.BaseAddress).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<Production>>(responseString);
        }

        private static void ExportAsCsv()
        {
            var csv = new StringBuilder();

            // Get the properties of the Facility, excluding the Productions property
            var properties = typeof(Facility).GetProperties().Where(p => p.Name != "Productions").ToList();
            properties.AddRange(typeof(ProductionOutcome).GetProperties());

            // Create the header row
            string header = string.Join(",", properties.Select(p => p.Name));
            csv.AppendLine(header);

            // Create the data rows
            foreach (var production in _productions.OrderBy(x => x.FacilityName))
            {
                // Add a row for each production outcome
                foreach (var productionOutcome in production.Productions)
                {
                    string row = $"{production.FacilityCode},{production.FacilityName},{string.Join(",", properties.Skip(2).Select(p => p.GetValue(productionOutcome, null)))}";
                    csv.AppendLine(row);
                }
            }

            Console.Write("Exporting 'Productions.csv'");

            // Write the csv data to a file
            File.WriteAllText("Productions.csv", csv.ToString());

            Console.Write("\rExporting 'Productions.csv' - ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("SUCCESS");
            Console.ResetColor();
            Console.WriteLine(string.Empty);
        }
    }

    internal class Facility
    {
        public string FacilityCode { get; set; }
        public string FacilityName { get; set; }

        public List<ProductionOutcome> Productions { get; set; }
    }

    internal class ProductionOutcome
    {
        public string ResourceName { get; set; }

        public decimal? ExchangePrice { get; set; }

        public decimal? MaterialCost { get; set; }

        public decimal? LaborCost { get; set; }

        public decimal? AdministrationOverhead { get; set; }

        public decimal? Fee { get; set; }

        public decimal? TransportCost { get; set; }

        public decimal? ProductionCost => MaterialCost + LaborCost;

        public decimal? UnitsPerHour { get; set; }

        public decimal? ProfitPerUnit => ExchangePrice - Fee - ProductionCost - TransportCost;

        public decimal? ProfitPerHour { get; set; }

        public decimal? ProfitPerLevelPerHour { get; set; }

        public decimal? ProfitPerDay => ProfitPerHour * 24;

    }
}
