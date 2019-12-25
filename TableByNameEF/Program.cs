using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TableByNameEF
{
    class Program
    {
        static void Main(string[] args)
        {
            GetByNormalEFuse();
            GetByReflection("AnimalDog");
            GetBySwitchAndEF("AnimalDog");

            Console.WriteLine("Done. Press any key..");
            Console.ReadKey();
        }

        private static void GetBySwitchAndEF(string tableName)
        {
            var entities = new pubsEntities();
            switch (tableName)
            {
                case "AnimalDog":
                    foreach (var r in entities.AnimalDogs)
                        Console.Write(r.Name + "; ");
                    Console.WriteLine();
                    break;

                case "AnimalCat":
                    foreach (var r in entities.AnimalCats)
                        Console.Write(r.Name + "; ");
                    Console.WriteLine();
                    break;

                default:
                    break;
            }
        }

        private static void GetByReflection(string tableName)
        {
            // based on 
            // https://stackoverflow.com/questions/26305737/how-do-i-select-correct-dbset-in-dbcontext-based-on-table-name

            Type t = Type.GetType(Assembly.GetExecutingAssembly().GetName().Name + "." + tableName);
            var entities = new pubsEntities();
            DbSet dbset = entities.Set(t); 

            foreach (var r in dbset)
            {
                //Console.WriteLine(((TableByNameEF.AnimalCat)r).Name);
                var a = new Animal(r); // it will do converion 
                Console.Write(a.Name + "; ");
            }
            Console.WriteLine();
        }

        private static void GetByNormalEFuse()
        {
            var entities = new pubsEntities();
            foreach (var r in entities.AnimalDogs)
                Console.Write(r.Name + "; ");
            Console.WriteLine();
        }
    }
}
