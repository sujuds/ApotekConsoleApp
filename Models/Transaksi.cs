using System;
using System.Collections.Generic;
using System.Text;

namespace ApotekConsoleApp.Models
{
    public class Transaksi : DomainObject
    {
        public int Total { get; set; }
        public IEnumerable<TransaksiDetail> TransaksiDetails { get; set; }
    }
}
