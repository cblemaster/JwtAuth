using CommunityToolkit.Mvvm.ComponentModel;
using JwtAuth.DataClient;

namespace JwtAuth.MAUI.PageModels;

public abstract class PageModelBase : ObservableObject
{
    public readonly IDataClient _dataClient;

    public PageModelBase(IDataClient dataClient)
    {
        _dataClient = dataClient;
    }
}
