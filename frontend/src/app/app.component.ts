import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, MatButtonModule, MatToolbarModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  constructor(private readonly titleService: Title) {
    this.titleService.setTitle('FIT Invest');
  }
}
