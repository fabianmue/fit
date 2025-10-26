import { Component, computed, input, Signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';

import { CompanyDto } from '../api/models';

@Component({
    selector: 'app-company-preview',
    imports: [MatCardModule, MatChipsModule],
    templateUrl: './company-preview.html',
    styleUrl: './company-preview.scss',
})
export class CompanyPreview {
    readonly company = input.required<CompanyDto>();
    readonly newReportingAvailable: Signal<boolean> = computed(
        () =>
            this.company().nextReportingDate !== null &&
            new Date(this.company().nextReportingDate!) < new Date(),
    );
    readonly lastReportingPeriod: Signal<string | null> = computed(() => {
        return null;
    });
}
