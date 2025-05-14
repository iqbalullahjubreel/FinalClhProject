using CommunityToolkit.Mvvm.DependencyInjection;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using Microsoft.Maui.Controls;

namespace FinalClhProject.Views;

[QueryProperty(nameof(UserId), "userId")]
public partial class DeleteUserPage : ContentPage
{
    private DeleteUserViewModel _viewModel;
    private int _userId;

    public int UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            if (_viewModel != null)
            {
                _viewModel.Id = _userId;
            }
        }
    }

    public DeleteUserPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel = Ioc.Default.GetRequiredService<DeleteUserViewModel>();
        BindingContext = _viewModel;
        _viewModel.Id = _userId;
    }

}
