import { Component, computed, input, Signal } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { RouterLink } from '@angular/router';

import { CompanyDto } from '../api/models';

@Component({
    selector: 'app-company-preview-card',
    imports: [MatCardModule, MatChipsModule, RouterLink],
    templateUrl: './company-preview-card.html',
    styleUrl: './company-preview-card.scss',
})
export class CompanyPreviewCard {
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
