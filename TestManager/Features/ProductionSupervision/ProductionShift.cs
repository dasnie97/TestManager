namespace TestManager.Features.ProductionSupervision;

public struct ProductionShift
{
    public string Description { get; private set; }
    public DateTime ShiftStart { get; private set; }
    public DateTime ShiftEnd { get; private set; }

    public ProductionShift()
    {
        Description = string.Empty;
        ShiftStart = DateTime.MinValue;
        ShiftEnd = DateTime.MinValue;
    }

    public ProductionShift(string shift, DateTime shiftStart, DateTime shiftEnd)
    {
        Description = shift;
        ShiftStart = shiftStart;
        ShiftEnd = shiftEnd;
    }

    public static ProductionShift CurrentShift
    {
        get
        {
            var y = DateTime.Now.Year;
            var m = DateTime.Now.Month;
            var d = DateTime.Now.Day;

            var ms = 6;
            var ds = 14;
            var ns = 22;

            var morningShiftStart = TimeOnly.FromDateTime(new DateTime(y, m, d, ms, 0, 0));
            var dayShiftStart = TimeOnly.FromDateTime(new DateTime(y, m, d, ds, 0, 0));
            var nightShiftStart = TimeOnly.FromDateTime(new DateTime(y, m, d, ns, 0, 0));

            var currentTime = TimeOnly.FromDateTime(DateTime.Now);

            if (currentTime.IsBetween(morningShiftStart, dayShiftStart))
            {
                return new ProductionShift("Morning shift", new DateTime(y, m, d, ms, 0, 0), new DateTime(y, m, d, ds, 0, 0));
            }
            if (currentTime.IsBetween(dayShiftStart, nightShiftStart))
            {
                return new ProductionShift("Day shift", new DateTime(y, m, d, ds, 0, 0), new DateTime(y, m, d, ns, 0, 0));
            }
            if (currentTime.IsBetween(nightShiftStart, morningShiftStart))
            {
                return new ProductionShift("Night shift", new DateTime(y, m, d, ns, 0, 0), new DateTime(y, m, d, ms, 0, 0));
            }
            return new ProductionShift();
        }
    }
}
