﻿namespace PeregrineDb.Tests.Databases.Mapper.SharedTypes
{
    using System;

    public class Dog
    {
        public int? Age { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float? Weight { get; set; }

        public int IgnoredProperty => 1;
    }
}
