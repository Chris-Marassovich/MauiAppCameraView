using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiAppCameraView.PageModels
{
    public partial class CameraViewPageViewModel : ObservableObject
    {
        readonly ICameraProvider cameraProvider;

        static class LayoutStateKey
        {
            public const string Preview = nameof(Preview);
            public const string Capture = nameof(Capture);
        }

        public CameraViewPageViewModel(ICameraProvider cameraProvider)
        {
            this.cameraProvider = cameraProvider;
            MainLayoutState = string.Empty;
        }

        public IReadOnlyList<CameraInfo> Cameras => cameraProvider.AvailableCameras ?? [];

        public CancellationToken Token => CancellationToken.None;

        public ICollection<CameraFlashMode> FlashModes { get; } = Enum.GetValues<CameraFlashMode>();

        [ObservableProperty]
        private string mainLayoutState;

        [ObservableProperty]
        private CameraFlashMode flashMode;

        [ObservableProperty]
        private CameraInfo? selectedCamera;

        [ObservableProperty]
        private Size selectedResolution;

        [ObservableProperty]
        private float currentZoom;

        [ObservableProperty]
        private string cameraNameText = string.Empty;

        [ObservableProperty]
        private string zoomRangeText = string.Empty;

        [ObservableProperty]
        private string currentZoomText = string.Empty;

        [ObservableProperty]
        private string flashModeText = string.Empty;

        [ObservableProperty]
        private string resolutionText = string.Empty;

        [RelayCommand]
        async Task RefreshCameras(CancellationToken token) => await cameraProvider.RefreshAvailableCameras(token);

        [RelayCommand]
        private async Task CapturePhoto()
        {
            MainLayoutState = LayoutStateKey.Preview;
            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task PreviewRetake()
        {
            MainLayoutState = LayoutStateKey.Capture;
            await Task.CompletedTask;
        }

        partial void OnFlashModeChanged(CameraFlashMode value)
        {
            UpdateFlashModeText();
        }

        partial void OnCurrentZoomChanged(float value)
        {
            UpdateCurrentZoomText();
        }

        partial void OnSelectedResolutionChanged(Size value)
        {
            UpdateResolutionText();
        }

        partial void OnSelectedCameraChanged(CameraInfo? oldValue, CameraInfo? newValue)
        {
            UpdateCameraInfoText();
        }

        void UpdateCameraInfoText()
        {
            if (SelectedCamera is null)
            {
                CameraNameText = string.Empty;
                ZoomRangeText = string.Empty;
            }
            else
            {
                CameraNameText = $"{SelectedCamera.Name}";
                ZoomRangeText = $"Min Zoom: {SelectedCamera.MinimumZoomFactor}, Max Zoom: {SelectedCamera.MaximumZoomFactor}";
                UpdateFlashModeText();
            }
        }

        void UpdateFlashModeText()
        {
            if (SelectedCamera is null)
            {
                FlashModeText = string.Empty;
            }
            else
            {
                FlashModeText = $"{(SelectedCamera.IsFlashSupported ? $"Flash mode: {FlashMode}" : "Flash not supported")}";
            }
        }

        void UpdateCurrentZoomText()
        {
            CurrentZoomText = $"Current Zoom: {CurrentZoom}";
        }

        void UpdateResolutionText()
        {
            ResolutionText = $"Selected Resolution: {SelectedResolution.Width} x {SelectedResolution.Height}";
        }

        void HandleAvailableCamerasChanged(object? sender, IReadOnlyList<CameraInfo>? e)
        {
            OnPropertyChanged(nameof(Cameras));
        }
    }
}
