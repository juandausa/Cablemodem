using Infraestructura;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace WebAPI.Helpers
{
    public class AppSettings : IAppSettings
    {
        protected IConfiguration Config;

        public AppSettings(IConfiguration config)
        {
            this.Config = config;
        }

        #region App

        public string JsonStorageFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.JsonStorageDirectory, Config["AppSettings:JsonStorageFilePath"]);
        public string SqlSeedDirectory => Config["AppSettings:SqlSeedDirectory"];
        public string SqlSeedFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.SqlSeedDirectory, Config["AppSettings:SqlSeedFilePath"]);
        public string JsonStorageDirectory => Config["AppSettings:JsonStorageDirectory"];

        #endregion App
    }
}
