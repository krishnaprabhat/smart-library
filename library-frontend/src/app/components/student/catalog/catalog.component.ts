import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { BookService } from '../../../services/book.service';
import { BorrowService } from '../../../services/borrow.service';
import { AuthService } from '../../../services/auth.service';
import { Book } from '../../../models/models';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  allBooks: Book[] = [];
  search = '';
  branch = '';
  selectedType = '';
  loading = true;
  toast: { message: string; type: 'success' | 'error' } | null = null;
  borrowingId: number | null = null;

  branches = ['CSE', 'ECE', 'MECH', 'CIVIL', 'EEE', 'GENERAL', 'ARTS'];
  types = ['Textbook', 'Reference', 'Novel', 'Journal', 'Magazine'];

  constructor(
    private bookService: BookService,
    private borrowService: BorrowService,
    public auth: AuthService
  ) {}

  ngOnInit() { this.loadBooks(); }

  loadBooks() {
    this.loading = true;
    this.bookService.getAll(this.search || undefined, this.branch || undefined).subscribe({
      next: (books) => { this.allBooks = books; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  borrow(book: Book) {
    if (this.borrowingId) return;
    this.borrowingId = book.id;
    this.borrowService.borrow(book.id).subscribe({
      next: () => {
        book.availableCopies--;
        this.borrowingId = null;
        this.showToast(`"${book.title}" borrowed successfully!`, 'success');
      },
      error: (err) => {
        this.borrowingId = null;
        this.showToast(err.error?.message || 'Failed to borrow.', 'error');
      }
    });
  }

  get regularBooks() {
    return this.allBooks.filter(b => 
      b.type !== 'Magazine' && 
      (!this.selectedType || b.type === this.selectedType)
    );
  }

  get magazines() {
    return this.allBooks.filter(b => b.type === 'Magazine');
  }

  getMagazineGradient(title: string): string {
    const name = title.toLowerCase();
    if (name.includes('wired')) return 'linear-gradient(135deg, #ff0055 0%, #0a0a0f 100%)';
    if (name.includes('national') || name.includes('geo')) return 'linear-gradient(135deg, #f59e0b 0%, #1c1917 100%)';
    if (name.includes('time')) return 'linear-gradient(135deg, #ef4444 0%, #180505 100%)';
    if (name.includes('forbes')) return 'linear-gradient(135deg, #2563eb 0%, #0f172a 100%)';
    if (name.includes('scientific') || name.includes('american')) return 'linear-gradient(135deg, #10b981 0%, #064e3b 100%)';
    if (name.includes('vogue')) return 'linear-gradient(135deg, #ec4899 0%, #31102f 100%)';
    return 'linear-gradient(135deg, #8b5cf6 0%, #1e1b4b 100%)';
  }

  showToast(message: string, type: 'success' | 'error') {
    this.toast = { message, type };
    setTimeout(() => (this.toast = null), 3500);
  }

  onSearchChange() { this.loadBooks(); }
  onBranchChange() { this.loadBooks(); }
  onTypeChange() { /* Client-side filter, no load needed */ }
  clearSearch() { this.search = ''; this.branch = ''; this.selectedType = ''; this.loadBooks(); }
}
