using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

/// <summary>
/// code study:
/// https://www.hackerrank.com/rest/contests/morgan-stanley-codeathon-2017/challenges/shell-sort-command/hackers/outvoider/download_solution
/// </summary>
class Solution
{
    static void Main(String[] args)
    {
        HandleConsoleInput(); 
        new OrderStrings().Solve();
    }

    public static void HandleConsoleInput()
    {
        var item = new OrderStrings(); 

        var number = int.Parse(Console.ReadLine());

        item.StringsToOrder = new string[number];
        for (var i = 0; i < number; ++i)
        {
            item.StringsToOrder[i] = Console.ReadLine();
        }

        item.Commands = Console.ReadLine().Split(' ');

        item.Column = int.Parse(item.Commands[0]) - 1;

        item.Key = item.StringsToOrder.Select(v => v.Split(' ')[item.Column]);

        item.Solve();

        foreach (var v in item.StringsToOrder)
        {
            Console.WriteLine(v);
        }
    }
}

class OrderStrings
{
    public int      Column   { get; set; }
    public string[] Commands { get; set; }
    public IEnumerable[]   Key      { get; set; }
    public string[] StringsToOrder { get; set; }

    public void Solve()
    {
        if (Commands[2] == "numeric")
        {
            SortByNumericValue(); 
        }
        else
        {
            SortByString(); 
        }

        if (Commands[1] == "true")
        {
            Array.Reverse(StringToOrder);
        }        
    }

    private void SortByNumericValue()
    {
        var keyArray = Key.Select((item, index) => (BigInteger.Parse(item) << 32) + index).ToArray();
        Array.Sort(keyArray, StringToOrder); 
    }

    private void SortByString()
    {
        var keyArray = Key.ToArray();
        Array.Sort(keyArray, StringToOrder);
    }
}