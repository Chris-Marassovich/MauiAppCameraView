using CommunityToolkit.Mvvm.Input;
using MauiAppCameraView.Models;

namespace MauiAppCameraView.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}