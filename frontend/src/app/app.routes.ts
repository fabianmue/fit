import { Routes } from '@angular/router';

import { CompaniesDashboardComponent } from '@app/companies/companies-dashboard/companies-dashboard.component';
import { CharacteristicsDashboardComponent } from '@app/characteristics/characteristics-dashboard/characteristics-dashboard.component';

export const routes: Routes = [
  { path: '', redirectTo: '/companies-dashboard', pathMatch: 'full' },
  { path: 'companies-dashboard', component: CompaniesDashboardComponent },
  {
    path: 'characteristics-dashboard',
    component: CharacteristicsDashboardComponent,
  },
];
