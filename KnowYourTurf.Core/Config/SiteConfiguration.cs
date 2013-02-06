﻿using System;
using System.Configuration;
using System.Web.Script.Serialization;
using CC.Core.Services;

namespace KnowYourTurf.Core.Config
{
    public class SiteConfiguration : SiteConfigurationBase
    {
        public virtual string NumberOfFieldsPerCategory { get; set; }
        public virtual string CustomerPhotosEmployeePath { get; set; }
        public virtual string CustomerPhotosFacilitiesPath { get; set; }
        public override void Initialize()
        {
            base.Initialize();
            NumberOfFieldsPerCategory = ConfigurationSettings.AppSettings["NumberOfFieldsPerCategory"];
            CustomerPhotosEmployeePath = ConfigurationSettings.AppSettings["CustomerPhotosEmployeePath"];
            CustomerPhotosFacilitiesPath = ConfigurationSettings.AppSettings["CustomerPhotosFacilitiesPath"];
        }
    }

    public static class SiteConfig
    {
            private static SiteConfiguration _config;
        public static SiteConfiguration Config
        {
            get
            {
                if (_config.CreatedDate.AddHours(2) <= DateTime.Now)
                {
                    _config.Initialize();
                }
                return _config;
            }
        }

        static SiteConfig()
        {
            _config = new SiteConfiguration();
            _config.Initialize();
        }
    }

    public class InjectableSiteConfig : IInjectableSiteConfig
    {
        public SiteConfigurationBase Settings()
        {
            return SiteConfig.Config;
        }
    }
}
