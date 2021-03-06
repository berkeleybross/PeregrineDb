// <copyright file="ISqlUnitOfWork.cs" company="Berkeleybross">
// Copyright (c) Berkeleybross. All rights reserved.
// </copyright>

namespace PeregrineDb
{
    using System.Data;

    public interface ISqlUnitOfWork
        : ISqlConnection
    {
        IDbTransaction Transaction { get; }

        void SaveChanges();

        void Rollback();
    }
}