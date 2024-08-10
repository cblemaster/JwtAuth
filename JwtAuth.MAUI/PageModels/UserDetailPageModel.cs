using CommunityToolkit.Mvvm.ComponentModel;
using JwtAuth.Core.DataTransferObjects;

namespace JwtAuth.MAUI.PageModels;

public partial class UserDetailPageModel : PageModelBase<GetUserDTO>
{
    [ObservableProperty]
    private GetUserDTO detailUser = null!;
}
