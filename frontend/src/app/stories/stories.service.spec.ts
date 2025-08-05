import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { StoriesService } from './stories.service';

describe('StoriesService', () => {
  let service: StoriesService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(StoriesService);
    http = TestBed.inject(HttpTestingController);
  });

  it('should call backend with query params', () => {
    service.getStories(1, 20, 'test').subscribe();
    const req = http.expectOne(r => r.method === 'GET' && r.url === '/api/stories');
    expect(req.request.params.get('page')).toBe('1');
    expect(req.request.params.get('pageSize')).toBe('20');
    expect(req.request.params.get('search')).toBe('test');
    req.flush({ items: [], total: 0 });
  });
});
