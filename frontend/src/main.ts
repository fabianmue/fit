/// <reference types="@angular/localize" />

import { registerLocaleData } from '@angular/common';
import localeDeCh from '@angular/common/locales/de-CH';
import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

registerLocaleData(localeDeCh, 'de-CH');

bootstrapApplication(AppComponent, appConfig).catch((err) =>
  console.error(err)
);
