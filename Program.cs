﻿using System;

namespace workshop_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Member one = new Member("Kalle", "Anka", "199002021412", 1); //Wrong Social Security Number
            Console.WriteLine(one.FullName);
        }
    }
}
