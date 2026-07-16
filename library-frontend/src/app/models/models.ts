export interface AuthResponse {
  token: string;
  name: string;
  role: string;
  userId: number;
}

export interface Book {
  id: number;
  title: string;
  author: string;
  branch: string;
  type: string;
  isbn: string;
  category: string;
  publisher: string;
  description: string;
  totalCopies: number;
  availableCopies: number;
}

export interface BorrowRecord {
  id: number;
  bookId: number;
  bookTitle: string;
  bookAuthor: string;
  studentName: string;
  studentId: string;
  borrowDate: string;
  dueDate: string;
  returnDate: string | null;
  fine: number;
  isReturned: boolean;
  isOverdue: boolean;
}

export interface Reservation {
  id: number;
  bookId: number;
  bookTitle: string;
  bookAuthor: string;
  studentName: string;
  studentId: string;
  reservedAt: string;
  expiresAt: string;
  isFulfilled: boolean;
  isCancelled: boolean;
}

export interface User {
  id: number;
  name: string;
  studentId: string;
  role: string;
  phone: string;
  department: string;
  createdAt: string;
  borrowCount: number;
}

export interface DashboardStats {
  totalBooks: number;
  totalCopies: number;
  availableCopies: number;
  borrowedCopies: number;
  activeBorrows: number;
  overdueBorrows: number;
  totalUsers: number;
  totalFinesCollected: number;
  totalFinesPending: number;
  activeReservations: number;
}
