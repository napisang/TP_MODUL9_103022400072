using System;
using System.IO;
using System.Text.Json;

public class CovidConfig
{
    private string CONFIG1 = "celcius";
    private int CONFIG2 = 14;
    private string CONFIG3 = "Anda tidak diperbolehkan masuk ke dalam gedung ini";
    private string CONFIG4 = "Anda dipersilahkan untuk masuk ke dalam gedung ini";

    public CovidConfig()
    {
        LoadConfig();
    }

    private void LoadConfig()
    {
        string configPath = "covid_config.json";
        if (File.Exists(configPath))
        {
            try
            {
                string jsonText = File.ReadAllText(configPath);
                using JsonDocument doc = JsonDocument.Parse(jsonText);
                JsonElement root = doc.RootElement;

                if (root.TryGetProperty("satuan_suhu", out JsonElement suhu))
                    CONFIG1 = suhu.GetString() ?? CONFIG1;

                if (root.TryGetProperty("batas_hari_deman", out JsonElement hari))
                    CONFIG2 = int.TryParse(hari.GetString(), out int val) ? val : CONFIG2;

                if (root.TryGetProperty("pesan_ditolak", out JsonElement ditolak))
                    CONFIG3 = ditolak.GetString() ?? CONFIG3;

                if (root.TryGetProperty("pesan_diterima", out JsonElement diterima))
                    CONFIG4 = diterima.GetString() ?? CONFIG4;
            }
            catch
            {
                Console.WriteLine("Gagal membaca config, menggunakan nilai default.");
            }
        }
    }

    public string GetSatuanSuhu() => CONFIG1;
    public int GetBatasHariDeman() => CONFIG2;
    public string GetPesanDitolak() => CONFIG3;
    public string GetPesanDiterima() => CONFIG4;

    public void UbahSatuan()
    {
        if (CONFIG1.ToLower() == "celcius")
        {
            CONFIG1 = "fahrenheit";
        }
        else
        {
            CONFIG1 = "celcius";
        }
    }
}