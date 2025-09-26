
//oppgave 1

class MyDictionary
{
    private List<(string key, double value)> items;
    public MyDictionary()
    {
        items = new List<(string key, double value)>();

    }


    //oppgave 2
    public void Add(string key, double value)
    {
        //looper igjennom alle items for å sjekke om key eksisterer
        foreach (var item in items)
        {
            if (item.key == key)
            {
                System.Console.WriteLine($"{key} finnes fra før");
                return;
            }
        }
        items.Add((key, value));
    }


    //oppgave 3
    public double? Get(string key)
    {
        //looper igjennom hele items til jeg finner riktig key, og returnerer item.value deretter
        foreach (var item in items)
        {
            if (item.key == key)
            {
                return item.value;
            }
        }
        //ingen verdi funnet
        return null;
    }
}


//oppgave 4

class MyStack
{
    
}



class Program
{
    static void Main(string[] args)
    {
        //oppgave 3
        Random rand = new Random();
        MyDictionary myDict = new MyDictionary();
        //initializing a built in dictionary object to compare against later
        Dictionary<string, double> builtInDict = new Dictionary<string, double>();


        //populating both dictionaries
        for (int i = 0; i < 10000; i++)
        {
            string key = "key" + i;
            double value = rand.NextDouble();
            myDict.Add(key, value);
            builtInDict.Add(key, value);
        }

        //testing time difference in looking up from both dictionaries

        var watch = System.Diagnostics.Stopwatch.StartNew();

        for (int i = 0; i < 10000; i++)
        {
            string key = "key" + rand.Next(0, 10000);
            myDict.Get(key);
        }
        watch.Stop();
        System.Console.WriteLine($"{watch.ElapsedMilliseconds} milliseconds elapsed");


        watch.Reset();
        watch.Start();
        for (int i = 0; i < 10000; i++)
        {
            string key = "key" + rand.Next(0, 10000);
            builtInDict.TryGetValue(key, out double value);

        }
        watch.Stop();
        System.Console.WriteLine($"{watch.ElapsedMilliseconds} milliseconds elapsed");

        /*
        Oppgave 3: The built in Dictionary TryGetValue is faster than my Dictionary class.
        
        I could use a Hash table to make my lookup faster. The current implementation is
        O(N) (linear search through the entire dict worst case). 

        With a Hash table you would have a method that turns a string into an integer value,
        then we have an "internal" array or a array in the background.
        Lets say "key123".GetHashCode() returns 123456789 or some other big integer.

        We then find a place in our array to store it by % size of our dictionary.
        In this problem it would be 123456789 % 10000 which would return the index of
        where we store our key and value.

        When looking up then, we do the same and find our hashnumber, we go to the index
        we get after our hashing function, and check the list if our key and value exists
    

        */

    }
}