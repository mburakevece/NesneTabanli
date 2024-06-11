using System;
using System.Collections.Generic;
using System.IO;

namespace EmlakciUygulamasi
{
    class Program
    {
        static List<KiralikEv> kiralikEvler = new List<KiralikEv>();
        static List<SatilikEv> satilikEvler = new List<SatilikEv>();

        static void Main(string[] args)
        {
            while (true)
            {
                MenuGetir();
            }
        }

        public static void MenuGetir()
        {
            Console.WriteLine("\n1. Yeni Ev Girişi\n2. Kayıtlı Evleri Göster\n3. Çıkış\nSeçiminiz Nedir?:");
            if (byte.TryParse(Console.ReadLine(), out byte menuSecim))
            {
                switch (menuSecim)
                {
                    case 1:
                        YeniEvGirisi();
                        break;
                    case 2:
                        KayitliEvBilgileriGoster();
                        break;
                    case 3:
                        Console.WriteLine("Çıkılıyor...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Yanlış seçim, lütfen tekrar deneyin.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Yanlış Girdi , Yeniden denemenizi rica ediyorum.");
            }
        }

        public static void YeniEvGirisi()
        {
            Console.WriteLine("\n1. Kiralık Ev\n2. Satılık Ev\nSeçiminiz Nedir?:");
            if (byte.TryParse(Console.ReadLine(), out byte evSecim))
            {
                switch (evSecim)
                {
                    case 1:
                        EvBilgisiGiris<KiralikEv>();
                        break;
                    case 2:
                        EvBilgisiGiris<SatilikEv>();
                        break;
                    default:
                        Console.WriteLine("Yanlış Girdi , Yeniden denemenizi rica ediyorum.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Yanlış Girdi, Yeniden denemenizi rica ediyorum.");
            }
        }

        public static void EvBilgisiGiris<T>() where T : Ev, new()
        {
            var ev = new T();

            Console.WriteLine("\nSemt:\n");
            ev.Semt = Console.ReadLine();

            Console.WriteLine("\nKat No:\n");
            ev.KatNo = int.Parse(Console.ReadLine());

            Console.WriteLine("\nOda Sayısı:\n");
            ev.OdaSayisi = int.Parse(Console.ReadLine());

            Console.WriteLine("\nAlanı :\n");
            ev.Alan = double.Parse(Console.ReadLine());

            if (ev is KiralikEv kiralikEv)
            {
                Console.WriteLine("\nKira Bedeli :\n");
                kiralikEv.KiraFiyati = double.Parse(Console.ReadLine());

                Console.WriteLine("\nDepozito :\n");
                kiralikEv.Depozito = double.Parse(Console.ReadLine());

                kiralikEvler.Add(kiralikEv);
                Yazici(kiralikEv.ToString(), "kiralikevler.txt");
            }
            else if (ev is SatilikEv satilikEv)
            {
                Console.WriteLine("\nSatış Fiyatı :\n");
                satilikEv.SatisFiyati = double.Parse(Console.ReadLine());

                satilikEvler.Add(satilikEv);
                Yazici(satilikEv.ToString(), "satilikevler.txt");
            }

            Console.WriteLine("\nGirdi Başarılı , Devam etmek istiyor musunuz? (Evet/Hayır)");
            string cevap = Console.ReadLine();
            if (cevap.ToUpper() == "Evet")
            {
                YeniEvGirisi();
            }
            else
            {
                MenuGetir();
            }
        }

        public static void KayitliEvBilgileriGoster()
        {
            Console.WriteLine("Kiralık Evler:");
            Okuyucu("kiralikevler.txt");

            Console.WriteLine("\nSatılık Evler:");
            Okuyucu("satilikevler.txt");
        }

        public static void Okuyucu(string dosyaYolu)
        {
            if (File.Exists(dosyaYolu))
            {
                string[] satirlar = File.ReadAllLines(dosyaYolu);
                foreach (string satir in satirlar)
                {
                    Console.WriteLine(satir);
                }
            }
            else
            {
                Console.WriteLine("Kayıtlı ev bulunamadı.");
            }
        }

        public static void Yazici(string veri, string dosyaYolu)
        {
            using (StreamWriter sw = new StreamWriter(dosyaYolu, true))
            {
                sw.WriteLine(veri);
            }
        }
    }

    public class Ev
    {
        public string Semt { get; set; }
        public int KatNo { get; set; }
        public int OdaSayisi { get; set; }
        public double Alan { get; set; }

        public override string ToString()
        {
            return $"Semt: {Semt}, Kat No: {KatNo}, Oda Sayısı: {OdaSayisi}, Alan: {Alan} m²";
        }
    }

    public class KiralikEv : Ev
    {
        public double KiraFiyati { get; set; }
        public double Depozito { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", Kira Fiyatı: {KiraFiyati} TL, Depozito: {Depozito} TL";
        }
    }

    public class SatilikEv : Ev
    {
        public double SatisFiyati { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", Satış Fiyatı: {SatisFiyati} TL";
        }
    }
}
