import { Component, inject, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { Api } from '../api/api';
import { apiCompaniesGet$Json } from '../api/functions';
import { CompanyDto } from '../api/models';
import { CompanyPreview } from '../company-preview/company-preview';

@Component({
    selector: 'app-company-overview',
    imports: [CompanyPreview, MatCardModule],
    templateUrl: './company-overview.html',
    styleUrl: './company-overview.scss',
})
export class CompanyOverview {
    readonly companies = signal<CompanyDto[] | null>(null);

    private readonly api = inject(Api);

    async ngOnInit() {
        this.companies.set(await this.api.invoke(apiCompaniesGet$Json));
    }
}
