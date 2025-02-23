import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';

import { CompanyCharacteristicComponent } from '../company-characteristic/company-characteristic.component';
import { CompanyReadDto } from '../api/models/company-read-dto';
import { CompanyCharacteristicReadDto } from '../api/models/company-characteristic-read-dto';
import { CompanyCharacteristicUpdateDto } from '../api/models/company-characteristic-update-dto';

@Component({
  selector: 'app-company-panel-content',
  imports: [
    MatButtonModule,
    MatExpansionModule,
    MatIconModule,
    CompanyCharacteristicComponent,
  ],
  templateUrl: './company-panel-content.component.html',
  styleUrl: './company-panel-content.component.scss',
})
export class CompanyPanelContentComponent {
  @Input({ required: true }) company!: CompanyReadDto;
  @Output() addCompanyCharacteristic = new EventEmitter<void>();
  @Output() updateCompanyCharacteristic = new EventEmitter<{
    companyCharacteristic: CompanyCharacteristicReadDto;
    companyCharacteristicUpdateDto: CompanyCharacteristicUpdateDto;
  }>();
  @Output() deleteCompanyCharacteristic = new EventEmitter<{
    companyCharacteristic: CompanyCharacteristicReadDto;
  }>();

  onAddCompanyCharacteristic(): void {
    this.addCompanyCharacteristic.emit();
  }

  onUpdateCompanyCharacteristic(
    companyCharacteristic: CompanyCharacteristicReadDto,
    companyCharacteristicUpdateDto: CompanyCharacteristicUpdateDto
  ): void {
    this.updateCompanyCharacteristic.emit({
      companyCharacteristic,
      companyCharacteristicUpdateDto,
    });
  }

  onDeleteCompanyCharacteristic(
    companyCharacteristic: CompanyCharacteristicReadDto
  ): void {
    this.deleteCompanyCharacteristic.emit({ companyCharacteristic });
  }
}
