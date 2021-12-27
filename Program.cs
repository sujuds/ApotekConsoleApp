using ApotekConsoleApp.DbContexts;
using ApotekConsoleApp.Models;
using ApotekConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            int menu = 0;
            Program program = new Program();

            do
            {
                Console.WriteLine("APOTEK BAHAGIA SELALU");
                Console.WriteLine("[1] Transaksi");
                Console.WriteLine("[2] Data Obat");
                Console.WriteLine("[3] Riwayat Transaksi");
                Console.WriteLine("[0] Exit");

                Console.Write("Pilih menu: ");

                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Input anda salah!");
                }

                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        program.Transaksi();
                        break;
                    case 2 :
                        Console.Clear();
                        program.DataObat();
                        break;
                    case 3 :
                        Console.Clear();
                        program.RiwayatTransaksi();
                        break;
                    case 0:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Tidak ada pilihan menu! \n");
                        break;
                }

            } while (menu != 0);

            Console.WriteLine("Terima kasih! \n");
        }


        private void Transaksi()
        {
            int count = 1;
            string flag = "";
            string status = "";
            List<TransaksiDetail> detailList = new List<TransaksiDetail>();
            GenericDataService<Obat> obatService = new GenericDataService<Obat>(new ApotekDbContextFactory());
            GenericDataService<Transaksi> transaksiService = new GenericDataService<Transaksi>(new ApotekDbContextFactory());


            Console.WriteLine("\nTRANSAKSI");

            do
            {
                string kode;
                int jumlah;

                Loop :

                Console.WriteLine("Item "+count);
                Console.Write("Kode Obat : ");
                kode = Console.ReadLine();

                var obatSelected = obatService.GetByKode(kode).Result;          


                if (obatSelected != null)
                {
                    Console.WriteLine($" => { obatSelected.Nama }, stok: { obatSelected.Stok }, harga: Rp. { obatSelected.Harga }");

                    Console.Write("Jumlah : ");
                    jumlah = Convert.ToInt32(Console.ReadLine());

                    
                    detailList.Add(new TransaksiDetail()
                    {
                        Jumlah = jumlah,
                        Obat = obatSelected,
                        Transaksi = null
                    });


                    if (jumlah > obatSelected.Stok)
                    {
                        Console.WriteLine("\nJumlah melebihi stok");
                        goto Loop;
                    }

                }
                else
                {
                    Console.WriteLine("\nKode obat " + kode + " tidak ditemukan");
                    goto Loop;
                }

                Console.Write("Ada lagi (y/n) : ");
                flag = Console.ReadLine();

                count++;
            } while (flag != "n");


            Console.Clear();
            int total = 0;
            foreach (var item in detailList)
            {
                total += (item.Obat.Harga * item.Jumlah);
                Console.WriteLine($"{item.Obat.Nama} => Rp. {item.Obat.Harga}  x  {item.Jumlah} \t= Rp.  {(item.Obat.Harga * item.Jumlah)}");
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Total = Rp. "+total);
            Console.WriteLine("--------------------------------------------------");
            Console.Write("Simpan transaksi (y/n) : ");
            status  = Console.ReadLine();

            if (status == "y")
            {
                DateTime dateTime = DateTime.UtcNow.Date;
                Random rnd = new Random();


                var _transaksi = new Transaksi()
                {
                    Kode = "T" + dateTime.ToString("ddMMyyyy") + rnd.Next(10),
                    Total = total
                };


                foreach (var item in detailList)
                {
                    item.Transaksi = _transaksi;
                    item.ObatId = item.Obat.Id;


                    var update = obatService.Update(item.Obat.Id, new Obat
                    {
                        Kode = item.Obat.Kode,
                        Nama = item.Obat.Nama,
                        Stok = item.Obat.Stok - item.Jumlah,
                        Harga = item.Obat.Harga
                    }).Result;


                    item.Obat = null;

                }

                _transaksi.TransaksiDetails = detailList;

                if (transaksiService.Create(new Transaksi
                    {
                        Kode = _transaksi.Kode,
                        Total = _transaksi.Total,
                        TransaksiDetails = _transaksi.TransaksiDetails
                    }).Result != null)
                {

                    Console.Clear();
                    Console.WriteLine("Transaksi berhasil disimpan!\n");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Transaksi gagal!\n");
                }

            }

        }

        private void DataObat()
        {
            GenericDataService<Obat> obatService = new GenericDataService<Obat>(new ApotekDbContextFactory());
            int menu = 0;
            string kode;
            string nama;
            int stok;
            int harga;

            do
            {
                Console.WriteLine("APOTEK BAHAGIA SELALU / DATA OBAT");
                Console.WriteLine("[1] Daftar Obat");
                Console.WriteLine("[2] Tambah Obat");
                Console.WriteLine("[3] Edit Obat");
                Console.WriteLine("[4] Hapus Obat");
                Console.WriteLine("[0] Kembali");

                Console.Write("Pilih menu: ");

                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Input anda salah!");
                }

                switch (menu)
                {
                    // getall
                    case 1:
                        Console.Clear();
                        Console.WriteLine("APOTEK BAHAGIA SELALU / DATA OBAT / DAFTAR OBAT");
                        foreach (var obat in obatService.GetAll().Result)
                        {
                            Console.WriteLine($"=> [kode : { obat.Kode}] { obat.Nama }, stok: { obat.Stok }, harga: Rp. { obat.Harga }");
                        }
                        Console.WriteLine("");
                        break;

                    // create
                    case 2:
                        Console.Clear();
                        Console.WriteLine("APOTEK BAHAGIA SELALU / DATA OBAT / TAMBAH OBAT");
                        Console.Write("Kode : ");
                        kode = Console.ReadLine();
                        Console.Write("Nama : ");
                        nama = Console.ReadLine();
                        Console.Write("Stok : ");
                        stok = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Harga : ");
                        harga = Convert.ToInt32(Console.ReadLine());

                        obatService.Create(new Obat
                        {
                            Kode = kode,
                            Nama = nama,
                            Stok = stok,
                            Harga = harga
                        }).Wait();

                        Console.Clear();
                        Console.WriteLine("Data berhasil ditambahkan!\n");
                        break;

                    //update
                    case 3:
                        Console.WriteLine("\nAPOTEK BAHAGIA SELALU / DATA OBAT / EDIT OBAT");
                        Console.Write("Kode : ");
                        kode = Console.ReadLine();

                        Obat obatSelected = obatService.GetByKode(kode).Result;

                        if ( obatSelected == null) 
                        { 
                            Console.WriteLine("Kode tidak ditemukan!\n");
                        }
                        else
                        {
                            Console.Write("Nama : ");
                            nama = Console.ReadLine();
                            Console.Write("Stok : ");
                            stok = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Harga : ");
                            harga = Convert.ToInt32(Console.ReadLine());

                            var update = obatService.Update(obatSelected.Id, new Obat {
                                Kode = kode, Nama = nama, Stok = stok, Harga = harga 
                            }).Result;
                            if (update != null)
                            {
                                Console.Clear();
                                Console.WriteLine("Data berhasil diubah!\n");
                            }
                        }

                        break;

                    //delete
                    case 4:
                        Console.WriteLine("\nAPOTEK BAHAGIA SELALU / DATA OBAT / HAPUS OBAT");
                        Console.Write("Kode : ");
                        kode = Console.ReadLine();

                        Console.Clear();
                        if (obatService.Delete(kode).Result)
                        {
                            Console.WriteLine("Data berhasil dihapus!\n");
                        }
                        else
                        {
                            Console.WriteLine("Kode tidak ditemukan!\n");
                        }
                        break;
                    case 0:
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Tidak ada pilihan menu! \n");
                        break;
                }
            } while (menu != 0);

            Console.Clear();
        }
        
        
        private void RiwayatTransaksi()
        {
            GenericDataService<Transaksi> transaksiService = new GenericDataService<Transaksi>(new ApotekDbContextFactory());
            int menu = 0;

            do
            {
                Console.WriteLine("APOTEK BAHAGIA SELALU / RIWAYAT TRANSAKSI");

                using (var _context = new ApotekDbContextFactory().CreateDbContext())
                {
                    var transaksiResult = _context.Transaksis;
                    foreach (Transaksi t in transaksiResult)
                    {
                        Console.WriteLine($"=> Transaksi { t.Kode} :");

                        _context.Entry(t).Collection(p => p.TransaksiDetails).Load();
                        foreach (TransaksiDetail td in t.TransaksiDetails)
                        {
                            _context.Obats.Where(p => p.Id == td.ObatId).Load();

                            Console.WriteLine($"\t[{ td.Obat.Kode}] { td.Obat.Nama } \tRp {td.Obat.Harga} x {td.Jumlah} = {td.Obat.Harga*td.Jumlah}");
                        }

                        Console.WriteLine($"   Total : Rp. { t.Total}");
                    }
                }

                Console.WriteLine("\n[0] Kembali");
                Console.Write("Pilih menu: ");

                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Input anda salah!");
                }

            } while (menu != 0);

            Console.Clear();
        }



    }
}
