using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindDuplicateFileInSystem
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public IList<IList<string>> FindDuplicate(string[] paths)
        {
            var dictionary = new Dictionary<string, List<string>>(); 

            foreach(var path in paths)
            {
                var words = path.Split(' ');

                var root = words[0]; 

                for(int i = 1; i < words.Length; i++)
                {
                    var visit = words[i];
                    var index = visit.IndexOf('(');
                    var fileName = visit.Substring(0, index);
                    var content = visit.Substring(index + 1, visit.Length - index - 1); //

                    var fileFullInfo = root + "/" + fileName;
                    if(dictionary.ContainsKey(content))
                    {
                        var list = dictionary[content];
                        list.Add(fileFullInfo);

                        dictionary[content] = list; 
                    }
                    else
                    {
                        var list = new List<string>(); 
                        list.Add(fileFullInfo); 
                        dictionary.Add(content, list); 
                    }
                }
            }

            var result = new List<IList<string>>(); 
           
            foreach(var pair in dictionary)
            {                
                var list = pair.Value; 
                if(list.Count <= 1)
                {
                    continue; 
                }

                result.Add(list); 
            }

            return result; 
        }
    }
}
