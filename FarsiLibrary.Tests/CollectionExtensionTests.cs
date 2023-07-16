namespace FarsiLibrary.Tests;

using System.Collections.Generic;

using FarsiLibrary.Utils.Internals;

public class CollectionExtensionTests
{
    [Fact]
    public void Can_Use_ForEach_On_Dictionaries()
    {
        var dictionary = new Dictionary<string, string>();
        var i = 0;

        dictionary.Add("John", "Doe");
        dictionary.ForEach(_ => i++);

        Assert.Equal(1, i);
    }

    [Fact]
    public void Using_ForEach_On_Null_Will_Not_Throw()
    {
        IDictionary<object, object> dictionary = null;
        var i = 0;

        var exception = Record.Exception(() => dictionary.ForEach(o => i++));
        Assert.Null(exception);

        Assert.Equal(0, i);
    }
}