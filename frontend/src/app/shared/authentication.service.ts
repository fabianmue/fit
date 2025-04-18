import { effect, inject, Injectable } from '@angular/core';
import {
  KEYCLOAK_EVENT_SIGNAL,
  KeycloakEventType,
  ReadyArgs,
  typeEventArgs,
} from 'keycloak-angular';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private authenticated = new BehaviorSubject<boolean>(false);

  get authenticated$(): Observable<boolean> {
    return this.authenticated.asObservable();
  }

  constructor() {
    this.setupAuthenticated();
  }

  private setupAuthenticated(): void {
    const keycloakSignal = inject(KEYCLOAK_EVENT_SIGNAL);
    effect(() => {
      const keycloakEvent = keycloakSignal();

      if (keycloakEvent.type === KeycloakEventType.Ready) {
        this.authenticated.next(typeEventArgs<ReadyArgs>(keycloakEvent.args));
      }

      if (keycloakEvent.type === KeycloakEventType.AuthLogout) {
        this.authenticated.next(false);
      }
    });
  }
}
