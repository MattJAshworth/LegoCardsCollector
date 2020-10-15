using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace LegoCardsCollector
{
    class Program
    {

        public static JObject o1;
        public static int counter = 0;

        static void Main(string[] args)
        {
            o1 = JObject.Parse(File.ReadAllText("cards.json"));

            Console.WriteLine("Lego Living Amazingly Collector Cards Tracker!");
            Console.WriteLine();
            Console.WriteLine("Use the following commands to get started:");
            Console.WriteLine("load - Loads your existing collection");
            Console.WriteLine("edit - Make changes to your collection! Set card(s) as collected");
            Console.WriteLine("reset - Restart your collection from the beginning");
            Console.WriteLine("commit - Save changes");
            Console.WriteLine();
            Console.WriteLine("By MattJAshworth, 2020.");
            Console.WriteLine();


            ShowStats(o1);

            MainMenu();

            //continueClass();
            //LoadJson();
        }

        public static void MainMenu()
        {

            string modifier = Console.ReadLine();

            if (modifier == "")
            {
                Console.WriteLine("Please enter a command!");
                Console.WriteLine("Commands: load, edit, batch, reset, commit");
            }

            if (modifier == "load")
            {
                LoadJson(o1);
            }

            if (modifier == "commit")
            {
                WriteJson(o1);
                Console.WriteLine("Changes saved!");
                Console.WriteLine("Commands: load, edit, batch, reset, commit");
                Console.WriteLine();
            }

            if (modifier == "edit")
            {
                Console.WriteLine("Enter card number collected!");
                int cardNo = int.Parse(Console.ReadLine());
                if (cardNo > 140 || cardNo < 1)
                {
                    Console.WriteLine("Please enter a card number from 1 to 149!");
                } else
                {
                    o1[cardNo.ToString()] = true;
                    Console.WriteLine("Card: " + cardNo + " Collected!");
                    Console.WriteLine("Don't forget to save your changes with the 'commit' modifier!");
                    Console.WriteLine();
                }
            }

            if (modifier == "batch")
            {
                Console.WriteLine("Enter card numbers collected, type 'finish' when done!");
                BatchTest();
            }
  

            if (modifier == "reset")
            {
                Console.WriteLine("Enter number to reset, or 0 to reset all!");
                int cardNo = int.Parse(Console.ReadLine());
                if (cardNo == 0)
                {
                    for (int i = 1; i < 141; i++)
                    {
                        o1[i.ToString()] = false;
                        Console.WriteLine("All cards reset! Have Fun Collecting!");
                    }
                } else
                {
                    o1[cardNo.ToString()] = false;
                    Console.WriteLine("Card: " + cardNo + " reset to not colleted!");
                }
            }

            if (modifier != "load" && modifier != "edit" && modifier != "batch" && modifier != "reset" && modifier != "commit" )
            {
                Console.WriteLine("Command '" + modifier + "' not recognised!");
                Console.WriteLine("Commands: load, edit, batch, reset, commit");
                Console.WriteLine();
            }

            MainMenu();
        }

        public static void BatchTest()
        {
            string input = Console.ReadLine();

            if (input == "finish")
            {
                //Do Something
                Console.WriteLine(counter + " Cards Collected!");
                Console.WriteLine("Don't forget to save your changes with the 'commit' modifier!");
                Console.WriteLine("Commands: load, edit, batch, reset, commit");
                Console.WriteLine();
            }
            else
            {

                counter++;
                BatchLoop(input);
            }
        }

        public static void BatchLoop(String input)
        {
            int cardNo = int.Parse(input);
            if (cardNo > 140 || cardNo < 1)
            {
                Console.WriteLine("Please enter a card number between 1 and 140!");
                BatchTest();
            } else
            {
                o1[cardNo.ToString()] = true;
                BatchTest();
            }
        }

        public static void ShowStats(JObject o1)
        {
            Decimal remainingCounter = 0;
            Decimal collectedCounter = 0;

            for (int i = 1; i < 141; i++)
            {
                if (o1.Value<Boolean>(i.ToString()) == true)
                {
                    collectedCounter++;
                } else
                {
                    remainingCounter++;
                }
            }

            Console.WriteLine("STATS:");
            Decimal temp = collectedCounter / 140;
            temp = temp * 100;
            Console.WriteLine(Decimal.ToInt32(temp) + "% Collected");
            Console.WriteLine(collectedCounter + " Cards Collected!");
            Console.WriteLine(remainingCounter + " Cards Remaining!");
            Console.WriteLine();
        }

        public static void LoadJson(JObject o1)
        {
            for (int i = 1; i < 141; i++)
            {
                Console.WriteLine("Card " + i + ": " + o1.Value<Boolean>(i.ToString()));
            }

            Console.WriteLine();
            Console.WriteLine("Commands: load, edit, batch, reset, commit");
            Console.WriteLine();
        }
       
        public static void WriteJson(JObject o1)
        {
            File.WriteAllText("cards.json", o1.ToString());

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText("cards.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                o1.WriteTo(writer);
            }
        }

    }
}
