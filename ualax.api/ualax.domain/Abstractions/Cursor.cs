using System.Text;
using System.Text.Json;
using ualax.shared.Extensions;

namespace ualax.domain.Abstractions
{
    public class Cursor
    {
        public DateTime Date { get; set; }
        public int Id { get; set; }

        /// <summary>
        /// Create a cursor from a date and an id
        /// </summary>
        /// <param name="date"></param>
        /// <param name="id"></param>
        /// <returns>
        /// Returns a base64 encoded cursor
        /// </returns>
        public static string ToCursor(DateTime date, int id)
        {

            var newCursor = new Cursor
            {
                Date = date.SetKindUtc(),
                Id = id
            };

            var json = JsonSerializer.Serialize(newCursor);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// Create a cursor from a base64 encoded string
        /// </summary>
        /// <returns>
        /// Returns a cursor
        /// </returns>
        public static Cursor? FromCursor(string cursor)
        {
            if (string.IsNullOrWhiteSpace(cursor))
            {
                return null;
            }

            try
            {
                var json = Encoding.UTF8.GetString(Convert.FromBase64String(cursor));
                return JsonSerializer.Deserialize<Cursor>(json);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
