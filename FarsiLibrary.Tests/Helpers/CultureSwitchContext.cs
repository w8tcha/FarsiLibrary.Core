namespace FarsiLibrary.Tests.Helpers;

using System;
using System.Threading;

public class CultureSwitchContext : IDisposable
{
    private readonly CultureInfo newCulture;

    private CultureInfo oldCulture;

    private CultureInfo oldUiCulture;

    public CultureSwitchContext(CultureInfo newCulture)
    {
        this.newCulture = newCulture;
        this.SnapshotCulture();
    }

    private void SnapshotCulture()
    {
        this.oldUiCulture = Thread.CurrentThread.CurrentUICulture;
        this.oldCulture = Thread.CurrentThread.CurrentCulture;

        Thread.CurrentThread.CurrentUICulture = this.newCulture;
        Thread.CurrentThread.CurrentCulture = this.newCulture;
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentUICulture = this.oldUiCulture;
        Thread.CurrentThread.CurrentCulture = this.oldCulture;
    }
}