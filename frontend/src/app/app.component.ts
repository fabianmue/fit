import { AsyncPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconRegistry } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Title } from '@angular/platform-browser';
import { RouterOutlet, RouterLink } from '@angular/router';
import Keycloak from 'keycloak-js';
import { Observable } from 'rxjs';

import { AuthenticationService } from '@app/shared/authentication.service';

@Component({
  selector: 'app-root',
  imports: [
    AsyncPipe,
    MatButtonModule,
    MatToolbarModule,
    RouterLink,
    RouterOutlet,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  authenticated$: Observable<boolean>;

  constructor(
    private readonly authenticationService: AuthenticationService,
    private readonly iconRegistry: MatIconRegistry,
    private readonly keycloak: Keycloak,
    private readonly titleService: Title
  ) {
    this.titleService.setTitle('FIT Invest');
    this.authenticated$ = this.authenticationService.authenticated$;
  }

  ngOnInit(): void {
    this.iconRegistry.setDefaultFontSetClass('material-symbols-outlined');
  }

  login(): void {
    this.keycloak.login();
  }

  logout(): void {
    this.keycloak.logout();
  }
}
