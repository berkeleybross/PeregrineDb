﻿namespace PeregrineDb.Tests.Databases
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using FluentAssertions;
    using PeregrineDb.Dialects;
    using PeregrineDb.Tests.ExampleEntities;
    using PeregrineDb.Tests.Utils;
    using Xunit;

    public abstract class DefaultSqlConnectionPerformanceTests
    {
        private long PerformInsert(IDialect dialect)
        {
            using (var database = BlankDatabaseFactory.MakeDatabase(dialect))
            {
                // Arrange
                var entities = Enumerable.Range(0, 30000).Select(i => new SimpleBenchmarkEntity
                    {
                        FirstName = $"First Name {i}",
                        LastName = $"Last Name {i}",
                        DateOfBirth = DateTime.Now
                    }).ToList();

                var stopWatch = Stopwatch.StartNew();

                // Act
                using (var transaction = database.StartUnitOfWork())
                {
                    foreach (var entity in entities)
                    {
                        transaction.Insert(entity);
                    }

                    transaction.SaveChanges();
                }

                // Assert
                stopWatch.Stop();
                Console.WriteLine($"Performed insert in {stopWatch.ElapsedMilliseconds}ms");

                return stopWatch.ElapsedMilliseconds;
            }
        }

        private long PerformInsertRange(IDialect dialect)
        {
            using (var database = BlankDatabaseFactory.MakeDatabase(dialect))
            {
                // Arrange
                var entities = Enumerable.Range(0, 30000).Select(i => new SimpleBenchmarkEntity
                    {
                        FirstName = $"First Name {i}",
                        LastName = $"Last Name {i}",
                        DateOfBirth = DateTime.Now
                    }).ToList();

                var stopWatch = Stopwatch.StartNew();

                // Act
                using (var transaction = database.StartUnitOfWork())
                {
                    transaction.InsertRange(entities);
                    transaction.SaveChanges();
                }

                // Assert
                stopWatch.Stop();
                Console.WriteLine($"Performed insertrange in {stopWatch.ElapsedMilliseconds}ms");

                return stopWatch.ElapsedMilliseconds;
            }
        }

        public class SqlServer2012
            : DefaultSqlConnectionPerformanceTests
        {
            [Fact(Skip = "Too slow locally")]
            public void Takes_less_than_6_seconds_to_insert_30000_rows()
            {
                var timeTaken = this.PerformInsert(Dialect.SqlServer2012);
                timeTaken.Should().BeLessThan(6000);
            }

            [Fact(Skip = "Too slow locally")]
            public void Takes_less_than_6_seconds_to_InsertRange_30000_rows()
            {
                var timeTaken = this.PerformInsertRange(Dialect.SqlServer2012);
                timeTaken.Should().BeLessThan(6000);
            }
        }

        public class PostgreSQL
            : DefaultSqlConnectionPerformanceTests
        {
            [Fact(Skip = "Too slow locally")]
            public void Takes_less_than_6_seconds_to_insert_30000_rows()
            {
                var timeTaken = this.PerformInsert(Dialect.PostgreSql);
                timeTaken.Should().BeLessThan(6000);
            }

            [Fact(Skip = "Too slow locally")]
            public void Takes_less_than_6_seconds_to_InsertRange_30000_rows()
            {
                var timeTaken = this.PerformInsertRange(Dialect.PostgreSql);
                timeTaken.Should().BeLessThan(6000);
            }
        }
    }
}