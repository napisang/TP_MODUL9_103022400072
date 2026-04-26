using System;

class Program
{
    static void Main(string[] args)
    {
        CovidConfig config = new CovidConfig();

        Console.WriteLine(">>> Memanggil method UbahSatuan()...");
        config.UbahSatuan();

        config.UbahSatuan();

        Console.Write($"Berapa suhu badan anda saat ini? Dalam nilai <{config.GetSatuanSuhu().ToUpper()}>\n> ");
        string inputSuhu = Console.ReadLine();

        Console.Write($"\nBerapa hari yang lalu (perkiraan) anda terakhir memiliki gejala deman?\n> ");
        string inputHari = Console.ReadLine();

        if (!double.TryParse(inputSuhu, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out double suhu))
        {
            Console.WriteLine("[ERROR] Input suhu tidak valid.");
            return;
        }

        if (!int.TryParse(inputHari, out int hariDeman))
        {
            Console.WriteLine("[ERROR] Input hari tidak valid.");
            return;
        }

        bool kondisiSuhu = false;
        bool kondisiHari = hariDeman < config.GetBatasHariDeman();

        string satuan = config.GetSatuanSuhu().ToLower();

        if (satuan == "celcius")
        {
            kondisiSuhu = (suhu >= 36.5 && suhu <= 37.5);
        }
        else if (satuan == "fahrenheit")
        {
            kondisiSuhu = (suhu >= 97.7 && suhu <= 99.5);
        }

        bool kondisiTerpenuhi = kondisiSuhu && kondisiHari;

        if (kondisiTerpenuhi)
        {
            Console.WriteLine(config.GetPesanDiterima());
        }
        else
        {
            Console.WriteLine(config.GetPesanDitolak());
        }
    }
}