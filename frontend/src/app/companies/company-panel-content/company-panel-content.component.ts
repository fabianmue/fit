import { AsyncPipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { Observable } from 'rxjs';

import { CompanyCharacteristicReadDto } from '@app/api/models/company-characteristic-read-dto';
import { CompanyCharacteristicUpdateDto } from '@app/api/models/company-characteristic-update-dto';
import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';
import { CompanyReadDto } from '@app/api/models/company-read-dto';
import { CompanyCharacteristicComponent } from '@app/company-characteristics/company-characteristic/company-characteristic.component';
import { CompanyHistoricCharacteristicComponent } from '@app/company-characteristics/company-historic-characteristic/company-historic-characteristic.component';
import { AuthenticationService } from '@app/shared/authentication.service';

@Component({
  selector: 'app-company-panel-content',
  imports: [
    AsyncPipe,
    CompanyCharacteristicComponent,
    CompanyHistoricCharacteristicComponent,
    MatButtonModule,
    MatExpansionModule,
    MatIconModule,
  ],
  templateUrl: './company-panel-content.component.html',
  styleUrl: './company-panel-content.component.scss',
})
export class CompanyPanelContentComponent {
  @Input({ required: true }) company!: CompanyReadDto;
  @Input({ required: true }) disableAddCompanyCharacteristic!: boolean;
  @Input({ required: true }) disableAddCompanyHistoricCharacteristic!: boolean;
  @Output() addCompanyCharacteristic = new EventEmitter<void>();
  @Output() addCompanyHistoricCharacteristic = new EventEmitter<void>();
  @Output() updateCompanyCharacteristic = new EventEmitter<{
    companyCharacteristic: CompanyCharacteristicReadDto;
    companyCharacteristicUpdateDto: CompanyCharacteristicUpdateDto;
  }>();
  @Output() updateCompanyHistoricCharacteristic = new EventEmitter<{
    companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto;
  }>();
  @Output() deleteCompanyCharacteristic = new EventEmitter<{
    companyCharacteristic: CompanyCharacteristicReadDto;
  }>();
  @Output() deleteCompanyHistoricCharacteristic = new EventEmitter<{
    companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto;
  }>();
  authenticated$: Observable<boolean>;

  constructor(private readonly authenticationService: AuthenticationService) {
    this.authenticated$ = this.authenticationService.authenticated$;
  }

  onAddCompanyCharacteristic(): void {
    this.addCompanyCharacteristic.emit();
  }

  onAddCompanyHistoricCharacteristic(): void {
    this.addCompanyHistoricCharacteristic.emit();
  }

  onUpdateCompanyCharacteristic(
    companyCharacteristic: CompanyCharacteristicReadDto,
    result: Partial<{ value: number | null; unit: string | null }>
  ): void {
    this.updateCompanyCharacteristic.emit({
      companyCharacteristic,
      companyCharacteristicUpdateDto: {
        value: result.value,
        unit: result.unit,
      } as CompanyCharacteristicUpdateDto,
    });
  }

  onUpdateCompanyHistoricCharacteristic(
    companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto
  ): void {
    this.updateCompanyHistoricCharacteristic.emit({
      companyHistoricCharacteristic,
    });
  }

  onDeleteCompanyCharacteristic(
    companyCharacteristic: CompanyCharacteristicReadDto
  ): void {
    this.deleteCompanyCharacteristic.emit({ companyCharacteristic });
  }

  onDeleteCompanyHistoricCharacteristic(
    companyHistoricCharacteristic: CompanyHistoricCharacteristicReadDto
  ): void {
    this.deleteCompanyHistoricCharacteristic.emit({
      companyHistoricCharacteristic,
    });
  }
}
