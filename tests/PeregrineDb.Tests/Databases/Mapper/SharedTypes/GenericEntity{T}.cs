﻿namespace PeregrineDb.Tests.Databases.Mapper.SharedTypes
{
    public class GenericEntity<T>
    {
        public GenericEntity()
        {
        }

        public GenericEntity(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
    }
}