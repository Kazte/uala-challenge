using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extension
{
    public static class DateTimeExtensions
    {
        public static DateTime? SetKindUtc(this DateTime? dateTime)
        {
            if (dateTime is null)
            {
                return null;
            }

            return dateTime.Value.SetKindUtc();
        }

        public static DateTime SetKindUtc(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime;
            }

            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }
    }
}
