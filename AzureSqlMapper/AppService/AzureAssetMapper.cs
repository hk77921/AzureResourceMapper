using AzureSqlMapper.Models;
using AzureSqlMapper.Repository;
using AzureSqlMapper.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace AzureSqlMapperRespository.AppService
{
    public class AzureAssetMapper
    {
        private readonly IAzureAssets _azureasset;
        private readonly ICollectAzureAssests _collectAzureAssests;
        private readonly SqlRepository _sqlRepository;
        private readonly IConfiguration _config;

        public AzureAssetMapper(IAzureAssets azureasset, ICollectAzureAssests collectAzureAssests, SqlRepository sqlRepository, IConfiguration config)
        {
            _azureasset = azureasset;
            _collectAzureAssests = collectAzureAssests;
           _sqlRepository = sqlRepository;
            _config = config;


        }

        public void Run()
        {
           
            Log.Logger.Information("Starting to fetch SQL Instances");

            var _sqlservers = _azureasset.GetSqlServers();


            Log.Logger.Information("Starting to fetch SQL properties");
            var data = _collectAzureAssests.GetSqlProps(_sqlservers);


            Log.Logger.Information("Saving the data");
            _sqlRepository.InsertRecords<AzureSqlProps>("sqltable", data);
            Log.Logger.Information("Data saved!!");

        }
    }
}
