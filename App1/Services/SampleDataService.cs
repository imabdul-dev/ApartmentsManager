using App1.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace App1.Services
{
    // This class holds sample data used by some generated pages to show how they can be used.
    public static class SampleDataService
    {
        private static IEnumerable<SampleApartment> _allApartments;

        private static IEnumerable<SampleApartment> Apartments()
        {
            // The following is order summary data
            return AllApartments();
        }

        private static IEnumerable<SampleApartment> AllApartments()
        {
            var list = new List<SampleApartment>();
            using (var reader = new StreamReader("Assets/apartmentMockData.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var  records = csv.GetRecords<SampleApartment>();
                list.AddRange(records);
            }

            return list;
        }

        public static async Task<IEnumerable<SampleApartment>> GetApartmentsDataAsync()
        {
            if (_allApartments == null)
            {
                _allApartments = new List<SampleApartment>();
                _allApartments = AllApartments();
            }

            await Task.CompletedTask;
            return _allApartments;
        }
    }
}
