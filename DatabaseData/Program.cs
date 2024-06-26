﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace DatabaseData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            string connectionString = ConfigurationHelper.GetConfigValue(
                "OnlineNotebook\\OnlineNotebook",
                "ConnectionStrings:DefaultConnection"
            );

            optionsBuilder.UseSqlServer(connectionString);
            using var context = new DatabaseContext(optionsBuilder.Options);

            if (builder.Environment.IsDevelopment())
                context.Database.EnsureDeleted();

            context.Database.Migrate();

            // Insert data
            var users = new List<User>
            {
                new(
                    "johnDoe@test.com",
                    "password123",
                    "John",
                    "Doe",
                    1,
                    "Licență",
                    "FSA",
                    "Mate-Info",
                    "1311",
                    UserRoles.Student
                )
                {
                    },
                new(
                    "johnDoe2@test.com",
                    "password123",
                    "John2",
                    "Doe",
                    2,
                    "Licență",
                    "FSA",
                    "Mate-Info",
                    "1323",
                    UserRoles.Student
                )
                {
                    },
                new(
                    "johnDoe3@test.com",
                    "password123",
                    "John3",
                    "Doe",
                    3,
                    "Licență",
                    "FSA",
                    "Mate-Info",
                    "1333",
                    UserRoles.Student
                )
                {
                    },
                new("admin@admin.upb", "admin", "Admin", "Adminulescu") { },
            };

            var classes = new List<StudyClass>
            {
                new()
                {
                    Credits = 3,
                    Name = "Introduction to Computer Science",
                    Semester = 1,
                    Type = ClassType.Seminar,
                    YearOfStudy = 1
                },
                new()
                {
                    Credits = 4,
                    Name = "Programming Fundamentals",
                    Semester = 1,
                    Type = ClassType.Laborator,
                    YearOfStudy = 1
                },
                // Year 1, Semester 2
                new()
                {
                    Credits = 3,
                    Name = "Data Structures",
                    Semester = 2,
                    Type = ClassType.Proiect,
                    YearOfStudy = 1
                },
                new()
                {
                    Credits = 4,
                    Name = "Object-Oriented Programming",
                    Semester = 2,
                    Type = ClassType.Laborator,
                    YearOfStudy = 1
                },
                // Year 2, Semester 1
                new StudyClass
                {
                    Credits = 3,
                    Name = "Algorithm Design",
                    Semester = 1,
                    Type = ClassType.Seminar,
                    YearOfStudy = 2
                },
                new()
                {
                    Credits = 4,
                    Name = "Database Management Systems",
                    Semester = 1,
                    Type = ClassType.Laborator,
                    YearOfStudy = 2
                },
                // Year 2, Semester 2
                new()
                {
                    Credits = 3,
                    Name = "Software Engineering",
                    Semester = 2,
                    Type = ClassType.Seminar,
                    YearOfStudy = 2
                },
                new()
                {
                    Credits = 4,
                    Name = "Web Development",
                    Semester = 2,
                    Type = ClassType.Laborator,
                    YearOfStudy = 2
                },
                // Year 3, Semester 1
                new()
                {
                    Credits = 3,
                    Name = "Operating Systems",
                    Semester = 1,
                    Type = ClassType.Proiect,
                    YearOfStudy = 3
                },
                new()
                {
                    Credits = 4,
                    Name = "Computer Networks",
                    Semester = 1,
                    Type = ClassType.Laborator,
                    YearOfStudy = 3
                },
                // Year 3, Semester 2
                new()
                {
                    Credits = 3,
                    Name = "Software Testing",
                    Semester = 2,
                    Type = ClassType.Seminar,
                    YearOfStudy = 3
                },
                new()
                {
                    Credits = 4,
                    Name = "Cybersecurity",
                    Semester = 2,
                    Type = ClassType.Laborator,
                    YearOfStudy = 3
                }
            };

            var studentClasses = new List<StudentClass>();

            for (int i = 1; i < 4; i++)
            {
                var user = users[i - 1];

                for (int j = 0; j < 4 * i; j++)
                {
                    int? grade = GetRandomGrade();
                    studentClasses.Add(new StudentClass(user, classes[j], grade) { });
                }
            }

            var news = new List<News>
            {
                new("Title 1", "Content 1"),
                new("Title 2", "Content 2"),
                new("Title 3", "Content 3"),
            };
            context.News.AddRange(news);
            context.Classes.AddRange(classes);
            context.Users.AddRange(users);
            context.StudentClases.AddRange(studentClasses);
            context.SaveChanges();

            // Function to generate random grades
            static int? GetRandomGrade()
            {
                Random rand = new();
                return rand.Next(2, 11); // Random grade between 2 and 10
            }
        }





















        public static class ConfigurationHelper
        {
            public static string GetConfigValue(string projectName, string key)
            {
                if (string.IsNullOrEmpty(projectName) || string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException(nameof(projectName) + " or " + nameof(key));
                }

                var configuration = GetConfiguration(projectName);
                return configuration?.GetValue<string>(key);
            }

            private static IConfiguration GetConfiguration(string projectName)
            {
                var solutionPath = Path.GetDirectoryName(Directory.GetCurrentDirectory());
                var projectDirectory = Path.Combine(solutionPath, projectName);
                var configPath = Path.Combine(projectDirectory, "appsettings.json");

                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException(
                        $"appsettings.json not found for project: {projectName}"
                    );
                }

                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(projectDirectory)
                    .AddJsonFile(configPath);

                return configurationBuilder.Build();
            }
        }
    }
}
