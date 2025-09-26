using System.IO;
using System;
using System.Globalization;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Text.Json;
using Newtonsoft.Json;

//oppgave1
public class DataTable
{

    public List<DataRow> DataRows { get; private set; }
    private List<string[]> rows;
    private List<string> columns;

    public List<string[]> Rows
    {
        get { return rows; }
        private set { rows = value; }
    }

    public List<string> Columns
    {
        get { return columns; }
        private set { columns = value; }
    }

    public DataTable()
    {

        rows = new List<string[]>();
        columns = new List<string>();
        DataRows = new List<DataRow>();

    }

    public void LoadDataFromFile(string filePath)
    {
        //leser alle linjene og lagrer de i lines
        string[] lines = File.ReadAllLines(filePath);

        // loop for å lagre data i lines

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            // Ekstremt mange tomme verdier så fjerner de
            string[] value = line.Split(",", StringSplitOptions.RemoveEmptyEntries);

            if (i == 0) //legger første linje i columns
            {
                columns.AddRange(value);
            }
            else
            {
                rows.Add(value);
            }

        }

    }


    //oppgave2 metode:

    public void ConvertRowsToDataRows()
    {
        DataRows = new List<DataRow>();

        foreach (var row in Rows)
        {
            DateTime dateTime = DateTime.Parse(row[0]);

            double[] values = new double[row.Length - 1];
            for (int i = 1; i < row.Length; i++)
            {
                values[i - 1] = double.Parse(row[i], CultureInfo.InvariantCulture);
            }

            DataRows.Add(new DataRow(dateTime, values));
        }

    }


    //oppgave 3
    public double[] GetColumn(string name)
    {
        //finner riktig index
        int index = Columns.IndexOf(name);
        double[] result = new double[DataRows.Count];


        for (int i = 0; i < DataRows.Count; i++)
        {
            result[i] = DataRows[i].Values[index - 1];
        }


        return result;
    }

    // oppgave4: sorterings algo
    public void BubbleSort()
    {

        for (int i = 0; i < DataRows.Count - 1; i++)
        {
            for (int j = 0; j < DataRows.Count - 1; j++)
            {
                if (DataRows[j].TimeStamp > DataRows[j + 1].TimeStamp)
                {
                    var temp = DataRows[j];
                    DataRows[j] = DataRows[i];
                    DataRows[i] = temp;
                }
            }
        }


    }


}

//oppgave2
public class DataRow
{

    public DateTime TimeStamp { get; set; }
    public double[] Values { get; set; }
    public DataRow(DateTime timeStamp, double[] values)
    {
        TimeStamp = timeStamp;
        Values = values;
    }

}

//oppgave 5
public class JsonHandler
{
    public Dictionary<string, double> LoadJsonFromFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);

        var dict = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonString);
        if (dict == null)
        {
            throw new Exception("Kunne ikke lese JSON til dictionary");

        }

        return dict;
    }
}



class Program
{
    static void Main(string[] args)
    {
        DataTable dt = new DataTable();
        dt.LoadDataFromFile("dsDataTrain.csv");


        //tester oppgave 1

        Console.WriteLine("Kolonner:");
        Console.WriteLine(string.Join("|", dt.Columns));


        Console.WriteLine("\nFørste 5 rader:");
        for (int i = 0; i < 5 && i < dt.Rows.Count; i++)
        {
            Console.WriteLine(string.Join("|", dt.Rows[i]));
        }


        //tester oppgave 2

        dt.ConvertRowsToDataRows();

        for (int i = 0; i < 5; i++)
        {
            var dr = dt.DataRows[i];
            System.Console.WriteLine($"Timestamp: {dr.TimeStamp}, First value {dr.Values[0]}");
        }

        //tester oppgave 4

        System.Console.WriteLine("\nFør sortering:");
        foreach (var dr in dt.DataRows.Take(8))
        {
            System.Console.WriteLine(dr.TimeStamp);
        }

        dt.BubbleSort();
        System.Console.WriteLine("\nEtter sortering");

        foreach (var dr in dt.DataRows.Take(8))
        {
            System.Console.WriteLine(dr.TimeStamp);
        }

        //ser ut som alt allerede er sortert
        //tester med testdata for å sjekke at det funker
        var testTable = new DataTable();
        testTable.DataRows.Add(new DataRow(new DateTime(2022, 1, 1), new double[] { 1 }));
        testTable.DataRows.Add(new DataRow(new DateTime(2020, 1, 1), new double[] { 1 }));
        testTable.DataRows.Add(new DataRow(new DateTime(2021, 1, 1), new double[] { 1 }));

        System.Console.WriteLine("\nTester med testdata før:");
        foreach (var dr in testTable.DataRows)
        {
            System.Console.WriteLine(dr.TimeStamp);
        }
        testTable.BubbleSort();
        System.Console.WriteLine("\nTester etter bubble sort:");
        foreach (var dr in testTable.DataRows)
        {
            System.Console.WriteLine(dr.TimeStamp);
        }


        //oppgave 5:

        JsonHandler jsonHandler = new JsonHandler();

        var dataDict = jsonHandler.LoadJsonFromFile("data.json");


        System.Console.WriteLine("\nJSON innhold: ");
        System.Console.WriteLine("\nKEY       VALUE");


        foreach (var kvp in dataDict)
        {
            System.Console.WriteLine($"{kvp.Key}    :    {kvp.Value}");
        }


        //oppgave 6:

        string jsonString = File.ReadAllText("data.json");

        var dict = JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonString);



        //oppgave 7:

        string alphabet = "abcdefghijklmnopqrstuvwxyz";


        int index1 = MatchPattern(alphabet, "def");
        int index2 = MatchPattern(alphabet, "xyz");
        int index3 = MatchPattern(alphabet, "å");

        System.Console.WriteLine($"Indeks 1: {index1}");
        System.Console.WriteLine($"Indeks 2: {index2}");
        System.Console.WriteLine($"Indeks 3: {index3} (finnes ikke)");



    }

    public static int MatchPattern(string data, string target)
    {

        for (int i = 0; i <= data.Length - target.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < target.Length; j++)
            {
                if (data[i + j] != target[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return i;
            }
        }

        return -1;

    }


}