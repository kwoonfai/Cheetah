using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Cheetah
{
    class Recipients
    {
        public List<InternalStructure> recipients { get; set; }
    }

    public class InternalStructure
    {
        public List<string> tags { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input;
            try
            {
                input = File.ReadAllText("data.json");
            }
            catch
            {
                Console.WriteLine("data.json is missing");
                return;
            }
            
            Recipients rcp = JsonConvert.DeserializeObject<Recipients>(input);
            List<Tuple<string, string>> myResult2 = new List<Tuple<string, string>>();

            for (int a=0; a<rcp.recipients.Count; a++)
            {
                if (a == rcp.recipients.Count - 1) break;
                var compareTo = GetExtractedInternalStructure(rcp, a + 1);

                int count = a+1;
                foreach (var element in compareTo)
                {
                    var intersect = rcp.recipients[a].tags.Intersect(element.tags);
                    if (intersect.Count() >= 2)
                    {
                        myResult2.Add(new Tuple<string, string>(rcp.recipients[a].name, element.name));
                    }
                    count++;
                }
            }
            var sorted = myResult2.OrderBy(x => x.Item1);

            Console.WriteLine("Unsorted list:");
            foreach (var v in myResult2)
            {
                Console.Write($"{v.Item1}, {v.Item2}|");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sorted list:");
            foreach (var v in sorted)
            {
                Console.Write($"{v.Item1}, {v.Item2}|");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Easy view:");
            foreach (var v in sorted)
            {
                Console.WriteLine($"{v.Item1}, {v.Item2}");
            }
        }

        static List<InternalStructure> GetExtractedInternalStructure(Recipients rcp, int startCopyFrom)
        {
            List<InternalStructure> result = new List<InternalStructure>();

            if (startCopyFrom > rcp.recipients.Count - 1) return result;
            for (int a=startCopyFrom; a<rcp.recipients.Count; a++)
            {
                result.Add(rcp.recipients[a]);
            }
            return result;
        }
    }
}
