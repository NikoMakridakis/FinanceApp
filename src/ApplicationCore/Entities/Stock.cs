using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Stock : BaseEntity
    {
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public DateTime TodaysDate { get; set; }

        [Column(TypeName = "decimal(19,4)")]
        public decimal LatestPrice { get; set; }
        public DateTime LatestDate { get; set; }
        public double Open { get; set; }
        public DateTime OpenTime { get; set; }
        public double Close { get; set; }
        public DateTime CloseTime { get; set; }
        public double PreviousClose { get; set; }
        public double Change { get; set; }
        public double ChangePercentage { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public bool IsUsMarketOpen { get; set; }
    }
}


