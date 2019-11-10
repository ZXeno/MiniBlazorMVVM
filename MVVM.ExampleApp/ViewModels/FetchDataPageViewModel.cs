namespace MVVM.ExampleApp.ViewModels
{
    using MBMVVM;
    using MVVM.ExampleApp.Data;
    using System;
    using System.Threading.Tasks;

    public class FetchDataPageViewModel : ViewModelBase
    {
        private readonly WeatherForecastService weatherService;

        private WeatherForecast[] forecasts;

        public FetchDataPageViewModel(WeatherForecastService weatherService)
        {
            this.weatherService = weatherService;
        }

        public WeatherForecast[] Forecasts
        {
            get => forecasts;
            set
            {
                this.forecasts = value;
                this.OnPropertyChanged();
            }
        }

        public async Task GetForecastAsync(DateTime startDate)
        {
            this.Forecasts = await weatherService.GetForecastAsync(startDate);
        }
    }
}
