// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello C# Programmers");

using System;
using System.Data;
using System.Text.Json;
using AutoMapper;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();

            // string fileText = File.ReadAllText("log.txt");

            // Console.Write(fileText);

            //string computersJson = File.ReadAllText("Computers.json");
            string computersJson = File.ReadAllText("ComputersSnake.json");

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, options =>
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.CPUCores, options =>
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.HasLTE, options =>
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.HasWifi, options =>
                        options.MapFrom(source => source.has_wifi))
                    .ForMember(destination => destination.Motherboard, options =>
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.VideoCard, options =>
                        options.MapFrom(source => source.video_card))
                    .ForMember(destination => destination.Price, options =>
                        options.MapFrom(source => source.price * .8m));
            }));

            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if (computersSystem != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                Console.WriteLine("Automapper Count:" + computerResult.Count());

                // foreach (Computer computer in computerResult)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            }

            IEnumerable<Computer>? computersJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);
            if (computersJsonPropertyMapping != null)
            {
                Console.WriteLine("JSON Property Count:" + computersJsonPropertyMapping.Count());
                // foreach (Computer computer in computersJsonPropertyMapping)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            }

            // Console.WriteLine(computersJson);

            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computersNewtonSoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            // IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

            // if (computersNewtonSoft != null)
            // {
            //     foreach(Computer computer in computersNewtonSoft)
            //     {
            //         //Console.WriteLine(computer.Motherboard);

            //         string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //             Motherboard,
            //             HasWifi,
            //             HasLTE,
            //             ReleaseDate,
            //             Price,
            //             VideoCard
            //         ) VALUES ('" + escapeSingleQuote(computer.Motherboard)
            //                 + "','" + computer.HasWifi
            //                 + "','" + computer.HasLTE
            //                 + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
            //                 + "','" + computer.Price
            //                 + "','" + escapeSingleQuote(computer.VideoCard)
            //         + "')";

            //         dapper.ExecuteSql(sql);
            //     }
            // }

            // JsonSerializerSettings settings = new JsonSerializerSettings()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };

            // string computersCopyNewtonSoft = JsonConvert.SerializeObject(computersNewtonSoft, settings);

            // File.WriteAllText("computersCopyNewtonSoft.txt", computersCopyNewtonSoft);

            // string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options);

            // File.WriteAllText("computersCopySystem.txt", computersCopySystem);
        }

        static string escapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }
    }
}