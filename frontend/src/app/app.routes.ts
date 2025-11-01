import { Routes } from '@angular/router';

import { CompanyDetail } from './company-detail/company-detail';
import { CompanyOverview } from './company-overview/company-overview';

export const routes: Routes = [
    { path: 'companies', component: CompanyOverview },
    { path: 'companies/:id', component: CompanyDetail },
    { path: '**', redirectTo: 'companies' },
];
