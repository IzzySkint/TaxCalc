using System;
using TaxCalc.Core.Enums;

namespace TaxCalc.Core.Models
{
	public class TaxTable
	{
		public TaxTable()
		{
			Entries = new List<Entry>();
		}

		public List<Entry> Entries { get; private set; }
		public class Entry
		{
			public decimal From { get; set; }
			public decimal To { get; set; }
			public decimal Rate { get; set; }
			public decimal Value { get; set; }
			public TaxCalculationTypes CalculationType { get; set; }
		}
	}
}

