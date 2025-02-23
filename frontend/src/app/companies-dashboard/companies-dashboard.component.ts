import { Component, OnInit, viewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { tap } from 'rxjs';

import { CompanyPanelContentComponent } from '../company-panel-content/company-panel-content.component';
import { CompanyCreateDialogComponent } from '../company-create-dialog/company-create-dialog.component';
import { CompanyUpdateDialogComponent } from '../company-update-dialog/company-update-dialog.component';
import { CompanyCharacteristicCreateDialogComponent } from '../company-characteristic-create-dialog/company-characteristic-create-dialog.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import {
  CharacteristicsService,
  CompaniesService,
  CompanyCharacteristicsService,
} from '../api/services';
import { CompanyReadDto } from '../api/models/company-read-dto';
import { CompanyCreateDto } from '../api/models/company-create-dto';
import { CompanyUpdateDto } from '../api/models/company-update-dto';
import { CharacteristicReadDto } from '../api/models/characteristic-read-dto';
import { CompanyCharacteristicReadDto } from '../api/models/company-characteristic-read-dto';
import { CompanyCharacteristicCreateDto } from '../api/models/company-characteristic-create-dto';
import { CompanyCharacteristicUpdateDto } from '../api/models/company-characteristic-update-dto';

@Component({
  selector: 'app-companies-dashboard',
  imports: [
    MatButtonModule,
    MatExpansionModule,
    MatIconModule,
    CompanyPanelContentComponent,
  ],
  templateUrl: './companies-dashboard.component.html',
  styleUrl: './companies-dashboard.component.scss',
})
export class CompaniesDashboardComponent implements OnInit {
  accordion = viewChild.required(MatAccordion);
  companies = new Array<CompanyReadDto>();
  characteristics = new Array<CharacteristicReadDto>();

  constructor(
    private readonly companiesService: CompaniesService,
    private readonly characteristicsService: CharacteristicsService,
    private readonly companyCharacteristicsService: CompanyCharacteristicsService,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.readCompanies();
    this.readCharacteristics();
  }

  openCreateCompanyDialog(): void {
    const dialogRef = this.dialog.open(CompanyCreateDialogComponent, {
      width: '600px',
    });

    dialogRef
      .afterClosed()
      .subscribe((dialogResult: CompanyCreateDto | null) => {
        if (dialogResult) {
          this.createCompany(dialogResult);
        }
      });
  }

  openUpdateCompanyDialog(company: CompanyReadDto): void {
    const dialogRef = this.dialog.open(CompanyUpdateDialogComponent, {
      width: '600px',
      data: { company },
    });

    dialogRef
      .afterClosed()
      .subscribe((dialogResult: CompanyUpdateDto | null) => {
        if (dialogResult) {
          this.updateCompany(company.id!, dialogResult);
        }
      });
  }

  openConfirmDeleteCompanyDialog(company: CompanyReadDto): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '600px',
      data: {
        title: `Delete company '${company.name}' ?`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((dialogResult: boolean | null) => {
      if (dialogResult) {
        this.deleteCompany(company.id!);
      }
    });
  }

  openCreateCompanyCharacteristicDialog(company: CompanyReadDto): void {
    const characteristicOptions = this.characteristics.filter(
      (characteristic) =>
        company.companyCharacteristics!.find(
          (companyCharacteristic) =>
            companyCharacteristic.characteristicId === characteristic.id
        ) === undefined
    );

    const dialogRef = this.dialog.open(
      CompanyCharacteristicCreateDialogComponent,
      {
        width: '600px',
        data: { company, characteristicOptions },
      }
    );

    dialogRef
      .afterClosed()
      .subscribe((dialogResult: CompanyCharacteristicCreateDto | null) => {
        if (dialogResult) {
          this.createCompanyCharacteristics(dialogResult);
        }
      });
  }

  onUpdateCompanyCharacteristic(
    companyCharacteristicId: string,
    companyCharacteristic: CompanyCharacteristicUpdateDto
  ): void {
    this.updateCompanyCharacteristic(
      companyCharacteristicId,
      companyCharacteristic
    );
  }

  openConfirmDeleteCompanyCharacteristicDialog(
    company: CompanyReadDto,
    companyCharacteristic: CompanyCharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '600px',
      data: {
        title: `Delete characteristic ${companyCharacteristic.label} of company '${company.name}' ?`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((dialogResult: boolean | null) => {
      if (dialogResult) {
        this.deleteCompanyCharacteristics(companyCharacteristic.id!);
      }
    });
  }

  private readCompanies(): void {
    this.companiesService
      .companiesGet()
      .subscribe((companies) => (this.companies = companies));
  }

  private createCompany(company: CompanyCreateDto): void {
    this.companiesService
      .companiesPost({ body: company })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private updateCompany(companyId: string, company: CompanyUpdateDto): void {
    this.companiesService
      .companiesIdPut({ id: companyId, body: company })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private deleteCompany(companyId: string): void {
    this.companiesService
      .companiesIdDelete({ id: companyId })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private readCharacteristics(): void {
    this.characteristicsService
      .characteristicsGet()
      .subscribe((characteristics) => (this.characteristics = characteristics));
  }

  private createCompanyCharacteristics(
    companyCharacteristic: CompanyCharacteristicCreateDto
  ): void {
    this.companyCharacteristicsService
      .companyCharacteristicsPost({ body: companyCharacteristic })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private updateCompanyCharacteristic(
    companyCharacteristicId: string,
    companyCharacteristic: CompanyCharacteristicUpdateDto
  ): void {
    this.companyCharacteristicsService
      .companyCharacteristicsIdPut({
        id: companyCharacteristicId,
        body: companyCharacteristic,
      })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private deleteCompanyCharacteristics(companyCharacteristicId: string): void {
    this.companyCharacteristicsService
      .companyCharacteristicsIdDelete({ id: companyCharacteristicId })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }
}
