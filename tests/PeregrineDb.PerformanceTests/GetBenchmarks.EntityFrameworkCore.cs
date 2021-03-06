// <copyright file="GetBenchmarks.EntityFrameworkCore.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>

namespace PeregrineDb.PerformanceTests
{
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using Microsoft.EntityFrameworkCore;

    public class EfCoreGetBenchmarks : GetBenchmarks
    {
        private EntityFrameworkCore.EFCoreContext Context;

        [GlobalSetup]
        public void Setup()
        {
            this.BaseSetup();
            this.Context = new EntityFrameworkCore.EFCoreContext(this.Connection.ConnectionString);
        }

        [Benchmark(Description = "Normal")]
        public Post Normal()
        {
            this.Step();
            return this.Context.Posts.First(p => p.Id == this.i);
        }

        [Benchmark(Description = "SqlQuery")]
        public Post SqlQuery()
        {
            this.Step();
            return this.Context.Posts.FromSql("select * from Posts where Id = {0}", this.i).First();
        }

        [Benchmark(Description = "No Tracking")]
        public Post NoTracking()
        {
            this.Step();
            return this.Context.Posts.AsNoTracking().First(p => p.Id == this.i);
        }
    }
}