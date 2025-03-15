import {
  ApplicationConfig,
  importProvidersFrom,
  LOCALE_ID,
  provideZoneChangeDetection,
} from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';

import { routes } from '@app/app.routes';
import { ApiModule } from '@app/api/api.module';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(),
    importProvidersFrom([ApiModule.forRoot({ rootUrl: '/api' })]),
    { provide: LOCALE_ID, useValue: 'de-CH' },
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: { width: '650px', minWidth: '650px' },
    },
  ],
};
