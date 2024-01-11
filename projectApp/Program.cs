using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Cache;

class Program
{
    static void Main()
    {
       
            DataProcessor dataProcessor = new DataProcessor();
            dataProcessor.LoadData("D:\\Suli\\AsztAlk\\projectApp\\projectApp\\szoveg.txt");

            
            Console.WriteLine("Legidősebb: " + dataProcessor.LegIdosebb());

            
            Console.WriteLine("Legfiatalabb: " + dataProcessor.LegFiatalabb());

      
            Console.WriteLine("Játékosok száma: " + dataProcessor.JatekosSzam());


            Console.WriteLine("Hány éves játékosra kiványcsi?");
            string keresett_kor = Console.ReadLine();
            Console.WriteLine("Vannnak idősebbbek "+keresett_kor+"-nál: " + dataProcessor.ContainsElement(x => x.Age > Convert.ToInt32(keresett_kor)));
            
            Console.WriteLine("Írja be a játékos nevét: ");
            string keresett_jatekos = Console.ReadLine();
            Console.WriteLine("A keresett jatekos: "+keresett_jatekos+" letezik e a rendszerben: "+dataProcessor.ContainsElement(x => x.Name == keresett_jatekos));

            Console.WriteLine("Átlag pontszám: " + dataProcessor.AtlagPont());

            Console.WriteLine("Legmagasabb pontszám: " + dataProcessor.LegmagasabbPont());
            
        

 
       
    }
}

class DataItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Score { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"{Id};{Name};{Age};{Score};{Date:yyyy-MM-dd}";
    }
}

class DataProcessor
{
    public List<DataItem> DataList { get; private set; }

    public void LoadData(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                DataList = File.ReadLines(filePath)
                    .Skip(1) 
                    .Select(line =>
                    {
                        string[] parts = line.Split(';');
                        return new DataItem
                        {
                            Id = int.Parse(parts[0]),
                            Name = parts[1],
                            Age = int.Parse(parts[2]),
                            Score = double.Parse(parts[3], CultureInfo.InvariantCulture), 
                            Date = DateTime.ParseExact(parts[4], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        };
                    })
                    .ToList();
            }
            else
            {
                throw new FileNotFoundException("File not found.", filePath);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error loading data: " + ex.Message);
        }
    }

    public int LegIdosebb()
    {


        return DataList.Max(x => x.Age); 
    }

    public int LegFiatalabb()
    {
       

        return DataList.Min(x => x.Age); 
    }


    public int JatekosSzam()
    {
        return DataList.Count;
    }


    public bool ContainsElement(Func<DataItem, bool> condition)
    {
        return DataList.Any(condition);
    }

    public int AtlagPont()
    {
       return (int)DataList.Average(x => x.Score);
    }

    public int LegmagasabbPont()
    {
        return (int)DataList.Max(x => x.Score);
    }


}
