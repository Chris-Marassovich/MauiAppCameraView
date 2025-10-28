namespace MauiAppCameraView.Pages;

public partial class CameraViewPage : ContentPage
{
    CameraViewPageViewModel viewModel;
	public CameraViewPage(CameraViewPageViewModel model)
	{
		InitializeComponent();
		BindingContext = viewModel = model;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
        await viewModel.RefreshCamerasCommand.ExecuteAsync(cancellationTokenSource.Token);
        await Camera.StartCameraPreview(cancellationTokenSource.Token);
    }
}