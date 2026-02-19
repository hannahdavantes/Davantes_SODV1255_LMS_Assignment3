using Davantes_SODV1255_LMS_Assignment3.Models;

namespace Davantes_SODV1255_LMS_Assignment3.Repositories {
    public class ReaderRepository {

        private static List<Reader> _readers = new List<Reader>() {
            new Reader(1, "John", "Smith", "john@email.com", "4031234567"),
            new Reader(2, "Jane", "Doe", "jane@email.com", "4039876543"),
            new Reader(3, "Sophie", "Lee", "sophie@email.com", "4035557890"),
            new Reader(4, "Aiden", "Cruz", "aiden@email.com", "5871112233"),
            new Reader(5, "Isabella", "Reyes", "isabella@email.com", "5872223344"),
        };

        public static void AddReader(Reader reader) {
            var nextID = (_readers.Count == 0) ? 1 : _readers.Max(reader => reader.ID) + 1;
            reader.ID = nextID;
            _readers.Add(reader);
        }

        public static List<Reader> GetReaderList() {
            return _readers;
        }

        public static Reader GetReaderById(int id) {
            var reader = _readers.FirstOrDefault(r => r.ID == id);

            if (reader != null) {
                reader.Borrowings = BorrowingRepository
                    .GetBorrowingList()
                    .Where(b => b.ReaderID == id)
                    .ToList();
            }

            return reader;
        }

        public static void UpdateReader(Reader updatedReader) {
            var existingReader = _readers.FirstOrDefault(reader => reader.ID == updatedReader.ID);
            if (existingReader != null) {
                existingReader.FirstName = updatedReader.FirstName;
                existingReader.LastName = updatedReader.LastName;
                existingReader.Email = updatedReader.Email;
                existingReader.Phone = updatedReader.Phone;
            }
        }

        public static void DeleteReader(int id) {
            var readerToRemove = _readers.FirstOrDefault(reader => reader.ID == id);
            if (readerToRemove != null) {
                _readers.Remove(readerToRemove);
            }
        }
    }
}
