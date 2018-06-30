using System.Collections.Generic;
public class Solution
{
    public static void Main()
    {
        var lunchMenuPairs = new string[,]{
            {"pizza","Italian"},
            {"curry","Indian"},
            {"Masala","Indian"}
        };

        var teamCusinie = new string[,]{
            {"Jose","Italian"},
            {"John","Indian"},
            {"Sarah","Thai"},
            {"Mary","*"}
        };

        var result = matchLunches(lunchMenuPairs, teamCusinie); 

    }
    // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
    // RETURN AN EMPTY MATRIX IF PREFERRED LUNCH IS NOT FOUND
    public static string[,] matchLunches(string[,] lunchMenuPairs,
                                            string[,] teamCuisinePreference)
    {
        // WRITE YOUR CODE HERE
        if(teamCuisinePreference == null || 
            teamCuisinePreference.GetLength(0 )== 0 || teamCuisinePreference.GetLength(1) == 0 ||
            lunchMenuPairs.GetLength(0) == 0 || lunchMenuPairs.GetLength(1) == 0)
        {
            return new string[0, 0]; 
        }

        var lunchMenu = getLunchMenu(lunchMenuPairs);
 

        int rows = teamCuisinePreference.GetLength(0);
        int cols = teamCuisinePreference.GetLength(1);

        var nameLunchChoices = new List<string[]>(); 

        for (int row = 0; row < rows; row++)
        {            
            var name = teamCuisinePreference[row,   0];
            var cursine = teamCuisinePreference[row, 1];

            bool isStar = cursine.CompareTo("*") == 0; 

            if(isStar)
            {
                foreach(var pair in lunchMenu)
                {
                    foreach(var item in pair.Value)
                    {
                        nameLunchChoices.Add(new string[] { name, item });
                    }
                }
            }
            else
            {
                if(!lunchMenu.ContainsKey(cursine))
                {
                    continue; 
                }
                var choices = lunchMenu[cursine]; 
                foreach(var item in choices)
                {
                    nameLunchChoices.Add(new string[] { name, item });
                }
            }            
        }

        return toStringDimensionArray(nameLunchChoices); 
    }

    private static string[,] toStringDimensionArray(List<string[]> nameLunchChoices)
    {
        int choices = nameLunchChoices.Count; 
        var choicesFinal = new string[choices, 2];

        int index = 0; 
        foreach(var item in nameLunchChoices)
        {
            choicesFinal[index, 0] = item[0];
            choicesFinal[index, 1] = item[1]; 
            index ++; 
        }

        return choicesFinal; 
    }

    private static Dictionary<string, List<string>> getLunchMenu(string[,] lunchMenuPairs)
    {
        int rows = lunchMenuPairs.GetLength(0);
        int cols = lunchMenuPairs.GetLength(1);

        var result = new Dictionary<string, List<string>>(); 
        for(int row = 0; row < rows; row++)
        {
            var cusine = lunchMenuPairs[row, 0];
            var style = lunchMenuPairs[row, 1]; 

            if(result.ContainsKey(style))
            {
                var existing = result[style]; 
                existing.Add(cusine);
                result[style] = existing; 
                // result[style].Add(cusine);
            }
            else
            {
                var list = new List<string>();
                list.Add(cusine);
                result.Add(style, list); 
            }
        }

        return result; 
    }
    // METHOD SIGNATURE ENDS
}