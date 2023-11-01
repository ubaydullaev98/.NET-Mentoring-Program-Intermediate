﻿/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            await Task.Run(() =>
            {
                var array = CreateArray();
                OutputInfo("Initial array is", array);
                return array;
            })
               .ContinueWith(x =>
               {
                   var randNum = new Random();
                   var value = randNum.Next(1, 10);
                   var array = Multiply(x.Result, value);
                   OutputInfo($"Initial array multiplied by {value} is", array);
                   return array;
               })
               .ContinueWith(x =>
               {
                   var array = SortArray(x.Result);
                   OutputInfo("Sorted array is", array);
                   return array;
               })
               .ContinueWith(x =>
               {
                   var averageValue = GetAverage(x.Result);
                   OutputInfo($"Average of the sorted array is {averageValue}", null);
               });

            Console.ReadLine();
        }

        private static int[] CreateArray()
        {

            var rnd = new Random();
            var array = new int[10];
            for (int i = 0; i < 10; i++)
            {
                array[i] = rnd.Next(0, 99);
            }
            return array;
        }
 
        static int[] Multiply(int[] array, int val)
        {
            int[] multiple = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                multiple[i] = array[i] * val;
            }

            return multiple;
        }

        static int[] SortArray(int[] array)
            => array.OrderBy(i => i).ToArray();

        static double GetAverage(int[] array)
            => array.Average(i => i);

        static void OutputInfo(string text, int[] array)
            => Console.WriteLine($"{text} {(array == null ? string.Empty : $"[{string.Join(", ", array)}]")}.");
    }
}
