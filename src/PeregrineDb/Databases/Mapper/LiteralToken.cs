﻿namespace PeregrineDb.Databases.Mapper
{
    using System.Collections.Generic;
    
    /// <summary>
    /// Represents a placeholder for a value that should be replaced as a literal value in the resulting sql
    /// </summary>
    internal struct LiteralToken
    {
        /// <summary>
        /// The text in the original command that should be replaced
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// The name of the member referred to by the token
        /// </summary>
        public string Member { get; }

        internal LiteralToken(string token, string member)
        {
            this.Token = token;
            this.Member = member;
        }

        internal static readonly IList<LiteralToken> None = new LiteralToken[0];
    }
}
