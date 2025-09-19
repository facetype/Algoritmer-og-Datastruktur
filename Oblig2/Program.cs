using System.IO;
using System;
using System.Globalization;

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
                values[i - 1] = double.Parse(row[i]);
            }

            DataRows.Add(new DataRow(dateTime, values));
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





class Program
{
    static void Main(string[] args)
    {
        DataTable dt = new DataTable();
        dt.LoadDataFromFile("dsDataTrain.csv");


        //tester

        Console.WriteLine("Kolonner:");
        Console.WriteLine(string.Join("|", dt.Columns));


        Console.WriteLine("\nFørste 5 rader:");
        for (int i = 0; i < 5 && i < dt.Rows.Count; i++)
        {
            Console.WriteLine(string.Join("|", dt.Rows[i]));
        }


    }
}