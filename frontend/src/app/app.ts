import { Component } from '@angular/core';
import { StoriesComponent } from './stories/stories.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [StoriesComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {}
