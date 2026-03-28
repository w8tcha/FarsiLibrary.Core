namespace FarsiLibrary.Tests;

using FarsiLibrary.Core.Utils.Internals;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CollectionExtensionTests
{
    [Test]
    public void Can_Use_ForEach_On_Dictionaries()
    {
        var dictionary = new Dictionary<string, string>();
        var i = 0;

        dictionary.Add("John", "Doe");
        dictionary.ForEach(_ => i++);

        i.Should().Be(1);
    }

    [Test]
    public void Using_ForEach_On_Null_Will_Not_Throw()
    {
        IDictionary<object, object> dictionary = null;
        var i = 0;

        var act =  () => { dictionary.ForEach(_ => i++); };

        act.Should().NotThrow<Exception>();

        i.Should().Be(0);
    }
}