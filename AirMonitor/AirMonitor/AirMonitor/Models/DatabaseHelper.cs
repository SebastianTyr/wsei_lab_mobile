using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SQLite;

namespace AirMonitor.Models
{
    public class DatabaseHelper : IDisposable
    {
        private static SQLiteConnection _con;

        public DatabaseHelper()
        {
            Connect();
        }

        public void Connect()
        {
            if (_con == null)
            {
                _con = new SQLiteConnection(App.PathDB);
                _con.CreateTable<MeasurementEntity>();
            }
        }

        public void Disconnect()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_con != null)
            {
                _con.Dispose();
                _con = null;
            }
        }

        public void AddData(List<Measurement> _data)
        {
            if (_data == null) return;
            if (_con == null) throw new Exception("Connection closed");

            var _measure = new MeasurementEntity();
            _measure.Measurement = JsonConvert.SerializeObject(_data);
            _measure.Date = DateTime.Now;

            _con.Insert(_measure);
        }

        public MeasurementEntity Select()
        {
            if (_con != null)
            {
                var query = _con.Table<MeasurementEntity>();
                var ent = query.ToList();

                var measure = ent.Count() > 0 ? ent.LastOrDefault() : null;

                return measure;
            }

            throw new Exception("Connection closed");
        }

        public void Truncate()
        {
            if (_con != null)
            {
                _con.DropTable<MeasurementEntity>();
                _con.CreateTable<MeasurementEntity>();
            }
        }
    }
}
