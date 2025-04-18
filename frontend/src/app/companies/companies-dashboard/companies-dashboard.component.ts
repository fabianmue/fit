import { AsyncPipe } from '@angular/common';
import { Component, OnInit, viewChild } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatAccordion, MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { Observable, tap } from 'rxjs';

import { CharacteristicReadDto } from '@app/api/models/characteristic-read-dto';
import { CompanyCreateDto } from '@app/api/models/company-create-dto';
import { CompanyUpdateDto } from '@app/api/models/company-update-dto';
import { CompanyCharacteristicCreateDto } from '@app/api/models/company-characteristic-create-dto';
import { CompanyCharacteristicReadDto } from '@app/api/models/company-characteristic-read-dto';
import { CompanyCharacteristicUpdateDto } from '@app/api/models/company-characteristic-update-dto';
import { CompanyHistoricCharacteristicCreateDto } from '@app/api/models/company-historic-characteristic-create-dto';
import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';
import { CompanyHistoricCharacteristicUpdateDto } from '@app/api/models/company-historic-characteristic-update-dto';
import { CompanyReadDto } from '@app/api/models/company-read-dto';
import { HistoricCharacteristicReadDto } from '@app/api/models/historic-characteristic-read-dto';
import {
  CharacteristicsService,
  CompaniesService,
  CompanyCharacteristicsService,
  CompanyHistoricCharacteristicsService,
  HistoricCharacteristicsService,
} from '@app/api/services';
import { CompanyCreateDialogComponent } from '@app/companies/company-create-dialog/company-create-dialog.component';
import { CompanyPanelContentComponent } from '@app/companies/company-panel-content/company-panel-content.component';
import { CompanyUpdateDialogComponent } from '@app/companies/company-update-dialog/company-update-dialog.component';
import { CompanyCharacteristicCreateDialogComponent } from '@app/company-characteristics/company-characteristic-create-dialog/company-characteristic-create-dialog.component';
import { CompanyHistoricCharacteristicCreateDialogComponent } from '@app/company-characteristics/company-historic-characteristic-create-dialog/company-historic-characteristic-create-dialog.component';
import { CompanyHistoricCharacteristicUpdateDialogComponent } from '@app/company-characteristics/company-historic-characteristic-update-dialog/company-historic-characteristic-update-dialog.component';
import { AuthenticationService } from '@app/shared/authentication.service';
import { ConfirmDialogComponent } from '@app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-companies-dashboard',
  imports: [
    AsyncPipe,
    CompanyPanelContentComponent,
    MatButtonModule,
    MatExpansionModule,
    MatIconModule,
  ],
  templateUrl: './companies-dashboard.component.html',
  styleUrl: './companies-dashboard.component.scss',
})
export class CompaniesDashboardComponent implements OnInit {
  accordion = viewChild.required(MatAccordion);
  authenticated$: Observable<boolean>;
  companies = new Array<CompanyReadDto>();
  characteristics = new Array<CharacteristicReadDto>();
  historicCharacteristics = new Array<HistoricCharacteristicReadDto>();

  constructor(
    private readonly authenticationService: AuthenticationService,
    private readonly characteristicsService: CharacteristicsService,
    private readonly companiesService: CompaniesService,
    private readonly companyCharacteristicsService: CompanyCharacteristicsService,
    private readonly companyHistoricCharacteristicsService: CompanyHistoricCharacteristicsService,
    private readonly dialog: MatDialog,
    private readonly historicCharacteristicsService: HistoricCharacteristicsService
  ) {
    this.authenticated$ = this.authenticationService.authenticated$;
  }

  ngOnInit(): void {
    this.readCompanies();
    this.readCharacteristics();
    this.readHistoricCharacteristics();
  }

  openCreateCompanyDialog(): void {
    const dialogRef = this.dialog.open(CompanyCreateDialogComponent);

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          name: string | null;
          logoUrl: string | null;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.createCompany({
          name: result.name,
          logoUrl: result.logoUrl,
        } as CompanyCreateDto);
      }
    );
  }

  openUpdateCompanyDialog(company: CompanyReadDto): void {
    const dialogRef = this.dialog.open(CompanyUpdateDialogComponent, {
      data: { name: company.name, logoUrl: company.logoUrl },
    });

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          name: string | null;
          logoUrl: string | null;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.updateCompany(company.id!, {
          name: result.name,
          logoUrl: result.logoUrl,
        } as CompanyUpdateDto);
      }
    );
  }

  openConfirmDeleteCompanyDialog(company: CompanyReadDto): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: `Delete company '${company.name}'?`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean | null) => {
      if (!result) {
        return;
      }

      this.deleteCompany(company.id!);
    });
  }

  openCreateCompanyCharacteristicDialog(company: CompanyReadDto): void {
    const characteristicOptions = this.getCompanyCharacteristicOptions(company);
    const dialogRef = this.dialog.open(
      CompanyCharacteristicCreateDialogComponent,
      { data: { company, characteristicOptions } }
    );

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          characteristicId: string | null;
          unit: string | null;
          value: number | null;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.createCompanyCharacteristics({
          characteristicId: result.characteristicId,
          companyId: company.id,
          unit: result.unit,
          value: result.value,
        } as CompanyCharacteristicCreateDto);
      }
    );
  }

  openCreateCompanyHistoricCharacteristicDialog(company: CompanyReadDto): void {
    const historicCharacteristicOptions =
      this.getCompanyHistoricCharacteristicOptions(company);
    const dialogRef = this.dialog.open(
      CompanyHistoricCharacteristicCreateDialogComponent,
      {
        maxHeight: '80vh',
        data: { company, historicCharacteristicOptions },
      }
    );

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          historicCharacteristicId: string | null;
          unit: string | null;
          values: Array<{ value: number | null; date: Date | null }>;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.createCompanyHistoricCharacteristics({
          historicCharacteristicId: result.historicCharacteristicId,
          companyId: company.id,
          unit: result.unit,
          values: result.values?.map((value) => ({
            value: value.value,
            date: value.date?.toJSON(),
          })),
        } as CompanyHistoricCharacteristicCreateDto);
      }
    );
  }

  onUpdateCompanyCharacteristic(
    companyCharacteristic: CompanyCharacteristicReadDto,
    companyCharacteristicUpdateDto: CompanyCharacteristicUpdateDto
  ): void {
    this.updateCompanyCharacteristic(
      companyCharacteristic.id!,
      companyCharacteristicUpdateDto
    );
  }

  openUpdateCompanyHistoricCharacteristicDialog(
    company: CompanyReadDto,
    companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(
      CompanyHistoricCharacteristicUpdateDialogComponent,
      {
        data: {
          company,
          companyHistoricCharacteristic,
        },
      }
    );

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          unit: string | null;
          values: Array<{ value: number | null; date: Date | null }>;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.updateCompanyHistoricCharacteristic(
          companyHistoricCharacteristic.id!,
          {
            unit: result.unit,
            values: result.values?.map((value) => ({
              value: value.value,
              date: value.date?.toJSON(),
            })),
          } as CompanyHistoricCharacteristicUpdateDto
        );
      }
    );
  }

  openConfirmDeleteCompanyCharacteristicDialog(
    company: CompanyReadDto,
    companyCharacteristic: CompanyCharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: `Delete characteristic '${companyCharacteristic.label}' of company '${company.name}'?`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean | null) => {
      if (!result) {
        return;
      }

      this.deleteCompanyCharacteristics(companyCharacteristic.id!);
    });
  }

  openConfirmDeleteCompanyHistoricCharacteristicDialog(
    company: CompanyReadDto,
    companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: `Delete historic characteristic '${companyHistoricCharacteristic.label}' of company '${company.name}'?`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean | null) => {
      if (!result) {
        return;
      }

      this.deleteCompanyHistoricCharacteristics(
        companyHistoricCharacteristic.id!
      );
    });
  }

  getCompanyCharacteristicOptions(
    company: CompanyReadDto
  ): Array<CharacteristicReadDto> {
    return this.characteristics.filter(
      (characteristic) =>
        company.companyCharacteristics!.find(
          (companyCharacteristic) =>
            companyCharacteristic.characteristicId === characteristic.id
        ) === undefined
    );
  }

  getCompanyHistoricCharacteristicOptions(
    company: CompanyReadDto
  ): Array<CharacteristicReadDto> {
    return this.historicCharacteristics.filter(
      (historicCharacteristic) =>
        company.companyHistoricCharacteristics!.find(
          (companyCharacteristic) =>
            companyCharacteristic.historicCharacteristicId ===
            historicCharacteristic.id
        ) === undefined
    );
  }

  private readCompanies(): void {
    this.companiesService
      .getCompanies()
      .subscribe((companies) => (this.companies = companies));
  }

  private createCompany(company: CompanyCreateDto): void {
    this.companiesService
      .postCompany({ body: company })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private updateCompany(companyId: string, company: CompanyUpdateDto): void {
    this.companiesService
      .putCompany({ id: companyId, body: company })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private deleteCompany(companyId: string): void {
    this.companiesService
      .deleteCompany({ id: companyId })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private readCharacteristics(): void {
    this.characteristicsService
      .getCharacteristics()
      .subscribe((characteristics) => (this.characteristics = characteristics));
  }

  private readHistoricCharacteristics(): void {
    this.historicCharacteristicsService
      .getHistoricCharacteristics()
      .subscribe(
        (historicCharacteristics) =>
          (this.historicCharacteristics = historicCharacteristics)
      );
  }

  private createCompanyCharacteristics(
    companyCharacteristic: CompanyCharacteristicCreateDto
  ): void {
    this.companyCharacteristicsService
      .postCompanyCharacteristic({ body: companyCharacteristic })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private createCompanyHistoricCharacteristics(
    companyHistoricCharacteristic: CompanyHistoricCharacteristicCreateDto
  ): void {
    this.companyHistoricCharacteristicsService
      .postCompanyHistoricCharacteristic({
        body: companyHistoricCharacteristic,
      })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private updateCompanyCharacteristic(
    companyCharacteristicId: string,
    companyCharacteristic: CompanyCharacteristicUpdateDto
  ): void {
    this.companyCharacteristicsService
      .putCompanyCharacteristic({
        id: companyCharacteristicId,
        body: companyCharacteristic,
      })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private updateCompanyHistoricCharacteristic(
    companyHistoricCharacteristicId: string,
    companyHistoricCharacteristic: CompanyHistoricCharacteristicUpdateDto
  ): void {
    this.companyHistoricCharacteristicsService
      .putCompanyHistoricCharacteristic({
        id: companyHistoricCharacteristicId,
        body: companyHistoricCharacteristic,
      })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private deleteCompanyCharacteristics(companyCharacteristicId: string): void {
    this.companyCharacteristicsService
      .deleteCompanyCharacteristic({ id: companyCharacteristicId })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }

  private deleteCompanyHistoricCharacteristics(
    companyHistoricCharacteristicId: string
  ): void {
    this.companyHistoricCharacteristicsService
      .deleteCompanyHistoricCharacteristic({
        id: companyHistoricCharacteristicId,
      })
      .pipe(tap(() => this.readCompanies()))
      .subscribe();
  }
}
