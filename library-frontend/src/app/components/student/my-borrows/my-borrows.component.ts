import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BorrowService } from '../../../services/borrow.service';
import { AuthService } from '../../../services/auth.service';
import { BorrowRecord } from '../../../models/models';

@Component({
  selector: 'app-my-borrows',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './my-borrows.component.html',
  styleUrls: ['./my-borrows.component.css']
})
export class MyBorrowsComponent implements OnInit {
  borrows: BorrowRecord[] = [];
  loading = true;
  returning: number | null = null;
  toast: { message: string; type: 'success' | 'error' } | null = null;

  constructor(private borrowService: BorrowService, public auth: AuthService) {}

  ngOnInit() { this.loadBorrows(); }

  loadBorrows() {
    this.loading = true;
    this.borrowService.getMyBorrows().subscribe({
      next: (b) => { this.borrows = b; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  returnBook(borrow: BorrowRecord) {
    if (this.returning) return;
    this.returning = borrow.id;
    this.borrowService.return(borrow.id).subscribe({
      next: (updated) => {
        const idx = this.borrows.findIndex(b => b.id === borrow.id);
        if (idx !== -1) this.borrows[idx] = updated;
        this.returning = null;
        const fine = updated.fine > 0 ? ` Fine: Rs.${updated.fine.toFixed(2)}` : '';
        this.showToast(`"${updated.bookTitle}" returned successfully!${fine}`, 'success');
      },
      error: (err) => {
        this.returning = null;
        this.showToast(err.error?.message || 'Return failed.', 'error');
      }
    });
  }

  daysLabel(dueDate: string): string {
    const diff = Math.ceil((new Date(dueDate).getTime() - Date.now()) / 86400000);
    if (diff < 0) return `${Math.abs(diff)} days overdue`;
    if (diff === 0) return 'Due today';
    return `${diff} days remaining`;
  }

  get activeBorrows() { return this.borrows.filter(b => !b.isReturned); }
  get returnedBorrows() { return this.borrows.filter(b => b.isReturned); }

  showToast(message: string, type: 'success' | 'error') {
    this.toast = { message, type };
    setTimeout(() => (this.toast = null), 4000);
  }
}
