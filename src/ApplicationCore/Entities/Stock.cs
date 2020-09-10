using System;

namespace ApplicationCore.Entities
{
    public class Stock : BaseEntity
    {
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public DateTime TodaysDate { get; set; }
        public decimal LatestPrice { get; set; }
        public DateTime LatestDate { get; set; }
        public decimal Open { get; set; }
        public DateTime OpenTime { get; set; }
        public decimal Close { get; set; }
        public DateTime CloseTime { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercentage { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public bool IsUsMarketOpen { get; set; }
    }
}


