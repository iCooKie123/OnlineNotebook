﻿using Microsoft.EntityFrameworkCore;
using OnlineNotebook.DatabaseConfigurations;
using OnlineNotebook.DatabaseConfigurations.Entities;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace DatabaseData
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("data source=LocalPC\\SQLEXPRESS;initial catalog=OnlineNotebook;TrustServerCertificate=true;trusted_connection=true");
            using (var context = new DatabaseContext(optionsBuilder.Options))
            {
                context.Database.EnsureDeleted();
                // Apply migrations
                context.Database.Migrate();

                // Insert data
                var users = new List<User>
                {
                    new("johnDoe@test.com","password123","John","Doe",1)
                    {
                    },
                    new("johnDoe2@test.com","password123","John2","Doe",2)
                    {
                    },
                    new("johnDoe3@test.com","password123","John3","Doe",3)
                    {
                    },
                };

                var classes = new List<StudyClass>
                {
                    new() {
                        Credits = 3,
                        Name = "Introduction to Computer Science",
                        Semester = 1,
                        Type = ClassType.Seminar,
                        YearOfStudy = 1
                    },
                    new() {
                        Credits = 4,
                        Name = "Programming Fundamentals",
                        Semester = 1,
                        Type = ClassType.Laborator,
                        YearOfStudy = 1
                    },

                    // Year 1, Semester 2
                    new() {
                        Credits = 3,
                        Name = "Data Structures",
                        Semester = 2,
                        Type = ClassType.Proiect,
                        YearOfStudy = 1
                    },
                    new() {
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
                    new() {
                        Credits = 4,
                        Name = "Database Management Systems",
                        Semester = 1,
                        Type = ClassType.Laborator,
                        YearOfStudy = 2
                    },

                    // Year 2, Semester 2
                    new() {
                        Credits = 3,
                        Name = "Software Engineering",
                        Semester = 2,
                        Type = ClassType.Seminar,
                        YearOfStudy = 2
                    },
                    new() {
                        Credits = 4,
                        Name = "Web Development",
                        Semester = 2,
                        Type = ClassType.Laborator,
                        YearOfStudy = 2
                    },

                    // Year 3, Semester 1
                    new() {
                        Credits = 3,
                        Name = "Operating Systems",
                        Semester = 1,
                        Type = ClassType.Proiect,
                        YearOfStudy = 3
                    },
                    new() {
                        Credits = 4,
                        Name = "Computer Networks",
                        Semester = 1,
                        Type = ClassType.Laborator,
                        YearOfStudy = 3
                    },

                    // Year 3, Semester 2
                    new() {
                        Credits = 3,
                        Name = "Software Testing",
                        Semester = 2,
                        Type = ClassType.Seminar,
                        YearOfStudy = 3
                    },
                    new() {
                        Credits = 4,
                        Name = "Cybersecurity",
                        Semester = 2,
                        Type = ClassType.Laborator,
                        YearOfStudy = 3
                    }
                };

                var studentClasses = new List<StudentClass>();

                for (int i = 0; i < 12; i++)
                {
                    User user;
                    if (i < 4)
                    {
                        user = users[0];
                    }
                    else if (i < 8)
                    {
                        user = users[1];
                    }
                    else
                    {
                        user = users[2];
                    }

                    int? grade = (user.Equals(users[1]) || user.Equals(users[0])) ? GetRandomGrade() : null;

                    studentClasses.Add(new StudentClass
                    {
                        Student = user,
                        Class = classes[i],
                        Grade = grade
                    });
                }

                context.Classes.AddRange(classes);
                context.Users.AddRange(users);
                context.StudentClases.AddRange(studentClasses);
                context.SaveChanges();
            }

            // Function to generate random grades
            static int? GetRandomGrade()
            {
                Random rand = new Random();
                return rand.Next(2, 11); // Random grade between 2 and 10
            }
        }
    }
}