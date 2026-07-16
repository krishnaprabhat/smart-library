import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { BookService } from '../../../services/book.service';
import { AuthService } from '../../../services/auth.service';
import { Book } from '../../../models/models';

@Component({
  selector: 'app-manage-books',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './manage-books.component.html',
  styleUrls: ['./manage-books.component.css']
})
export class ManageBooksComponent implements OnInit {
  books: Book[] = [];
  filteredBooks: Book[] = [];
  search = '';
  branch = '';
  loading = true;
  showModal = false;
  isEdit = false;
  saving = false;
  toast: { message: string; type: 'success' | 'error' } | null = null;

  branches = ['CSE', 'ECE', 'MECH', 'CIVIL', 'EEE', 'GENERAL', 'ARTS'];
  types = ['Textbook', 'Reference', 'Novel', 'Journal', 'Magazine'];

  form: Partial<Book> & { id?: number } = this.emptyForm();

  constructor(private bookService: BookService, public auth: AuthService) {}

  ngOnInit() { this.loadBooks(); }

  emptyForm() {
    return { title: '', author: '', branch: 'CSE', type: 'Textbook', isbn: '', category: '', publisher: '', description: '', totalCopies: 1, availableCopies: 1 };
  }

  loadBooks() {
    this.loading = true;
    this.bookService.getAll().subscribe({
      next: (b) => { this.books = b; this.applyFilter(); this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  applyFilter() {
    this.filteredBooks = this.books.filter(b =>
      (!this.search || b.title.toLowerCase().includes(this.search.toLowerCase()) || b.author.toLowerCase().includes(this.search.toLowerCase())) &&
      (!this.branch || b.branch === this.branch)
    );
  }

  openAdd() {
    this.form = this.emptyForm();
    this.isEdit = false;
    this.showModal = true;
  }

  openEdit(book: Book) {
    this.form = { ...book };
    this.isEdit = true;
    this.showModal = true;
  }

  save() {
    this.saving = true;
    const obs = this.isEdit
      ? this.bookService.update(this.form.id!, this.form)
      : this.bookService.create(this.form);
    obs.subscribe({
      next: () => {
        this.showModal = false;
        this.saving = false;
        this.loadBooks();
        this.showToast(this.isEdit ? 'Book updated.' : 'Book added.', 'success');
      },
      error: () => { this.saving = false; this.showToast('Failed to save.', 'error'); }
    });
  }

  delete(book: Book) {
    if (!confirm(`Delete "${book.title}"?`)) return;
    this.bookService.delete(book.id).subscribe({
      next: () => { this.loadBooks(); this.showToast('Book deleted.', 'success'); },
      error: () => { this.showToast('Failed to delete.', 'error'); }
    });
  }

  showToast(message: string, type: 'success' | 'error') {
    this.toast = { message, type };
    setTimeout(() => (this.toast = null), 3500);
  }
}
