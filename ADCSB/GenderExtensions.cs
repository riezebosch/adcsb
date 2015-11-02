using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    static class GenderExtensions
    {
        public static string Description(this Gender g)
        {
            switch (g)
            {
                case Gender.Unknown:
                    return "Unknown gender";
                case Gender.Male:
                    return "Person is a male";
                case Gender.Female:
                    return "Person is a female";
                default:
                    return "Invalid value for gender.";
            }
        }
    }
}
