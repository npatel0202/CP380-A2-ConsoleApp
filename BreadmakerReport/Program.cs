using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RatingAdjustment.Services;
using BreadmakerReport.Models;

namespace BreadmakerReport
{
    class Program
    {
        static string dbfile = @".\data\breadmakers.db";
        static RatingAdjustmentService ratingAdjustmentService = new RatingAdjustmentService();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bread World");
            var BreadmakerDb = new BreadMakerSqliteContext(dbfile);
            var BMList = BreadmakerDb.Breadmakers
            // TODO: add LINQ logic ...
            //       ...
            .Select(p => new
            {
                 Reviews = p.Reviews.Count,
                 Average = (Double)p.Reviews.Average(q => q.stars),
                 Adjust = ratingAdjustmentService.Adjust((Double)p.Reviews.Average(q => q.stars), (Double)p.Reviews.Count),
                 Desc = p.title
            })

            .ToList()
            .OrderByDescending(ratingAdjustmentService => ratingAdjustmentService.Adjust)
            .ToList();

            Console.WriteLine("[#]  Reviews Average  Adjust    Description");
            for (var j = 0; j < 3; j++)
            {
                var i = BMList[j];
                // TODO: add output
                // Console.WriteLine( ... );
                Console.WriteLine($"[{j + 1}] {i.Reviews} {Math.Round(i.Average, 2)} {Math.Round(i.Adjust, 2)} {i.Desc}");
            }
            Console.ReadLine();
        }
    }
}
