import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { BorrowService } from '../../../services/borrow.service';
import { AuthService } from '../../../services/auth.service';
import { BorrowRecord } from '../../../models/models';

@Component({
  selector: 'app-monitor-borrows',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './monitor-borrows.component.html',
  styleUrls: ['./monitor-borrows.component.css']
})
export class MonitorBorrowsComponent implements OnInit {
  borrows: BorrowRecord[] = [];
  loading = true;
  returning: number | null = null;
  toast: { message: string; type: 'success' | 'error' } | null = null;
  filterOverdue = false;

  constructor(private borrowService: BorrowService, public auth: AuthService) {}

  ngOnInit() { this.loadBorrows(); }

  loadBorrows() {
    this.loading = true;
    this.borrowService.getAllActive().subscribe({
      next: (b) => { this.borrows = b; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  get displayed() {
    return this.filterOverdue ? this.borrows.filter(b => b.isOverdue) : this.borrows;
  }

  get overdueCount() {
    return this.borrows.filter(b => b.isOverdue).length;
  }

  returnBook(borrow: BorrowRecord) {
    if (this.returning) return;
    this.returning = borrow.id;
    this.borrowService.return(borrow.id).subscribe({
      next: (updated) => {
        this.borrows = this.borrows.filter(b => b.id !== borrow.id);
        this.returning = null;
        const fine = updated.fine > 0 ? ` Fine: Rs.${updated.fine.toFixed(2)}` : '';
        this.showToast(`"${borrow.bookTitle}" returned.${fine}`, 'success');
      },
      error: (err) => {
        this.returning = null;
        this.showToast(err.error?.message || 'Failed.', 'error');
      }
    });
  }

  daysOverdue(dueDate: string): number {
    return Math.max(0, Math.ceil((Date.now() - new Date(dueDate).getTime()) / 86400000));
  }

  showToast(message: string, type: 'success' | 'error') {
    this.toast = { message, type };
    setTimeout(() => (this.toast = null), 4000);
  }
}
