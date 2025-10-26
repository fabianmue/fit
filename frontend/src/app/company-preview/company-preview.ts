import { Component, input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';

import { CompanyDto } from '../api/models';

@Component({
    selector: 'app-company-preview',
    imports: [MatCardModule],
    templateUrl: './company-preview.html',
    styleUrl: './company-preview.scss',
})
export class CompanyPreview {
    readonly company = input.required<CompanyDto>();
}
