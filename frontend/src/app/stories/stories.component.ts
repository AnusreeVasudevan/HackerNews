import { Component, effect, inject, signal } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StoriesService, Story } from './stories.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-stories',
  standalone: true,
  imports: [NgFor, NgIf, FormsModule],
  templateUrl: './stories.component.html',
  styleUrl: './stories.component.scss'
})
export class StoriesComponent {
  private service = inject(StoriesService);
  private subscription: Subscription | undefined;

  readonly page = signal(1);
  readonly search = signal('');
  readonly stories = signal<Story[]>([]);
  readonly total = signal(0);
  readonly pageSize = 20;

  constructor() {
    effect(() => {
      const page = this.page();
      const search = this.search();
      this.subscription?.unsubscribe();
      this.subscription = this.service
        .getStories(page, this.pageSize, search)
        .subscribe(res => {
          this.stories.set(res.items);
          this.total.set(res.total);
        });
    });
  }

  onSearch(event: Event) {
    const value = (event.target as HTMLInputElement).value;
    this.search.set(value);
    this.page.set(1);
  }

  nextPage() {
    if (this.page() * this.pageSize < this.total()) {
      this.page.update(p => p + 1);
    }
  }

  prevPage() {
    if (this.page() > 1) {
      this.page.update(p => p - 1);
    }
  }
}
