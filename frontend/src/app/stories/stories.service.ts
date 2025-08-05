import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Story {
  id: number;
  title: string;
  url: string;
  by: string;
}

export interface StoriesResponse {
  items: Story[];
  total: number;
}

@Injectable({ providedIn: 'root' })
export class StoriesService {
  private http = inject(HttpClient);

  getStories(page: number, pageSize: number, search: string): Observable<StoriesResponse> {
    let params = new HttpParams().set('page', page).set('pageSize', pageSize);
    if (search) {
      params = params.set('search', search);
    }
    return this.http.get<StoriesResponse>('/api/stories', { params });
  }
}
