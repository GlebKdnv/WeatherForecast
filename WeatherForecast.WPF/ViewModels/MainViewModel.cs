// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using WeatherForecast.Domain;
using WeatherForecast.Services;

namespace WeatherForecast.WPF.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private OpenMeteoApi _meteoApi;
        public MainViewModel()
        {
            LoadCommand = new DelegateCommand(async () => await OnLoad(), () => !IsLoading);
            _meteoApi = new OpenMeteoApi(new System.Net.Http.HttpClient());
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


        private async Task OnLoad()
        {
            IsLoading = true;
            try
            {
                WeatherConditions.Clear();
                //todo choose coords control
                var result = await _meteoApi.GetForecast(54.7f, 20.5f);
                await Task.Delay(2000);
                WeatherConditions.AddRange(result.Conditions);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public ObservableCollection<WeatherCondition> WeatherConditions { get; private set; }
         = new ObservableCollection<WeatherCondition>();

        public DelegateCommand LoadCommand { get; private set; }

    }
}
