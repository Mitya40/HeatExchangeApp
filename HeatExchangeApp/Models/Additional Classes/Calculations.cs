namespace HeatExchangeApp.Models
{
    public class Calculations : InitData
    {
        //public double LayerHeight { get; set; }
        //public double InitTempMaterial { get; set; }
        //public double InitTempGas { get; set; }
        //public double GasSpeed { get; set; }
        //public double AvgGasHeatCapacity { get; set; }
        //public double MaterialRate { get; set; }
        //public double MaterialHeatCapacity { get; set; }
        //public double VolCoeffHeatTransfer { get; set; }
        //public double MachineDiameter { get; set; }
        //public int Actions { get; set; }
        private double CurrentLayerHeight;
        public double CutArea() =>
            Math.PI * Math.Pow(MachineDiameter / 2, 2);
        public double FlowsCapacityRatio() =>
            MaterialRate * MaterialHeatCapacity / (GasSpeed * AvgGasHeatCapacity * CutArea());
        public double RelativeLayerHeight(double height) =>
            (height * VolCoeffHeatTransfer) / (GasSpeed * AvgGasHeatCapacity * 1000);
        public double MaxRelativeLayerHeight() =>
            (LayerHeight * VolCoeffHeatTransfer * CutArea()) / (GasSpeed * AvgGasHeatCapacity * 1000);
        private double expY(double height) =>
            1 - Math.Exp((FlowsCapacityRatio() - 1) * RelativeLayerHeight(height) / FlowsCapacityRatio());
        private double mexpY(double height) =>
            1 - FlowsCapacityRatio() * Math.Exp((FlowsCapacityRatio() - 1) * RelativeLayerHeight(height) / FlowsCapacityRatio());
        public List<Results> CalculateResults()
        {
            var results = new List<Results>();
            for (double y = 0; y <= LayerHeight; y += LayerHeight / 10)
            {
                var MaterialTheta = expY(y) / mexpY(LayerHeight);
                var GasTheta = mexpY(y) / mexpY(LayerHeight);
                var t = InitTempMaterial + (InitTempGas - InitTempMaterial) * MaterialTheta;
                var T = InitTempMaterial + (InitTempGas - InitTempMaterial) * GasTheta;

                results.Add(new Results
                {
                    HeightCoordinate = Math.Round(y,1),
                    TempMaterial = Math.Round(t),
                    TempGas = Math.Round(T),
                    TempDiff = Math.Round(t - T)
                });
            }
            return results;
        }
    }
}
