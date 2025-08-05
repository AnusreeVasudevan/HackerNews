import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { StoriesComponent } from './stories.component';
import { StoriesService } from './stories.service';

describe('StoriesComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StoriesComponent],
      providers: [
        {
          provide: StoriesService,
          useValue: {
            getStories: () => of({ items: [{ id: 1, title: 'Test', url: 'http://test', by: 'me' }], total: 1 })
          }
        }
      ]
    }).compileComponents();
  });

  it('should render stories', () => {
    const fixture = TestBed.createComponent(StoriesComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('li a')?.textContent).toContain('Test');
  });
});
