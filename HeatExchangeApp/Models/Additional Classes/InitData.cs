using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeatExchangeApp.Models
{
    public class InitData
    {
        [Key]
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public double LayerHeight {  get; set; }
        public double InitTempMaterial {  get; set; }
        public double InitTempGas { get; set; }
        public double GasSpeed { get; set; }
        public double AvgGasHeatCapacity { get; set; }
        public double MaterialRate { get; set; }
        public double MaterialHeatCapacity { get; set; }
        public double VolCoeffHeatTransfer { get; set; }
        public double MachineDiameter { get; set; }
        [NotMapped]
        public int Actions { get; set; }
    }
}
