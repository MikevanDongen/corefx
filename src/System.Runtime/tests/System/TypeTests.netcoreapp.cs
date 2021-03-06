using System.Collections.Generic;
using Xunit;

namespace System.Tests
{
    public class TypeTestsNetcore
    { 
        private static IEnumerable<object[]> SZArrayOrNotTypes()
        {
            yield return new object[] {typeof(int[]), true};
            yield return new object[] {typeof(string[]), true};
            yield return new object[] {typeof(void), false};
            yield return new object[] {typeof(int), false};
            yield return new object[] {typeof(int[]).MakeByRefType(), false};
            yield return new object[] {typeof(int[,]), false};
            yield return new object[] {typeof(TypeTests), false};
            yield return new object[] {Array.CreateInstance(typeof(int), new[] {2}, new[] {-1}).GetType(), false};
            yield return new object[] {Array.CreateInstance(typeof(int), new[] {2}, new[] {1}).GetType(), false};
            yield return new object[] {Array.CreateInstance(typeof(int), new[] {2}, new[] {0}).GetType(), true};
            yield return new object[] {typeof(int[][]), true};
            yield return new object[] {Type.GetType("System.Int32[]"), true};
            yield return new object[] {Type.GetType("System.Int32[*]"), false};
            yield return new object[] {Type.GetType("System.Int32"), false};
            yield return new object[] {typeof(int).MakeArrayType(), true};
            yield return new object[] {typeof(int).MakeArrayType(1), false};
            yield return new object[] {typeof(int).MakeArrayType().MakeArrayType(), true};
            yield return new object[] {typeof(int).MakeArrayType(2), false};
            yield return new object[] {typeof(Outside<int>.Inside<string>), false};
            yield return new object[] {typeof(Outside<int>.Inside<string>[]), true};
            yield return new object[] {typeof(Outside<int>.Inside<string>[,]), false};
            yield return new object[] {Array.CreateInstance(typeof(Outside<int>.Inside<string>), new[] {2}, new[] {-1}).GetType(), false};
        }

        [Theory, MemberData(nameof(SZArrayOrNotTypes))]
        public void IsSZArray(Type type, bool expected)
        {
            Assert.Equal(expected, type.IsSZArray);
        }
    }
}