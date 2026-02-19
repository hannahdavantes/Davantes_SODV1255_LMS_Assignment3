using Davantes_SODV1255_LMS_Assignment3.Models;

namespace Davantes_SODV1255_LMS_Assignment3.Repositories {
    public class BorrowingRepository {

        private static List<Borrowing> _borrowings = new List<Borrowing>() {
            new Borrowing(1, 2, 1),
            new Borrowing(2, 5, 1),
            new Borrowing(3, 7, 2),
            new Borrowing(4, 9, 3),
            new Borrowing(5, 1, 3),
        };
        static BorrowingRepository() {

            var overdueBorrowing = _borrowings.FirstOrDefault(b => b.ID == 4);

            if (overdueBorrowing != null) {
                overdueBorrowing.BorrowingDate = DateTime.Now.AddDays(-20);
                overdueBorrowing.DueDate = overdueBorrowing.BorrowingDate.AddDays(14);
                overdueBorrowing.Status = "Overdue";
            }
        }

        public static List<Borrowing> GetBorrowingList() {
            return _borrowings;
        }

        public static Borrowing GetBorrowingById(int id) {
            return _borrowings.FirstOrDefault(b => b.ID == id);
        }

        public static bool AddBorrowing(Borrowing borrowing) {

            var book = BookRepository.GetBookById(borrowing.BookID);
            var reader = ReaderRepository.GetReaderById(borrowing.ReaderID);

            if (book == null || reader == null) return false;
            if (!book.IsAvailable) return false;

            var nextID = (_borrowings.Count == 0) ? 1 : _borrowings.Max(b => b.ID) + 1;
            borrowing.ID = nextID;

            _borrowings.Add(borrowing);

            book.IsAvailable = false;

            return true;
        }

        public static bool ReturnBook(int borrowingId) {

            var borrowing = GetBorrowingById(borrowingId);
            if (borrowing == null) return false;

            if (borrowing.Status != "Borrowed" && borrowing.Status != "Overdue")
                return false;

            borrowing.ReturnDate = DateTime.Now;
            borrowing.Status = "Returned";

            var book = BookRepository.GetBookById(borrowing.BookID);
            if (book != null) {
                book.IsAvailable = true;
            }

            return true;
        }

        public static bool ExtendBorrowing(int borrowingId) {

            var borrowing = GetBorrowingById(borrowingId);
            if (borrowing == null) return false;

            if (borrowing.Status != "Borrowed" && borrowing.Status != "Overdue")
                return false;

            borrowing.DueDate = borrowing.DueDate.AddDays(7);

            return true;
        }
    }
}
