using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApotekConsoleApp.Models
{
    public class Obat : DomainObject
    {
        public string Nama { get; set; }
        public int Stok { get; set; }
        public int Harga { get; set; }

    }
}
