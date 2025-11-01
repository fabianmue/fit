import { Component, inject, signal, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { Api } from '../api/api';
import { apiCompaniesGet$Json } from '../api/functions';
import { CompanyDto } from '../api/models';
import { CompanyPreviewCard } from '../company-preview-card/company-preview-card';

@Component({
    selector: 'app-company-overview',
    imports: [CompanyPreviewCard, MatCardModule],
    templateUrl: './company-overview.html',
    styleUrl: './company-overview.scss',
})
export class CompanyOverview implements OnInit {
    readonly companies = signal<CompanyDto[] | null>(null);

    private readonly api = inject(Api);

    async ngOnInit() {
        this.companies.set(await this.api.invoke(apiCompaniesGet$Json));
    }
}
