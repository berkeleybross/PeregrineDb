namespace PeregrineDb.Tests.Databases.Mapper
{
    using Dapper;
    using Xunit;

    public class DynamicParameterTests
    {
        [Fact]
        public void SO30156367_DynamicParamsWithoutExec()
        {
            var dbParams = new DynamicParameters();
            dbParams.Add("Field1", 1);
            var value = dbParams.Get<int>("Field1");
            Assert.Equal(1, value);
        }
    }
}