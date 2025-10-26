import { DatePipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';

import appInfo from '../version.json';

@Component({
    selector: 'app-root',
    imports: [DatePipe, MatToolbarModule, RouterModule],
    templateUrl: './app.html',
    styleUrl: './app.scss',
})
export class App implements OnInit {
    appInfo = appInfo;

    private readonly matIconRegistry = inject(MatIconRegistry);

    ngOnInit(): void {
        this.matIconRegistry.setDefaultFontSetClass('material-symbols-outlined');
    }
}
