using System;
using System.Collections.Generic;

namespace AkilliEv
{
    enum CihazTuru
    {
        Lamba,
        Klima,
        GuvenlikSistemi
    }

    class Cihaz
    {
        public int Kimlik { get; }
        public string Ad { get; }
        public CihazTuru Tur { get; }
        public bool AcikMi { get; private set; }

        public Cihaz(int kimlik, string ad, CihazTuru tur)
        {
            Kimlik = kimlik;
            Ad = ad;
            Tur = tur;
            AcikMi = false;
        }

        public void Ac()
        {
            AcikMi = true;
        }

        public void Kapat()
        {
            AcikMi = false;
        }

        public void BilgileriGoster()
        {
            string durum = AcikMi ? "Açık" : "Kapalı";
            Console.WriteLine(string.Format("{0,-5} {1,-22} {2,-16} {3}", Kimlik, Ad, Tur, durum));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Cihaz> cihazlar = new List<Cihaz>();
            cihazlar.Add(new Cihaz(1, "Salon Lambası", CihazTuru.Lamba));
            cihazlar.Add(new Cihaz(2, "Yatak Odası Lambası", CihazTuru.Lamba));
            cihazlar.Add(new Cihaz(3, "Salon Kliması", CihazTuru.Klima));
            cihazlar.Add(new Cihaz(4, "Ev Alarmı", CihazTuru.GuvenlikSistemi));

            bool devam = true;
            while (devam)
            {
                Console.WriteLine();
                Console.WriteLine("===== AKILLI EV MENÜSÜ =====");
                Console.WriteLine("1 - Cihazları listele");
                Console.WriteLine("2 - Yeni cihaz ekle");
                Console.WriteLine("3 - Cihaz aç");
                Console.WriteLine("4 - Cihaz kapat");
                Console.WriteLine("0 - Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = (Console.ReadLine() ?? "").Trim();

                switch (secim)
                {
                    case "1":
                        CihazlariListele(cihazlar);
                        break;
                    case "2":
                        CihazEkle(cihazlar);
                        break;
                    case "3":
                        CihazDurumDegistir(cihazlar, true);
                        break;
                    case "4":
                        CihazDurumDegistir(cihazlar, false);
                        break;
                    case "0":
                        devam = false;
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("Hata: Geçersiz seçim.");
                        break;
                }
            }
        }

        static void CihazlariListele(List<Cihaz> cihazlar)
        {
            if (cihazlar.Count == 0)
            {
                Console.WriteLine("Kayıtlı cihaz yok.");
                return;
            }

            Console.WriteLine(string.Format("{0,-5} {1,-22} {2,-16} {3}", "ID", "Ad", "Tür", "Durum"));
            foreach (Cihaz cihaz in cihazlar)
            {
                cihaz.BilgileriGoster();
            }
        }

        static Cihaz CihazBul(List<Cihaz> cihazlar, int kimlik)
        {
            foreach (Cihaz cihaz in cihazlar)
            {
                if (cihaz.Kimlik == kimlik)
                {
                    return cihaz;
                }
            }
            return null;
        }

        static void CihazEkle(List<Cihaz> cihazlar)
        {
            Console.Write("Kimlik: ");
            int kimlik;
            if (!int.TryParse(Console.ReadLine(), out kimlik))
            {
                Console.WriteLine("Hata: Kimlik bir sayı olmalıdır.");
                return;
            }

            if (CihazBul(cihazlar, kimlik) != null)
            {
                Console.WriteLine("Hata: " + kimlik + " kimlikli bir cihaz zaten var.");
                return;
            }

            Console.Write("Ad: ");
            string ad = (Console.ReadLine() ?? "").Trim();
            if (ad == "")
            {
                Console.WriteLine("Hata: Cihaz adı boş olamaz.");
                return;
            }

            Console.WriteLine("Tür: 1 - Lamba, 2 - Klima, 3 - GuvenlikSistemi");
            Console.Write("Tür seçiminiz: ");
            int turNo;
            if (!int.TryParse(Console.ReadLine(), out turNo) || turNo < 1 || turNo > 3)
            {
                Console.WriteLine("Hata: Geçersiz cihaz türü.");
                return;
            }

            CihazTuru tur = (CihazTuru)(turNo - 1);
            cihazlar.Add(new Cihaz(kimlik, ad, tur));
            Console.WriteLine(ad + " eklendi (başlangıç durumu: Kapalı).");
        }

        static void CihazDurumDegistir(List<Cihaz> cihazlar, bool ac)
        {
            Console.Write("Cihaz kimliği: ");
            int kimlik;
            if (!int.TryParse(Console.ReadLine(), out kimlik))
            {
                Console.WriteLine("Hata: Kimlik bir sayı olmalıdır.");
                return;
            }

            Cihaz cihaz = CihazBul(cihazlar, kimlik);
            if (cihaz == null)
            {
                Console.WriteLine("Hata: " + kimlik + " kimlikli cihaz bulunamadı.");
                return;
            }

            if (ac)
            {
                cihaz.Ac();
                Console.WriteLine(cihaz.Ad + " açıldı.");
            }
            else
            {
                cihaz.Kapat();
                Console.WriteLine(cihaz.Ad + " kapatıldı.");
            }
        }
    }
}