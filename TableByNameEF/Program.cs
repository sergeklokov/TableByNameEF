using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
//using Microsoft.Data.Entity;

namespace TableByNameEF
{
    class Program
    {
        const int pageSize = 2;
        const int pageNumber = 1;
        static pubsEntities entities = new pubsEntities();

        static void Main()
        {
            GetByNormalEFuse();
            GetBySwitchAndEF("AnimalDog");
            GetByReflection("AnimalDog");
            GetByGenerics("AnimalDog");

            Console.WriteLine("Done. Press any key..");
            Console.ReadKey();
        }

        private static void GetBySwitchAndEF(string tableName)
        {
            Console.WriteLine("GetBySwitchAndEF");
            switch (tableName)
            {
                case "AnimalDog":
                    {
                        var selected = entities.AnimalDogs.Select(r => new { r.Name, r.Age });
                        int count = selected.Count();
                        var display = selected.OrderBy(r => r.Name).Skip(pageNumber * pageSize).Take(pageSize);
                        foreach (var r in display)
                            Console.Write(r.Name + "; ");
                        Console.WriteLine($"count = {count}");
                        Console.WriteLine();
                    }
                    break;

                case "AnimalCat":
                    {
                        var selected = entities.AnimalCats.Select(r => new { r.Name, r.Age });
                        int count = selected.Count();
                        var display = selected.OrderBy(r => r.Name).Skip(pageNumber * pageSize).Take(pageSize);
                        foreach (var r in display)
                            Console.Write(r.Name + "; ");
                        Console.WriteLine($"count = {count}");
                        Console.WriteLine();
                    }
                    break;

                default:
                    break;
            }
        }

        private static void GetByNormalEFuse()
        {
            Console.WriteLine("GetByNormalEFuse");
            var selected = entities.AnimalDogs.Select(r => new { r.Name, r.Age });
            int count = selected.Count();
            var display = selected.OrderBy(r => r.Name).Skip(pageNumber * pageSize).Take(pageSize);

            foreach (var r in display)
                Console.Write(r.Name + "; ");
            Console.WriteLine($"count = {count}");
            Console.WriteLine();
        }

        private static void GetByReflection(string tableName)
        {
            Console.WriteLine("GetByReflection");
            // based on 
            // https://stackoverflow.com/questions/26305737/how-do-i-select-correct-dbset-in-dbcontext-based-on-table-name

            Type t = Type.GetType(Assembly.GetExecutingAssembly().GetName().Name + "." + tableName);
            DbSet dbset = entities.Set(t);  // this is NON GENERIC VERSION! unfortunately

            foreach (var r in dbset)
            {
                //Console.WriteLine(((TableByNameEF.AnimalCat)r).Name);
                var a = new Animal(r); // it will do converion 
                Console.Write(a.Name + "; ");
            }
            Console.WriteLine();
        }

        private static void GetByGenerics(string tableName)
        {
            Console.WriteLine("GetByGenerics");
            //https://stackoverflow.com/questions/21533506/find-a-specified-generic-dbset-in-a-dbcontext-dynamically-when-i-have-an-entity
            Type t = Type.GetType(Assembly.GetExecutingAssembly().GetName().Name + "." + tableName);
            var q1 = EntityFrameworkExtensions.Set(entities, t);  // it is Queryable
            Console.WriteLine(q1);

            var q2 = EntityFrameworkExtensions.Set<AnimalDog>(entities);
            var s2 = q2.Select(r => new { r.Name, r.Age });
            Console.WriteLine(s2);
            Console.WriteLine(s2.FirstOrDefault());

            //var selected = dbset.Select(r => new { r.Name, r.Age });

            foreach (var r in q1)
            {
                //Console.WriteLine(((TableByNameEF.AnimalCat)r).Name);
                var a = new Animal(r); // it will do converion 
                Console.Write(a.Name + "; ");
            }
            Console.WriteLine();
        }


    }


    public static class EntityFrameworkExtensions
    {
        public static IQueryable Set(this DbContext context, Type T)
        {
            // Get the generic type definition
            // System.Reflection.AmbiguousMatchException: 'Ambiguous match found.'
            //MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);

            // let's find proper methods because ambigiuty is possible 
            // {System.Data.Entity.DbSet`1[TEntity] Set[TEntity]()}
            //var methods = typeof(DbContext).GetMethods().Where(p => p.Name == "Set");
            //Console.WriteLine(methods);

            MethodInfo method = typeof(DbContext).GetMethods()
                .Where(p => p.Name == "Set" && p.ContainsGenericParameters).FirstOrDefault();
                                   
            // Build a method with the specific type argument you're interested in
            method = method.MakeGenericMethod(T);

            return method.Invoke(context, null) as IQueryable;
        }

        public static IQueryable<T> Set<T>(this DbContext context)
        {
            //MethodInfo method = typeof(DbContext).GetMethod(nameof(DbContext.Set), BindingFlags.Public | BindingFlags.Instance);
            MethodInfo method = typeof(DbContext).GetMethods()
                .Where(p => p.Name == "Set" && p.ContainsGenericParameters).FirstOrDefault();

            // Build a method with the specific type argument you're interested in 
            method = method.MakeGenericMethod(typeof(T));

            return method.Invoke(context, null) as IQueryable<T>;
        }

        //public static IEnumerable<object> AsEnumerable(this DbSet set)
        //{
        //    foreach (var entity in set)
        //    {
        //        yield return entity;
        //    }
        //}

    }
}
