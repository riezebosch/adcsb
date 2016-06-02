using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADCSB
{
    partial class Persoon
    {
        private DateTime _geboorteDatum;

        private event EventHandler<DateTime> OnGeboorteDatumChangedMetEenEvent;

        public DateTime GeboorteDatum
        {
            get { return _geboorteDatum; }
            set
            {
                OnGeboorteDatumChangedMetPartialMethod(value);

                if (OnGeboorteDatumChangedMetEenEvent != null)
                {
                    OnGeboorteDatumChangedMetEenEvent(this, value);
                }

                _geboorteDatum = value;
            }
        }

        public string Naam { get; internal set; }

        partial void OnGeboorteDatumChangedMetPartialMethod(DateTime value);
    
    }
}
