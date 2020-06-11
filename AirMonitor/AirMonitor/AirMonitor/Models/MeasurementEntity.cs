using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Essentials;

namespace AirMonitor.Models
{
    public class MeasurementEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        public string Measurement { get; set; }
        public DateTime Date { get; set; }
    }
}
