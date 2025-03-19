import { Routes } from '@angular/router';

import { HomeComponent } from '@app/home/home.component';
import { CompaniesDashboardComponent } from '@app/companies/companies-dashboard/companies-dashboard.component';
import { CharacteristicsDashboardComponent } from '@app/characteristics/characteristics-dashboard/characteristics-dashboard.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'companies-dashboard', component: CompaniesDashboardComponent },
  {
    path: 'characteristics-dashboard',
    component: CharacteristicsDashboardComponent,
  },
];
