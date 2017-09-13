﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NancyTodo.Core.Models;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;

namespace NancyTodo.Core.Repositories.Impl
{
    public class CsvToDoItemRepository : ITodoItemRepository
    {
        public const string CsvSourcePath = ".\\data\\todoitems.csv";

        private void EnsureCsvFile()
        {
            if (!File.Exists(CsvSourcePath))
                File.CreateText(CsvSourcePath);
        }

        private CsvReader GetReader()
        {
            this.EnsureCsvFile();

            return new CsvReader(
                new StreamReader(CsvSourcePath),
                new Configuration
                {
                    HasHeaderRecord = false,
                });
        }

        private CsvWriter GetWriter()
        {
            this.EnsureCsvFile();

            return new CsvWriter(
                new StreamWriter(CsvSourcePath, false),
                new Configuration
                {
                    HasHeaderRecord = false,
                });
        }

        public IList<TodoItem> GetAll()
        {
            using (var reader = this.GetReader())
            {
                return reader.GetRecords<TodoItem>()
                    .ToList();
            }
        }

        public IList<TodoItem> GetActive()
        {
            return this.GetAll()
                .Where(i => !i.IsCompleted)
                .ToList();
        }

        public TodoItem Get(string id)
        {
            using (var reader = this.GetReader())
            {
                return reader.GetRecords<TodoItem>()
                    .FirstOrDefault(r => r.Id == id);
            }
        }

        public TodoItem Add(TodoItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            item.Id = Guid.NewGuid().ToString();

            var items = this.GetAll();
            items.Add(item);

            using (var writer = this.GetWriter())
            {
                writer.WriteRecords(items);
                writer.Flush();
            }

            return item;
        }

        public TodoItem Complete(string id)
        {
            var items = this.GetAll();
            var item = items.FirstOrDefault(i => i.Id == id);

            if (item != null)
            {
                item.IsCompleted = true;
                using (var writer = this.GetWriter())
                {
                    writer.WriteRecords(items);
                    writer.Flush();
                }
            }

            return item;
        }
    }
}
