namespace Davantes_SODV1255_LMS_Assignment3.ViewModels {
    public class DashboardViewModel {
        public string? UserFullName { get; set; }

        public int TotalBooks { get; set; }
        public int AvailableBooks { get; set; }
        public int BorrowedBooks { get; set; }

        public int TotalReaders { get; set; }

        public int ActiveBorrowings { get; set; }
        public int OverdueBorrowings { get; set; }

        public DashboardViewModel() {
        }
    }
}
