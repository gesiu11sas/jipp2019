﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KonwerterZSQLiAZUREiPLUGIN.Model;

namespace KonwerterZSQLiAZUREiPLUGIN
{
    public class StatystykiAZURE : RepozytoriumStatystyk
    {
        private CloudTable table;

        public StatystykiAZURE()
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(ConfigurationManager.AppSettings["StorageName"], ConfigurationManager.AppSettings["StorageKey"]), true);
            CloudTableClient client = storageAccount.CreateCloudTableClient();
            this.table = client.GetTableReference("StatystykiDamianlukasiewicz");
            this.table.CreateIfNotExists();
        }

        public void AddStatistic(PolaDataGrid statistic)
        {
            StatisticsEntity entity = new StatisticsEntity();
            entity.PartitionKey = string.Empty; // computer name;
            entity.RowKey = Guid.NewGuid().ToString();
            entity.Type = statistic.LogKonwersji;
            entity.DateTime = statistic.DataGenerowania;

            TableOperation insertOperation = TableOperation.Insert(entity);

            table.Execute(insertOperation);
        }

        public IEnumerable<PolaDataGrid> GetStatistics()
        {
            TableQuery<StatisticsEntity> query = new TableQuery<StatisticsEntity>();

            return table.ExecuteQuery(query).Select(obj => new PolaDataGrid() { DataGenerowania = obj.DateTime, LogKonwersji = obj.Type }).ToList();

        }
    }
}
