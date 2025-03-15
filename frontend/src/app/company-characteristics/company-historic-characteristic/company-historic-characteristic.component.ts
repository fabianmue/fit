import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';
import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';

@Component({
  selector: 'app-company-historic-characteristic',
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
  ],
  templateUrl: './company-historic-characteristic.component.html',
  styleUrl: './company-historic-characteristic.component.scss',
})
export class CompanyHistoricCharacteristicComponent {
  @Input({ required: true })
  companyHistoricCharacteristic!: CompanyHistoricCharacteristicReadDto;
  @Output() update = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();

  get icon(): string {
    return getCharacteristicIcon(this.companyHistoricCharacteristic);
  }

  onUpdate(): void {
    this.update.emit();
  }

  onDelete(): void {
    this.delete.emit();
  }
}
