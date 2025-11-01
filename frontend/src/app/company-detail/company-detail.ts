import { DatePipe } from '@angular/common';
import { Component, computed, inject, OnInit, Signal, signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute } from '@angular/router';

import { Api } from '../api/api';
import { apiCompaniesCompanyIdGet$Json } from '../api/functions';
import { CompanyDto } from '../api/models';

const multiplierTexts: { [key: number]: string } = {
    1000: 'Thousands',
    1000000: 'Millions',
};

@Component({
    selector: 'app-company-detail',
    imports: [DatePipe, MatCardModule],
    templateUrl: './company-detail.html',
    styleUrl: './company-detail.scss',
})
export class CompanyDetail implements OnInit {
    readonly company = signal<CompanyDto | null>(null);
    readonly reportingMultiplier: Signal<string> = computed(() => {
        if (this.company() === null || !this.company()!.reportingMultiplier) {
            return '';
        }

        return multiplierTexts[this.company()!.reportingMultiplier!] || '';
    });

    private readonly api = inject(Api);
    private readonly route = inject(ActivatedRoute);

    async ngOnInit() {
        const companyId = Number(this.route.snapshot.paramMap.get('id'));
        this.company.set(await this.api.invoke(apiCompaniesCompanyIdGet$Json, { companyId }));
    }
}
