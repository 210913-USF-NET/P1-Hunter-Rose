using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace UI
{
    public class StoreService
    {
        /// <summary>
        /// Lists all the stores and locations available then asks the user to select one
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="listToPick"></param>
        /// <returns></returns>
            public Stores SelectAStore(string prompt, List<Stores> listToPick)
        {
            selectStore:
            Console.WriteLine(prompt);
            for (int i = 0; i < listToPick.Count; i++)
            {
                Console.WriteLine($"[{i}] {listToPick[i]}");
            }
            string input = Console.ReadLine();
            int parsedInput;

            //pass by reference in, out, ref
            bool parseSuccess = Int32.TryParse(input, out parsedInput);

            //I'm checking to see that parse has been successful
            //and the result stays within the boundary of the index
            if(parseSuccess && parsedInput >= 0 && parsedInput < listToPick.Count)
            {
                return listToPick[parsedInput];
            }
            else {
                Console.WriteLine("invalid input");
                goto selectStore;
            }
        }
       
    }
}