using System;

namespace Launcher.Controls.Update
{
    public static class SpeedUnitsExtension
    {
        private const long Kilo = 1024;

        public static SpeedUnits GetUnit(long value)
        {
            try
            {
                if (value >= Math.Pow(Kilo, (long)SpeedUnits.GBit))
                    return SpeedUnits.GBit;
                return value >= Math.Pow(Kilo, (long)SpeedUnits.MBit) ? SpeedUnits.MBit : SpeedUnits.KBit;
            }
            catch
            {
                return SpeedUnits.Bit;
            }
        }

        public static string GetUnitAbbr(long value)
        {
            return GetUnitAbbr(GetUnit(value));
        }

        public static string GetUnitAbbr(SpeedUnits value)
        {
            switch (value)
            {
                case SpeedUnits.Bit:
                    return "Б";
                case SpeedUnits.KBit:
                    return "КБ";
                case SpeedUnits.MBit:
                    return "МБ";
                case SpeedUnits.GBit:
                    return "ГБ";
                default:
                    return "unknown";
            }
        }

        public static string GetFormattedSize(long value, string format)
        {
            double dValue = value;
            string amount;
            var units = GetUnit(value);

            switch (units)
            {
                case SpeedUnits.Bit:
                    amount = string.Format(format, dValue);
                    break;
                case SpeedUnits.KBit:
                    amount = string.Format(format, dValue / Kilo);
                    break;
                default:
                    amount = string.Format(format, dValue / Math.Pow(Kilo, (long)units));
                    break;
            }

            return amount;
        }
    }
}
