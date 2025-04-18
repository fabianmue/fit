import { provideHttpClient, withInterceptors } from '@angular/common/http';
import {
  ApplicationConfig,
  importProvidersFrom,
  LOCALE_ID,
  provideZoneChangeDetection,
} from '@angular/core';
import { MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import {
  createInterceptorCondition,
  INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,
  IncludeBearerTokenCondition,
  includeBearerTokenInterceptor,
  provideKeycloak,
} from 'keycloak-angular';

import { ApiModule } from '@app/api/api.module';
import { routes } from '@app/app.routes';

const apiRelativePathCondition =
  createInterceptorCondition<IncludeBearerTokenCondition>({
    urlPattern: /\/api(\/.*)/i,
    httpMethods: ['POST', 'PUT', 'DELETE'],
  });

export const appConfig: ApplicationConfig = {
  providers: [
    provideKeycloak({
      config: {
        url: '/identity',
        realm: 'fit',
        clientId: 'fit-app',
      },
      initOptions: {
        onLoad: 'check-sso',
        silentCheckSsoRedirectUri:
          window.location.origin + '/silent-check-sso.html',
      },
    }),
    {
      provide: INCLUDE_BEARER_TOKEN_INTERCEPTOR_CONFIG,
      useValue: [apiRelativePathCondition],
    },
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([includeBearerTokenInterceptor])),
    importProvidersFrom([ApiModule.forRoot({ rootUrl: '/api' })]),
    { provide: LOCALE_ID, useValue: 'de-CH' },
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: { width: '650px', minWidth: '650px' },
    },
  ],
};
